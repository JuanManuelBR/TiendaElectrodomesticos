<template>
  <div class="container">
    <div class="form-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Actualizar Televisor</h1>
        <button @click="navigateTo('/App')" class="btn-close">✕</button>
      </div>

      <!-- Búsqueda -->
      <div class="search-section">
        <label class="form-label">N° Referencia Buscar:</label>
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
              type="text" 
              v-model="televisor.fechaCreacion" 
              class="form-input"
              placeholder="Fecha de creación"
              readonly
            />
          </div>

          <!-- Controles Remotos -->
          <div class="form-group">
            <label class="form-label">Controles Remotos:</label>
            <input 
              type="text" 
              v-model="televisor.codigosControles" 
              class="form-input"
              placeholder="Ej: 1, 2, 3 (separados por comas)"
              :disabled="!televisorCargado"
            />
          </div>

          <!-- TVBoxes -->
          <div class="form-group">
            <label class="form-label">TVBoxes:</label>
            <input 
              type="text" 
              v-model="televisor.codigosTvBoxes" 
              class="form-input"
              placeholder="Ej: 1, 2, 3 (separados por comas)"
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

      <div class="hint-text" v-if="televisorCargado">
        💡 Ingrese los códigos separados por comas. Puede agregar o quitar controles y TVBoxes.
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
        numeroReferencia: '',
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: '',
        codigosControles: '',
        codigosTvBoxes: ''
      },
      controlesActuales: [],
      tvBoxesActuales: [],
      televisorCargado: false,
      mensaje: '',
      mensajeTipo: ''
    }
  },
  methods: {
    async buscar() {
      try {
        // Validación: campo vacío
        if (!this.codigoBuscar || String(this.codigoBuscar).trim() === '') {
          this.mostrarMensaje('Por favor ingrese un código de televisor.', 'warning');
          return;
        }

        const codigoBuscar = parseInt(this.codigoBuscar);

        // Validar que sea un número válido
        if (isNaN(codigoBuscar)) {
          this.mostrarMensaje('El código debe ser un número válido.', 'warning');
          return;
        }

        const response = await fetch(
          `http://localhost:8090/televisor/${codigoBuscar}`,
          {
            method: 'GET',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );

        if (response.ok) {
          const data = await response.json();
          
          // Cargar datos básicos
          this.televisor.numeroReferencia = data.numeroReferencia;
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
          
          // Leer lista de controles
          this.controlesActuales = [];
          if (data.controles && Array.isArray(data.controles)) {
            data.controles.forEach(control => {
              if (control.numeroReferencia !== null && control.numeroReferencia !== undefined) {
                this.controlesActuales.push(control.numeroReferencia);
              }
            });
          }
          
          // Mostrar controles separados por comas
          this.televisor.codigosControles = this.controlesActuales.length > 0
            ? this.controlesActuales.join(', ')
            : '';

          // Leer lista de TVBoxes
          this.tvBoxesActuales = [];
          if (data.tvboxNumeroReferencias && Array.isArray(data.tvboxNumeroReferencias)) {
            data.tvboxNumeroReferencias.forEach(num => {
              this.tvBoxesActuales.push(num);
            });
          }
          
          // Mostrar TVBoxes separados por comas
          this.televisor.codigosTvBoxes = this.tvBoxesActuales.length > 0
            ? this.tvBoxesActuales.join(', ')
            : '';
          
          this.televisorCargado = true;
        } else {
          this.limpiarFormulario();
          const error = await response.text();
          this.mostrarMensaje(error || 'Televisor no encontrado', 'error');
        }
      } catch (error) {
        this.limpiarFormulario();
        this.mostrarMensaje('Ocurrió un error: ' + error.message, 'warning');
      }
    },

    async actualizar() {
      try {
        // Validaciones
        if (!this.televisor.nombre || !this.televisor.precio || !this.televisor.marca) {
          this.mostrarMensaje('Por favor complete todos los campos', 'warning');
          return;
        }

        const codigoTv = parseInt(this.codigoBuscar);
        const nombreTv = this.televisor.nombre;
        const precioTv = parseFloat(this.televisor.precio);
        const marcaTv = this.televisor.marca;
        
        // Parsear la fecha desde el formato yyyy-MM-dd HH:mm:ss
        const fechaCreacion = new Date(this.televisor.fechaCreacion.replace(' ', 'T'));

        // 1. Actualizar datos del televisor
        const body = {
          tipo: 'televisor',
          numeroReferencia: codigoTv,
          nombre: nombreTv,
          marca: marcaTv,
          fechaCreacion: fechaCreacion.toISOString().slice(0, 19),
          precio: precioTv
        };

        const response = await fetch(
          `http://localhost:8090/televisor/${codigoTv}`,
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

        // 2. Manejo de controles
        const nuevosCodigos = [];
        if (this.televisor.codigosControles && this.televisor.codigosControles.trim() !== '') {
          const partes = this.televisor.codigosControles.split(',')
            .map(p => p.trim())
            .filter(p => p !== '' && !isNaN(parseInt(p)))
            .map(p => parseInt(p));
          nuevosCodigos.push(...partes);
        }

        const agregarControles = nuevosCodigos.filter(c => !this.controlesActuales.includes(c));
        const quitarControles = this.controlesActuales.filter(c => !nuevosCodigos.includes(c));

        // Asignar nuevos controles
        for (const codigoControl of agregarControles) {
          // Verificar que el control existe
          const responseCheck = await fetch(
            `http://localhost:8090/televisor/control/${codigoControl}`,
            {
              method: 'GET',
              headers: {
                'Authorization': 'Basic ' + btoa('admin:admin')
              }
            }
          );

          if (!responseCheck.ok) {
            continue;
          }

          // Asignar el control
          await fetch(
            `http://localhost:8090/televisor/asignarControl/${codigoTv}/${codigoControl}`,
            {
              method: 'PUT',
              headers: {
                'Authorization': 'Basic ' + btoa('admin:admin')
              }
            }
          );
        }

        // Desasignar controles eliminados
        for (const codigoControl of quitarControles) {
          await fetch(
            `http://localhost:8090/televisor/desasignarControl/${codigoTv}/${codigoControl}`,
            {
              method: 'PUT',
              headers: {
                'Authorization': 'Basic ' + btoa('admin:admin')
              }
            }
          );
        }

        // 3. Manejo de TVBoxes
        const nuevosTvBoxes = [];
        if (this.televisor.codigosTvBoxes && this.televisor.codigosTvBoxes.trim() !== '') {
          const partes = this.televisor.codigosTvBoxes.split(',')
            .map(p => p.trim())
            .filter(p => p !== '' && !isNaN(parseInt(p)))
            .map(p => parseInt(p));
          nuevosTvBoxes.push(...partes);
        }

        const agregarTvBoxes = nuevosTvBoxes.filter(t => !this.tvBoxesActuales.includes(t));
        const quitarTvBoxes = this.tvBoxesActuales.filter(t => !nuevosTvBoxes.includes(t));

        // Asignar nuevos TVBoxes
        for (const numeroTvBox of agregarTvBoxes) {
          await fetch(
            `http://localhost:8090/televisor/tvbox/asignarTelevisor/${numeroTvBox}/${codigoTv}`,
            {
              method: 'PUT',
              headers: {
                'Authorization': 'Basic ' + btoa('admin:admin')
              }
            }
          );
        }

        // Desasignar TVBoxes eliminados
        for (const numeroTvBox of quitarTvBoxes) {
          await fetch(
            `http://localhost:8090/televisor/tvbox/desasignar/${numeroTvBox}`,
            {
              method: 'PUT',
              headers: {
                'Authorization': 'Basic ' + btoa('admin:admin')
              }
            }
          );
        }

        this.mostrarMensaje('Televisor actualizado correctamente.', 'success');

        // Actualizar las listas actuales
        this.controlesActuales = nuevosCodigos;
        this.tvBoxesActuales = nuevosTvBoxes;
        
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error al actualizar: ' + error.message, 'warning');
      }
    },

    limpiarFormulario() {
      this.televisor = {
        numeroReferencia: '',
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: '',
        codigosControles: '',
        codigosTvBoxes: ''
      };
      this.controlesActuales = [];
      this.tvBoxesActuales = [];
      this.televisorCargado = false;
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
  margin-bottom: 15px;
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

.form-input:disabled,
.form-input:read-only {
  background: #d3d3d3;
  cursor: not-allowed;
  opacity: 0.7;
}

.form-input:focus:not(:disabled):not(:read-only) {
  outline: none;
  border-color: #667eea;
  background: #fff;
}

.hint-text {
  font-size: 0.9rem;
  color: #333;
  font-style: italic;
  margin-bottom: 15px;
  text-align: center;
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
  white-space: pre-line;
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