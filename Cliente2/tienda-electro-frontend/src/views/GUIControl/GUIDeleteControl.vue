<template>
  <div class="container">
    <div class="form-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Eliminar Control Remoto</h1>
        <button @click="navigateTo('/App')" class="btn-close">✕</button>
      </div>

      <!-- Búsqueda -->
      <div class="search-section">
        <label class="form-label">N° Referencia Eliminar:</label>
        <input 
          type="number" 
          v-model="numeroReferenciaBuscar" 
          class="form-input"
          placeholder="Ingrese N° Referencia a eliminar"
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
            <label class="form-label">Alcance:</label>
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
            <label class="form-label">Fecha Venta:</label>
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
              <rect x="4" y="2" width="16" height="20" rx="2" ry="2"></rect>
              <circle cx="12" cy="6" r="1" fill="currentColor"></circle>
              <circle cx="9" cy="10" r="1" fill="currentColor"></circle>
              <circle cx="15" cy="10" r="1" fill="currentColor"></circle>
              <circle cx="9" cy="14" r="1" fill="currentColor"></circle>
              <circle cx="12" cy="14" r="1" fill="currentColor"></circle>
              <circle cx="15" cy="14" r="1" fill="currentColor"></circle>
              <rect x="7" y="17" width="10" height="3" rx="1"></rect>
            </svg>
            <svg class="delete-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <path d="M3 6h18"></path>
              <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6"></path>
              <path d="M8 6V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>
              <line x1="10" y1="11" x2="10" y2="17"></line>
              <line x1="14" y1="11" x2="14" y2="17"></line>
            </svg>
          </div>
        </div>
      </div>

      <!-- Botones -->
      <div class="button-group">
        <button @click="buscar" class="btn btn-search">Buscar</button>
        <button 
          @click="eliminar" 
          class="btn btn-delete"
          :disabled="!controlCargado"
        >
          Eliminar
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
        // Parsear número de referencia igual que en C#
        const numeroReferenciaBuscar = parseInt(this.numeroReferenciaBuscar);
        
        if (isNaN(numeroReferenciaBuscar)) {
          this.mostrarMensaje('Por favor ingrese un N° Referencia válido', 'warning');
          return;
        }

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
            this.control.alcance = data.alcance ? data.alcance.toString() : '';
            this.control.marca = data.marca;
            
            // Formatear fecha: yyyy-MM-dd HH:mm:ss (igual que C#)
            if (data.fechaVenta) {
              const fecha = new Date(data.fechaVenta);
              const year = fecha.getFullYear();
              const month = String(fecha.getMonth() + 1).padStart(2, '0');
              const day = String(fecha.getDate()).padStart(2, '0');
              const hours = String(fecha.getHours()).padStart(2, '0');
              const minutes = String(fecha.getMinutes()).padStart(2, '0');
              const seconds = String(fecha.getSeconds()).padStart(2, '0');
              this.control.fechaVenta = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
            } else {
              this.control.fechaVenta = '';
            }
            
            // Habilitar botón eliminar
            this.controlCargado = true;
          } else {
            this.limpiarCampos();
            this.mostrarMensaje('No se encontró el control con ese N° Referencia', 'warning');
          }
        } else {
          this.limpiarCampos();
          this.mostrarMensaje('No se encontró el control con ese N° Referencia', 'warning');
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error al buscar el control: ' + error.message, 'error');
      }
    },

    async eliminar() {
      if (!this.controlCargado) {
        return;
      }

      try {
        const numeroReferenciaEliminar = parseInt(this.numeroReferenciaBuscar);

        const response = await fetch(
          `http://localhost:8090/televisor/control/${numeroReferenciaEliminar}`,
          {
            method: 'DELETE',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );

        if (response.ok) {
          this.mostrarMensaje('Control eliminado correctamente', 'success');
          
          // Limpiar campos después de eliminar (igual que C#)
          this.limpiarCampos();
        } else {
          const error = await response.text();
          this.mostrarMensaje('Error al eliminar el control: ' + error, 'error');
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error al eliminar el control: ' + error.message, 'warning');
      }
    },

    limpiarCampos() {
      this.numeroReferenciaBuscar = '';
      this.control = {
        nombre: '',
        alcance: '',
        marca: '',
        fechaVenta: ''
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
  width: 150%;
  max-width: 800px;
  margin: 2 auto;
  box-sizing: border-box;
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

.delete-icon {
  position: absolute;
  bottom: 10px;
  right: 10px;
  width: 30px;
  height: 30px;
  color: #ff4444;
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

.btn-delete {
  background: #8080ff;
  color: #000;
}

.btn-delete:hover:not(:disabled) {
  background: #6666cc;
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(128, 128, 255, 0.4);
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