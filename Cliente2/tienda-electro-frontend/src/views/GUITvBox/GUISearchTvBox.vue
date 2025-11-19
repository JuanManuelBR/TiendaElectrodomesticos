<template>
  <div class="container">
    <div class="form-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Buscar TV Box</h1>
        <button @click="navigateTo('/App')" class="btn-close">✕</button>
      </div>

      <!-- Búsqueda -->
      <div class="search-section">
        <label class="form-label">N° Referencia Buscar:</label>
        <input 
          type="number" 
          v-model="numeroReferenciaBuscar" 
          class="form-input"
          placeholder="Ingrese número a buscar"
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
              v-model="tvBox.nombre" 
              class="form-input"
              placeholder="Nombre"
              readonly
            />
          </div>

          <!-- Precio -->
          <div class="form-group">
            <label class="form-label">Precio:</label>
            <input 
              type="text" 
              v-model="tvBox.precio" 
              class="form-input"
              placeholder="Precio"
              readonly
            />
          </div>

          <!-- Marca -->
          <div class="form-group">
            <label class="form-label">Marca:</label>
            <input 
              type="text" 
              v-model="tvBox.marca" 
              class="form-input"
              placeholder="Marca"
              readonly
            />
          </div>

          <!-- Fecha Creación -->
          <div class="form-group">
            <label class="form-label">Fecha Creación:</label>
            <input 
              type="text" 
              v-model="tvBox.fechaCreacion" 
              class="form-input"
              placeholder="Fecha de creación"
              readonly
            />
          </div>

          <!-- Televisor Asociado -->
          <div class="form-group">
            <label class="form-label">TV Asociado:</label>
            <input 
              type="text" 
              v-model="tvBox.televisorAsociado" 
              class="form-input"
              placeholder="N° Referencia del televisor"
              readonly
            />
          </div>
        </div>

        <!-- Imagen derecha - TV Box -->
        <div class="form-right">
          <div class="image-container">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <rect x="3" y="8" width="18" height="12" rx="2" />
              <circle cx="7" cy="14" r="1" fill="currentColor" />
              <circle cx="12" cy="14" r="1" fill="currentColor" />
              <circle cx="17" cy="14" r="1" fill="currentColor" />
              <path d="M8 8v-2a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" />
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
      numeroReferenciaBuscar: '',
      tvBox: {
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: '',
        televisorAsociado: ''
      },
      mensaje: '',
      mensajeTipo: ''
    }
  },
  methods: {
    async buscar() {
      try {
        // =============================
        // ✅ VALIDACIÓN 1: CAMPO VACÍO
        // =============================
        if (!this.numeroReferenciaBuscar || String(this.numeroReferenciaBuscar).trim() === '') {
          this.mostrarMensaje('Por favor ingrese un ID de TV Box.', 'warning');
          return;
        }

        // =============================
        // ✅ VALIDACIÓN 2: NÚMERO VÁLIDO
        // =============================
        const idBuscar = parseInt(this.numeroReferenciaBuscar);
        if (isNaN(idBuscar)) {
          this.mostrarMensaje('El ID debe ser un número válido.', 'warning');
          return;
        }

        // =============================
        // 🔍 BUSCAR TVBOX
        // =============================
        const response = await fetch(
          `http://localhost:8090/televisor/tvbox/numero/${idBuscar}`,
          {
            method: 'GET',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );

        if (!response.ok) {
          // =============================
          // ❌ ERROR: LIMPIAR TODOS LOS CAMPOS
          // =============================
          this.limpiarFormulario();
          
          const error = await response.text();
          this.mostrarMensaje('No se encontró el TV Box o hubo un error.\n' + error, 'error');
          return;
        }

        // =============================
        // ✅ PROCESAR JSON (IGUAL QUE C#)
        // =============================
        const data = await response.json();
        
        this.tvBox.nombre = data.nombre;
        
        // Formato precio con 2 decimales (F2 en C#)
        this.tvBox.precio = parseFloat(data.precio).toFixed(2);
        
        this.tvBox.marca = data.marca;
        
        // Formatear fecha: yyyy-MM-dd HH:mm:ss (igual que C#)
        const fecha = new Date(data.fechaCreacion);
        const year = fecha.getFullYear();
        const month = String(fecha.getMonth() + 1).padStart(2, '0');
        const day = String(fecha.getDate()).padStart(2, '0');
        const hours = String(fecha.getHours()).padStart(2, '0');
        const minutes = String(fecha.getMinutes()).padStart(2, '0');
        const seconds = String(fecha.getSeconds()).padStart(2, '0');
        this.tvBox.fechaCreacion = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
        
        // =============================
        // 📺 televisorNumeroReferencia ES OPCIONAL (igual que C#)
        // =============================
        if (data.televisorNumeroReferencia !== undefined && 
            data.televisorNumeroReferencia !== null) {
          this.tvBox.televisorAsociado = data.televisorNumeroReferencia.toString();
        } else {
          this.tvBox.televisorAsociado = ''; // No tiene televisor asociado
        }
        
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error: ' + error.message, 'warning');
      }
    },

    limpiarFormulario() {
      this.tvBox = {
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: '',
        televisorAsociado: ''
      };
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