<template>
  <div class="container">
    <div class="form-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Actualizar Control Remoto</h1>
        <button @click="navigateTo('/App')" class="btn-close">✕</button>
      </div>

      <!-- Búsqueda -->
      <div class="search-section">
        <label class="form-label">N° Referencia Buscar:</label>
        <input 
          type="number" 
          v-model="numeroReferenciaBuscar" 
          class="form-input"
          placeholder="Ingrese N° Referencia a buscar"
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
              placeholder="Ingrese nombre"
              :disabled="!controlCargado"
            />
          </div>

          <!-- Alcance -->
          <div class="form-group">
            <label class="form-label">Alcance:</label>
            <input 
              type="number" 
              step="0.01"
              v-model="control.alcance" 
              class="form-input"
              placeholder="Ingrese alcance"
              :disabled="!controlCargado"
            />
          </div>

          <!-- Marca -->
          <div class="form-group">
            <label class="form-label">Marca:</label>
            <input 
              type="text" 
              v-model="control.marca" 
              class="form-input"
              placeholder="Ingrese marca"
              :disabled="!controlCargado"
            />
          </div>

          <!-- Fecha Venta -->
          <div class="form-group">
            <label class="form-label">Fecha Venta:</label>
            <input 
              type="datetime-local" 
              v-model="control.fechaVenta" 
              class="form-input"
              :disabled="!controlCargado"
            />
          </div>
        </div>

        <!-- Imagen derecha -->
        <div class="form-right">
          <div class="image-container">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <rect x="4" y="2" width="16" height="20" rx="2" ry="2"></rect>
              <circle cx="12" cy="6" r="1" fill="currentColor"></circle>
              <circle cx="9" cy="10" r="1" fill="currentColor"></circle>
              <circle cx="15" cy="10" r="1" fill="currentColor"></circle>
              <circle cx="9" cy="14" r="1" fill="currentColor"></circle>
              <circle cx="12" cy="14" r="1" fill="currentColor"></circle>
              <circle cx="15" cy="14" r="1" fill="currentColor"></circle>
              <rect x="7" y="17" width="10" height="3" rx="1"></rect>
            </svg>
            <svg class="edit-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"></path>
              <path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"></path>
            </svg>
          </div>
        </div>
      </div>

      <!-- Botones -->
      <div class="button-group">
        <button @click="buscar" class="btn btn-search">Buscar</button>
        <button 
          @click="actualizar" 
          class="btn btn-update"
          :disabled="!controlCargado"
        >
          Actualizar
        </button>
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
      numeroReferenciaBuscar: '',
      control: {
        numeroReferencia: '',
        nombre: '',
        alcance: '',
        marca: '',
        fechaVenta: ''
      },
      controlCargado: false,
      mensaje: '',
      mensajeTipo: ''
    }
  },
  methods: {
    async buscar() {
      try {
        // Validación 1: Campo vacío o solo espacios (igual que C#)
        if (!this.numeroReferenciaBuscar || String(this.numeroReferenciaBuscar).trim() === '') {
          this.mostrarMensaje('Por favor ingresa el N° Referencia del control a buscar', 'warning');
          return;
        }

        const numeroReferenciaBuscar = parseInt(this.numeroReferenciaBuscar);

        const response = await fetch(
          `http://localhost:8090/televisor/control/${numeroReferenciaBuscar}`,
          {
            method: 'GET',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );

        // Verificar respuesta exitosa Y contenido no vacío (como en C#)
        if (response.ok && response.headers.get('content-length') !== '0') {
          const responseText = await response.text();
          
          if (responseText && responseText.trim() !== '') {
            const data = JSON.parse(responseText);
            
            // Cargar datos en el formulario
            this.control.nombre = data.nombre;
            this.control.alcance = data.alcance.toString();
            this.control.marca = data.marca;
            
            // Formatear fecha para datetime-local y guardar texto formateado
            const fecha = new Date(data.fechaVenta);
            const year = fecha.getFullYear();
            const month = String(fecha.getMonth() + 1).padStart(2, '0');
            const day = String(fecha.getDate()).padStart(2, '0');
            const hours = String(fecha.getHours()).padStart(2, '0');
            const minutes = String(fecha.getMinutes()).padStart(2, '0');
            const seconds = String(fecha.getSeconds()).padStart(2, '0');
            
            // Formato para mostrar: yyyy-MM-dd HH:mm:ss
            this.control.fechaVentaTexto = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
            
            // Formato para datetime-local: yyyy-MM-ddTHH:mm
            this.control.fechaVenta = `${year}-${month}-${day}T${hours}:${minutes}`;
            
            // Habilitar botón actualizar
            this.controlCargado = true;
          } else {
            this.limpiarCampos();
            this.mostrarMensaje('No se encontró ningún control con ese N° Referencia', 'info');
          }
        } else {
          this.limpiarCampos();
          this.mostrarMensaje('No se encontró ningún control con ese N° Referencia', 'info');
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error al buscar el control: ' + error.message, 'error');
      }
    },

    async actualizar() {
      try {
        // Parsear valores igual que en C#
        const numeroReferenciaControl = parseInt(this.numeroReferenciaBuscar);
        const nombre = this.control.nombre;
        const alcance = parseFloat(this.control.alcance);
        const marca = this.control.marca;
        const fechaVenta = new Date(this.control.fechaVenta);

        // Validar parsing
        if (isNaN(alcance)) {
          this.mostrarMensaje('El alcance debe ser un número válido', 'warning');
          return;
        }

        // Formatear fecha: yyyy-MM-dd'T'HH:mm:ss (igual que C#)
        const year = fechaVenta.getFullYear();
        const month = String(fechaVenta.getMonth() + 1).padStart(2, '0');
        const day = String(fechaVenta.getDate()).padStart(2, '0');
        const hours = String(fechaVenta.getHours()).padStart(2, '0');
        const minutes = String(fechaVenta.getMinutes()).padStart(2, '0');
        const seconds = String(fechaVenta.getSeconds()).padStart(2, '0');
        const fechaFormateada = `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;

        // Crear body igual que en C# (usando numeroReferencia)
        const body = {
          numeroReferencia: numeroReferenciaControl,
          nombre: nombre,
          alcance: alcance,
          marca: marca,
          fechaVenta: fechaFormateada
        };

        const response = await fetch(
          `http://localhost:8090/televisor/control/${numeroReferenciaControl}`,
          {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json',
              'Authorization': 'Basic ' + btoa('admin:admin')
            },
            body: JSON.stringify(body)
          }
        );

        if (response.ok) {
          this.mostrarMensaje('Control actualizado correctamente', 'success');
        } else {
          const error = await response.text();
          this.mostrarMensaje('Error al actualizar el control: ' + error, 'error');
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error al actualizar el control: ' + error.message, 'warning');
      }
    },

    limpiarCampos() {
      this.numeroReferenciaBuscar = '';
      this.control = {
        numeroReferencia: '',
        nombre: '',
        alcance: '',
        marca: '',
        fechaVenta: '',
        fechaVentaTexto: ''
      };
      this.controlCargado = false;
    },

    mostrarMensaje(texto, tipo) {
      this.mensaje = texto;
      this.mensajeTipo = tipo;
      setTimeout(() => {
        this.mensaje = '';
        this.mensajeTipo = '';
      }, 5000);
    },

    navigateTo(route) {
      this.$router.push(route);
    }
  }
}
</script>

<style scoped>
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

.header {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-bottom: 30px;
  position: relative;
}

.title {
  font-size: 2rem;
  font-weight: bold;
  color: #000;
  margin: 0;
}

.btn-close {
  background: #d32f2f;
  color: white;
  border: none;
  border-radius: 50%;
  width: 40px;
  height: 40px;
  font-size: 1.5rem;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s;
  display: flex;
  align-items: center;
  justify-content: center;
  line-height: 1;
  position: absolute;
  right: 0;
  top: 50%;
  transform: translateY(-50%);
}

.btn-close:hover {
  background: #b71c1c;
  transform: translateY(-50%) scale(1.1);
  box-shadow: 0 4px 12px rgba(211, 47, 47, 0.4);
}

.search-section {
  display: flex;
  align-items: center;
  gap: 15px;
  margin-bottom: 25px;
  padding: 15px;
  background: rgba(255, 255, 255, 0.1);
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

.form-input:disabled {
  background: #e0e0e0;
  cursor: not-allowed;
  opacity: 0.6;
}

.form-input:focus:not(:disabled) {
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

.edit-icon {
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
  gap: 15px;
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

.btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.btn-search {
  background: #80ff80;
  color: #000;
}

.btn-search:hover:not(:disabled) {
  background: #66cc66;
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(128, 255, 128, 0.4);
}

.btn-update {
  background: #ffcc80;
  color: #000;
}

.btn-update:hover:not(:disabled) {
  background: #ffb84d;
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(255, 204, 128, 0.4);
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