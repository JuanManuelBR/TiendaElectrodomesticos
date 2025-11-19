<template>
  <div class="container">
    <div class="form-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Agregar Control Remoto</h1>
        <button @click="navigateTo('/App')" class="btn-close">✕</button>
      </div>

      <!-- Formulario -->
      <div class="form-grid">
        <div class="form-left">
          <!-- N° Referencia (antes Código) -->
          <div class="form-group">
            <label class="form-label">N° Referencia:</label>
            <input 
              type="number" 
              v-model="control.numeroReferencia" 
              class="form-input"
              placeholder="Ingrese número de referencia"
            />
          </div>

          <!-- Nombre -->
          <div class="form-group">
            <label class="form-label">Nombre:</label>
            <input 
              type="text" 
              v-model="control.nombre" 
              class="form-input"
              placeholder="Ingrese nombre"
            />
          </div>

          <!-- Alcance -->
          <div class="form-group">
            <label class="form-label">Alcance:</label>
            <input 
              type="text" 
              v-model="control.alcance" 
              class="form-input"
              placeholder="Ingrese alcance"
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
            />
          </div>

          <!-- Fecha Venta -->
          <div class="form-group">
            <label class="form-label">Fecha Venta:</label>
            <input 
              type="datetime-local" 
              v-model="control.fechaVenta" 
              class="form-input"
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
            <div class="plus-icon">+</div>
          </div>
        </div>
      </div>

      <!-- Botones -->
      <div class="button-group">
        <button @click="guardar" class="btn btn-save">Guardar</button>
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
      control: {
        numeroReferencia: '',
        nombre: '',
        marca: '',
        fechaVenta: this.getFechaActual(),
        alcance: ''
      },
      mensaje: '',
      mensajeTipo: ''
    }
  },
  methods: {
    getFechaActual() {
      const now = new Date();
      const year = now.getFullYear();
      const month = String(now.getMonth() + 1).padStart(2, '0');
      const day = String(now.getDate()).padStart(2, '0');
      const hours = String(now.getHours()).padStart(2, '0');
      const minutes = String(now.getMinutes()).padStart(2, '0');
      return `${year}-${month}-${day}T${hours}:${minutes}`;
    },

    async guardar() {
      try {
        // Parsear valores igual que en C#
        const codigoControl = parseInt(this.control.numeroReferencia);
        const nombreControl = this.control.nombre;
        const marcaControl = this.control.marca;
        const alcanceControl = this.control.alcance;
        const fechaVentaControl = new Date(this.control.fechaVenta);

        // Validar que se parsearon correctamente
        if (isNaN(codigoControl)) {
          this.mostrarMensaje('El número de referencia debe ser un número válido', 'warning');
          return;
        }

        // Formatear fecha: yyyy-MM-ddTHH:mm:ss (igual que C#)
        const year = fechaVentaControl.getFullYear();
        const month = String(fechaVentaControl.getMonth() + 1).padStart(2, '0');
        const day = String(fechaVentaControl.getDate()).padStart(2, '0');
        const hours = String(fechaVentaControl.getHours()).padStart(2, '0');
        const minutes = String(fechaVentaControl.getMinutes()).padStart(2, '0');
        const seconds = String(fechaVentaControl.getSeconds()).padStart(2, '0');
        const fechaFormateada = `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;

        // Crear body igual que en C#
        const body = {
          numeroReferencia: codigoControl,
          nombre: nombreControl,
          marca: marcaControl,
          fechaVenta: fechaFormateada,
          alcance: alcanceControl
        };

        // Realizar petición POST
        const response = await fetch('http://localhost:8090/televisor/control', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Basic ' + btoa('admin:admin')
          },
          body: JSON.stringify(body)
        });

        if (response.ok) {
          this.mostrarMensaje('Control guardado con éxito', 'success');
          
          // Limpiar formulario después de mostrar el mensaje
          setTimeout(() => {
            this.limpiarFormulario();
          }, 2000);
        } else {
          const error = await response.text();
          this.mostrarMensaje('Error: ' + error, 'error');
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error inesperado: ' + error.message, 'warning');
      }
    },

    mostrarMensaje(texto, tipo) {
      this.mensaje = texto;
      this.mensajeTipo = tipo;
      setTimeout(() => {
        this.mensaje = '';
        this.mensajeTipo = '';
      }, 5000);
    },

    limpiarFormulario() {
      this.control = {
        numeroReferencia: '',
        nombre: '',
        marca: '',
        fechaVenta: this.getFechaActual(),
        alcance: ''
      };
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

.form-input:focus {
  outline: none;
  border-color: #f5576c;
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

.image-container svg {
  width: 80px;
  height: 80px;
  color: #666;
}

.plus-icon {
  position: absolute;
  bottom: 10px;
  right: 10px;
  font-size: 2rem;
  color: #666;
  font-weight: bold;
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

.btn-save {
  background: #80ff80;
  color: #000;
}

.btn-save:hover {
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

  .button-group {
    flex-direction: column;
  }
}
</style>