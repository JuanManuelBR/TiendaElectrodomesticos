<template>
  <div class="container">
    <div class="form-card">
      <!-- Encabezado -->
      <h1 class="title">Actualizar Televisor</h1>

      <!-- Búsqueda -->
      <div class="search-section">
        <label class="form-label">Código Televisor Buscar:</label>
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
              v-model="televisor.nombre" 
              class="form-input"
              placeholder="Ingrese nombre"
              :disabled="!televisorCargado"
            />
          </div>

          <!-- Precio -->
          <div class="form-group">
            <label class="form-label">Precio:</label>
            <input 
              type="number" 
              step="0.01"
              v-model="televisor.precio" 
              class="form-input"
              placeholder="Ingrese precio"
              :disabled="!televisorCargado"
            />
          </div>

          <!-- Marca -->
          <div class="form-group">
            <label class="form-label">Marca:</label>
            <input 
              type="text" 
              v-model="televisor.marca" 
              class="form-input"
              placeholder="Ingrese marca"
              :disabled="!televisorCargado"
            />
          </div>

          <!-- Fecha Creación -->
          <div class="form-group">
            <label class="form-label">Fecha Creación:</label>
            <input 
              type="datetime-local" 
              v-model="televisor.fechaCreacion" 
              class="form-input"
              :disabled="!televisorCargado"
            />
          </div>

          <!-- Control Remoto -->
          <div class="form-group">
            <label class="form-label">Control Remoto:</label>
            <input 
              type="number" 
              v-model="televisor.codigoControl" 
              class="form-input"
              placeholder="Código del control"
              :disabled="!televisorCargado"
            />
          </div>
        </div>

        <!-- Imagen derecha -->
        <div class="form-right">
          <div class="image-container">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <rect x="2" y="7" width="20" height="15" rx="2" ry="2"></rect>
              <polyline points="17 2 12 7 7 2"></polyline>
              <line x1="12" y1="17" x2="12" y2="17"></line>
              <line x1="8" y1="21" x2="16" y2="21"></line>
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
          :disabled="!televisorCargado"
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
      codigoBuscar: '',
      televisor: {
        codigo: '',
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: '',
        codigoControl: ''
      },
      televisorCargado: false,
      mensaje: '',
      mensajeTipo: ''
    }
  },
  methods: {
    async buscar() {
      try {
        if (!this.codigoBuscar) {
          this.mostrarMensaje('Por favor ingrese un código a buscar', 'warning');
          return;
        }

        const response = await fetch(
          `http://localhost:8090/electrodomestico/${this.codigoBuscar}`,
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
          this.televisor.codigo = data.codigo;
          this.televisor.nombre = data.nombre;
          this.televisor.precio = data.precio;
          this.televisor.marca = data.marca;
          
          // Formatear fecha para datetime-local
          const fecha = new Date(data.fechaCreacion);
          const year = fecha.getFullYear();
          const month = String(fecha.getMonth() + 1).padStart(2, '0');
          const day = String(fecha.getDate()).padStart(2, '0');
          const hours = String(fecha.getHours()).padStart(2, '0');
          const minutes = String(fecha.getMinutes()).padStart(2, '0');
          this.televisor.fechaCreacion = `${year}-${month}-${day}T${hours}:${minutes}`;
          
          // Manejar control remoto (puede ser null, un objeto o un número)
          if (data.controlRemoto !== null && data.controlRemoto !== undefined) {
            if (typeof data.controlRemoto === 'object' && data.controlRemoto.codigo) {
              this.televisor.codigoControl = data.controlRemoto.codigo;
            } else if (typeof data.controlRemoto === 'number') {
              this.televisor.codigoControl = data.controlRemoto;
            } else {
              this.televisor.codigoControl = '';
            }
          } else {
            this.televisor.codigoControl = '';
          }
          
          this.televisorCargado = true;
          this.mostrarMensaje('Televisor cargado correctamente', 'success');
        } else {
          this.limpiarFormulario();
          const error = await response.text();
          this.mostrarMensaje('Televisor no encontrado o error al obtener datos', 'error');
        }
      } catch (error) {
        this.limpiarFormulario();
        this.mostrarMensaje('Ocurrió un error al buscar: ' + error.message, 'error');
      }
    },

    async actualizar() {
      try {
        // Validaciones
        if (!this.televisor.nombre || !this.televisor.precio || 
            !this.televisor.marca || !this.televisor.fechaCreacion) {
          this.mostrarMensaje('Por favor complete todos los campos', 'warning');
          return;
        }

        const codigoTv = parseInt(this.codigoBuscar);
        
        // Validar código del control si se ingresó
        let codigoControl = null;
        if (this.televisor.codigoControl && this.televisor.codigoControl !== '') {
          const parsed = parseInt(this.televisor.codigoControl);
          if (isNaN(parsed)) {
            this.mostrarMensaje('El código del control debe ser un número válido', 'warning');
            return;
          }
          codigoControl = parsed;
        }

        // Formatear fecha para el backend
        const fechaFormateada = new Date(this.televisor.fechaCreacion)
          .toISOString()
          .slice(0, 19);

        // 1. Actualizar el televisor (sin control en el body)
        const body = {
          tipo: 'televisor',
          codigo: codigoTv,
          nombre: this.televisor.nombre,
          marca: this.televisor.marca,
          fechaCreacion: fechaFormateada,
          precio: parseFloat(this.televisor.precio)
        };

        const response = await fetch(
          `http://localhost:8090/electrodomestico/${codigoTv}`,
          {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json',
              'Authorization': 'Basic ' + btoa('admin:admin')
            },
            body: JSON.stringify(body)
          }
        );

        if (!response.ok) {
          const error = await response.text();
          this.mostrarMensaje('Error al actualizar el televisor: ' + error, 'error');
          return;
        }

        // 2. Si no hay código de control, terminar aquí
        if (codigoControl === null) {
          this.mostrarMensaje('Televisor actualizado correctamente (sin control remoto)', 'success');
          setTimeout(() => {
            this.limpiarFormulario();
            this.codigoBuscar = '';
          }, 2000);
          return;
        }

        // 3. Verificar que el control existe
        const responseCheck = await fetch(
          `http://localhost:8090/electrodomestico/control/${codigoControl}`,
          {
            method: 'GET',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );

        if (!responseCheck.ok) {
          this.mostrarMensaje('El control con ese código no existe. Ingrese un control válido.', 'warning');
          return;
        }

        // 4. Asignar el control al televisor
        const responseAsignar = await fetch(
          `http://localhost:8090/electrodomestico/asignarControl/${codigoTv}/${codigoControl}`,
          {
            method: 'PUT',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );

        if (responseAsignar.ok) {
          this.mostrarMensaje('Televisor actualizado y control asignado correctamente', 'success');
          setTimeout(() => {
            this.limpiarFormulario();
            this.codigoBuscar = '';
          }, 2000);
        } else {
          this.mostrarMensaje('No se pudo asignar el control. Es posible que ya esté asignado a otro televisor.', 'warning');
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error al actualizar: ' + error.message, 'error');
      }
    },

    limpiarFormulario() {
      this.televisor = {
        codigo: '',
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: '',
        codigoControl: ''
      };
      this.televisorCargado = false;
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