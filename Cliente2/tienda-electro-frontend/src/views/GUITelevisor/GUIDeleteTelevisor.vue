<template>
  <div class="container">
    <div class="form-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Eliminar Televisor</h1>
        <button @click="navigateTo('/App')" class="btn-close">✕</button>
      </div>

      <!-- Búsqueda -->
      <div class="search-section">
        <label class="form-label">N° Referencia Televisor Eliminar:</label>
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
              v-model="televisor.nombre" 
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
              v-model="televisor.precio" 
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
              v-model="televisor.marca" 
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
              v-model="televisor.fechaCreacion" 
              class="form-input"
              placeholder="Fecha de creación"
              readonly
            />
          </div>

          <!-- Control Remoto -->
          <div class="form-group">
            <label class="form-label">N° Ref. Control Remoto:</label>
            <input 
              type="text" 
              v-model="televisor.numeroReferenciaControles" 
              class="form-input"
              placeholder="N° Referencia del control"
              readonly
            />
          </div>

          <!-- TVBox -->
          <div class="form-group">
            <label class="form-label">N° Ref. TVBox:</label>
            <input 
              type="text" 
              v-model="televisor.numeroReferenciaTvBoxes" 
              class="form-input"
              placeholder="N° Referencia del TVBox"
              readonly
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
          :disabled="!televisorCargado"
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
      televisor: {
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: '',
        numeroReferenciaControles: '',
        numeroReferenciaTvBoxes: ''
      },
      televisorCargado: false,
      mensaje: '',
      mensajeTipo: ''
    }
  },
  methods: {
    async buscar() {
      try {
        // Validación: campo vacío
        if (!this.numeroReferenciaBuscar || String(this.numeroReferenciaBuscar).trim() === '') {
          this.mostrarMensaje('Por favor ingrese un código de televisor.', 'warning');
          return;
        }

        const numeroReferenciaBuscarInt = parseInt(this.numeroReferenciaBuscar);

        // Validar que sea un número válido
        if (isNaN(numeroReferenciaBuscarInt)) {
          this.mostrarMensaje('El código debe ser un número válido.', 'warning');
          return;
        }

        const response = await fetch(
          `http://localhost:8090/televisor/${numeroReferenciaBuscarInt}`,
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
          this.televisor.nombre = data.nombre || '';
          this.televisor.precio = data.precio ? data.precio.toFixed(2) : '';
          this.televisor.marca = data.marca || '';
          
          // Formatear fecha: yyyy-MM-dd HH:mm:ss
          if (data.fechaCreacion) {
            const fecha = new Date(data.fechaCreacion);
            const year = fecha.getFullYear();
            const month = String(fecha.getMonth() + 1).padStart(2, '0');
            const day = String(fecha.getDate()).padStart(2, '0');
            const hours = String(fecha.getHours()).padStart(2, '0');
            const minutes = String(fecha.getMinutes()).padStart(2, '0');
            const seconds = String(fecha.getSeconds()).padStart(2, '0');
            this.televisor.fechaCreacion = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
          } else {
            this.televisor.fechaCreacion = '';
          }
          
          // Manejo de controles remotos
          if (data.controles && Array.isArray(data.controles)) {
            const codigos = [];
            for (const control of data.controles) {
              if (control.numeroReferencia !== undefined && control.numeroReferencia !== null) {
                codigos.push(control.numeroReferencia.toString());
              }
            }
            this.televisor.numeroReferenciaControles = codigos.length > 0 
              ? codigos.join(', ') 
              : 'No hay controles asignados';
          } else {
            this.televisor.numeroReferenciaControles = 'No hay controles asignados';
          }

          // Manejo de TVBoxes
          if (data.tvboxNumeroReferencias && Array.isArray(data.tvboxNumeroReferencias)) {
            const codigosTvBoxes = data.tvboxNumeroReferencias.map(num => num.toString());
            this.televisor.numeroReferenciaTvBoxes = codigosTvBoxes.length > 0
              ? codigosTvBoxes.join(', ')
              : 'No hay TVBoxes asignados';
          } else {
            this.televisor.numeroReferenciaTvBoxes = 'No hay TVBoxes asignados';
          }
          
          // Habilitar botón eliminar
          this.televisorCargado = true;
        } else {
          // Limpiar formulario y deshabilitar botón eliminar
          this.limpiarFormulario();
          this.televisorCargado = false;
          
          const error = await response.text();
          this.mostrarMensaje(error, 'error');
        }
      } catch (error) {
        // Limpiar formulario y deshabilitar botón eliminar
        this.limpiarFormulario();
        this.televisorCargado = false;
        
        this.mostrarMensaje('Ocurrió un error: ' + error.message, 'warning');
      }
    },

    async eliminar() {
      if (!this.televisorCargado) {
        return;
      }

      try {
        const numeroReferenciaEliminarInt = parseInt(this.numeroReferenciaBuscar);

        // Desasignar TVBoxes si los hay
        if (this.televisor.numeroReferenciaTvBoxes && 
            this.televisor.numeroReferenciaTvBoxes !== 'No hay TVBoxes asignados') {
          
          const tvBoxCodigos = this.televisor.numeroReferenciaTvBoxes
            .split(',')
            .map(p => p.trim())
            .filter(p => !isNaN(parseInt(p)))
            .map(p => parseInt(p));

          for (const tvBoxCodigo of tvBoxCodigos) {
            try {
              const responseDesasignar = await fetch(
                `http://localhost:8090/televisor/tvbox/desasignar/${tvBoxCodigo}`,
                {
                  method: 'PUT',
                  headers: {
                    'Authorization': 'Basic ' + btoa('admin:admin')
                  }
                }
              );

              if (!responseDesasignar.ok) {
                this.mostrarMensaje(`No se pudo desasignar el TVBox ${tvBoxCodigo}.`, 'warning');
              }
            } catch (error) {
              console.error(`Error desasignando TVBox ${tvBoxCodigo}:`, error);
            }
          }
        }

        // Eliminar televisor
        const response = await fetch(
          `http://localhost:8090/televisor/${numeroReferenciaEliminarInt}`,
          {
            method: 'DELETE',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );

        if (response.ok) {
          this.mostrarMensaje('Televisor eliminado correctamente', 'success');
          
          // Limpiar todo el formulario incluyendo el código de búsqueda
          this.numeroReferenciaBuscar = '';
          this.limpiarFormulario();
          this.televisorCargado = false;
        } else {
          const error = await response.text();
          this.mostrarMensaje(error, 'error');
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error: ' + error.message, 'warning');
      }
    },

    limpiarFormulario() {
      this.televisor = {
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: '',
        numeroReferenciaControles: '',
        numeroReferenciaTvBoxes: ''
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