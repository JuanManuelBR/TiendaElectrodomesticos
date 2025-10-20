<template>
  <div class="container">
    <div class="form-card">
      <!-- Encabezado -->
      <h1 class="title">Buscar Control Remoto</h1>

      <!-- Búsqueda -->
      <div class="search-section">
        <label class="form-label">Código Control Buscar:</label>
        <input 
          type="number" 
          v-model="codigoBuscar" 
          class="form-input"
          placeholder="Ingrese código a buscar"
          @keyup.enter="buscar"
        />
      </div>

      <!-- Formulario -->
      <div class="form-grid">
        <div class="form-left">
          <!-- Nombre -->
          <div class="form-group">
            <label class="form-label">Nombre:</label>
            <input 
              type="text" 
              v-model="control.nombre" 
              class="form-input"
              placeholder="Nombre"
              readonly
            />
          </div>

          <!-- Alcance -->
          <div class="form-group">
            <label class="form-label">Alcance (m):</label>
            <input 
              type="text" 
              v-model="control.alcance" 
              class="form-input"
              placeholder="Alcance"
              readonly
            />
          </div>

          <!-- Marca -->
          <div class="form-group">
            <label class="form-label">Marca:</label>
            <input 
              type="text" 
              v-model="control.marca" 
              class="form-input"
              placeholder="Marca"
              readonly
            />
          </div>

          <!-- Fecha Venta -->
          <div class="form-group">
            <label class="form-label">Fecha de Venta:</label>
            <input 
              type="text" 
              v-model="control.fechaVenta" 
              class="form-input"
              placeholder="Fecha de venta"
              readonly
            />
          </div>
        </div>

        <!-- Imagen derecha -->
        <div class="form-right">
          <div class="image-container">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <rect x="7" y="2" width="10" height="20" rx="2"></rect>
              <circle cx="12" cy="7" r="1.2"></circle>
              <circle cx="12" cy="11" r="1.2"></circle>
              <circle cx="12" cy="15" r="1.2"></circle>
              <rect x="10.2" y="18.2" width="3.6" height="1.6" rx="0.8"></rect>
            </svg>
            <svg class="search-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <circle cx="11" cy="11" r="8"></circle>
              <path d="m21 21-4.35-4.35"></path>
            </svg>
          </div>
        </div>
      </div>

      <!-- Botones -->
      <div class="button-group">
        <button @click="buscar" class="btn btn-search">Buscar</button>
      </div>

      <!-- Mensajes -->
      <div v-if="mensaje" :class="['mensaje', mensajeTipo]">
        {{ mensaje }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      codigoBuscar: '',
      control: {
        nombre: '',
        alcance: '',
        marca: '',
        fechaVenta: ''
      },
      mensaje: '',
      mensajeTipo: ''
    }
  },
  methods: {
    async buscar() {
      try {
        // Validación: Campo vacío o no numérico
        if (!this.codigoBuscar || String(this.codigoBuscar).trim() === '') {
          this.mostrarMensaje('Por favor ingrese un código de control remoto', 'warning');
          return;
        }

        const codigoBuscar = parseInt(this.codigoBuscar);
        if (isNaN(codigoBuscar)) {
          this.mostrarMensaje('El código debe ser un número válido', 'warning');
          return;
        }

        // Petición al backend (igual que en C#)
        const response = await fetch(
          `http://localhost:8090/electrodomestico/control/${codigoBuscar}`,
          {
            method: 'GET',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );

        if (response.ok) {
          const data = await response.json();

          // Cargar datos en el formulario
          this.control.nombre = data.nombre ?? '';
          this.control.alcance = this.formatNumero(data.alcance);
          this.control.marca = data.marca ?? '';

          // Formatear fecha (yyyy-MM-dd HH:mm:ss)
          this.control.fechaVenta = this.formatFecha(data.fechaVenta);
        } else {
          this.limpiarFormulario();
          this.mostrarMensaje('No se encontró ningún control con ese código', 'info');
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error: ' + error.message, 'error');
      }
    },

    limpiarFormulario() {
      this.control = {
        nombre: '',
        alcance: '',
        marca: '',
        fechaVenta: ''
      };
    },

    formatFecha(fechaISO) {
      if (!fechaISO) return '';
      const fecha = new Date(fechaISO);
      if (isNaN(fecha.getTime())) return '';
      const year = fecha.getFullYear();
      const month = String(fecha.getMonth() + 1).padStart(2, '0');
      const day = String(fecha.getDate()).padStart(2, '0');
      const hours = String(fecha.getHours()).padStart(2, '0');
      const minutes = String(fecha.getMinutes()).padStart(2, '0');
      const seconds = String(fecha.getSeconds()).padStart(2, '0');
      return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
    },

    formatNumero(valor) {
      if (valor === null || valor === undefined || valor === '') return '';
      const num = Number(valor);
      if (isNaN(num)) return valor;
      return Number.isInteger(num) ? num.toString() : num.toFixed(2);
    },

    mostrarMensaje(texto, tipo) {
      this.mensaje = texto;
      this.mensajeTipo = tipo;
      setTimeout(() => {
        this.mensaje = '';
        this.mensajeTipo = '';
      }, 5000);
    }
  }
}
</script>

<style scoped>
/* 🔹 Exactamente igual que tu CSS original */
.container {
  min-height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 20px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

.form-card {
  background: DarkGray;
  padding: 30px;
  border-radius: 10px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.3);
  width: 100%;
  max-width: 700px;
}

.title {
  font-size: 2rem;
  font-weight: bold;
  text-align: center;
  margin-bottom: 30px;
  color: #000;
}

.search-section {
  display: flex;
  align-items: center;
  gap: 15px;
  margin-bottom: 25px;
  padding: 15px;
  background: #d3d3d3;
  border-radius: 8px;
}

.search-section .form-label {
  min-width: auto;
  white-space: nowrap;
}

.form-grid {
  display: grid;
  grid-template-columns: 1fr auto;
  gap: 20px;
  margin-bottom: 30px;
}

.form-left {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.form-group {
  display: flex;
  align-items: center;
  gap: 10px;
}

.form-label {
  font-size: 1.1rem;
  font-weight: 500;
  min-width: 150px;
  color: #000;
}

.form-input {
  flex: 1;
  padding: 10px 15px;
  font-size: 1rem;
  border: 2px solid #999;
  border-radius: 5px;
  background: #f5f5f5;
  transition: all 0.3s;
}

.form-input:read-only {
  background: #d3d3d3;
  cursor: default;
}

.form-input:focus:not(:read-only) {
  outline: none;
  border-color: #667eea;
  background: #fff;
}

.form-right {
  display: flex;
  align-items: center;
  justify-content: center;
}

.image-container {
  width: 150px;
  height: 150px;
  border: 2px dashed #666;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  background: #f0f0f0;
  position: relative;
}

.image-container > svg:first-child {
  width: 80px;
  height: 80px;
  color: #666;
}

.search-icon {
  position: absolute;
  bottom: 10px;
  right: 10px;
  width: 30px;
  height: 30px;
  color: #666;
}

.button-group {
  display: flex;
  justify-content: space-between;
  gap: 20px;
  margin-top: 20px;
}

.btn {
  flex: 1;
  padding: 12px 30px;
  font-size: 1.1rem;
  font-weight: 600;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-search {
  background: #80ff80;
  color: #000;
}

.btn-search:hover {
  background: #66cc66;
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(128, 255, 128, 0.4);
}

.mensaje {
  margin-top: 20px;
  padding: 15px;
  border-radius: 5px;
  text-align: center;
  font-weight: 600;
  animation: slideIn 0.3s ease-out;
}

@keyframes slideIn {
  from {
    opacity: 0;
    transform: translateY(-10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.success {
  background: #d4edda;
  color: #155724;
  border: 1px solid #c3e6cb;
}

.error {
  background: #f8d7da;
  color: #721c24;
  border: 1px solid #f5c6cb;
}

.warning {
  background: #fff3cd;
  color: #856404;
  border: 1px solid #ffeeba;
}

.info {
  background: #d1ecf1;
  color: #0c5460;
  border: 1px solid #bee5eb;
}

@media (max-width: 768px) {
  .form-grid {
    grid-template-columns: 1fr;
  }

  .form-group {
    flex-direction: column;
    align-items: flex-start;
  }

  .form-label {
    min-width: auto;
  }

  .search-section {
    flex-direction: column;
    align-items: flex-start;
  }

  .button-group {
    flex-direction: column;
  }
}
</style>
