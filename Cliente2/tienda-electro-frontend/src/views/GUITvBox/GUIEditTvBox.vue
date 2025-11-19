<template>
  <div class="container">
    <div class="form-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Actualizar TV Box</h1>
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
              v-model="tvBox.nombre" 
              class="form-input"
              placeholder="Ingrese nombre"
              :disabled="!tvBoxCargado"
            />
          </div>

          <!-- Precio -->
          <div class="form-group">
            <label class="form-label">Precio:</label>
            <input 
              type="number" 
              step="0.01"
              v-model="tvBox.precio" 
              class="form-input"
              placeholder="Ingrese precio"
              :disabled="!tvBoxCargado"
            />
          </div>

          <!-- Marca -->
          <div class="form-group">
            <label class="form-label">Marca:</label>
            <input 
              type="text" 
              v-model="tvBox.marca" 
              class="form-input"
              placeholder="Ingrese marca"
              :disabled="!tvBoxCargado"
            />
          </div>

          <!-- Fecha Creación -->
          <div class="form-group">
            <label class="form-label">Fecha Creación:</label>
            <input 
              type="datetime-local" 
              v-model="tvBox.fechaCreacion" 
              class="form-input"
              :disabled="!tvBoxCargado"
            />
          </div>

          <!-- Televisor Asociado -->
          <div class="form-group">
            <label class="form-label">TV Asociado:</label>
            <input 
              type="number" 
              v-model="tvBox.televisorAsociado" 
              class="form-input"
              placeholder="N° referencia TV (vacío para desasignar)"
              :disabled="!tvBoxCargado"
            />
          </div>
          <div class="hint-text" v-if="tvBoxCargado">
            💡 Deje el campo vacío para desasignar el televisor. Ingrese un N° de referencia para asignar uno nuevo.
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
          :disabled="!tvBoxCargado"
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
      tvBox: {
        numeroReferencia: '',
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: '',
        televisorAsociado: '' // String, puede estar vacío
      },
      tvBoxCargado: false,
      mensaje: '',
      mensajeTipo: ''
    }
  },
  methods: {
    async buscar() {
      try {
        // =============================
        // 🔍 VALIDAR INPUT
        // =============================
        if (!this.codigoBuscar || String(this.codigoBuscar).trim() === '') {
          this.mostrarMensaje('Por favor ingrese un ID de TV Box.', 'warning');
          return;
        }

        const idBuscar = parseInt(this.codigoBuscar);
        if (isNaN(idBuscar)) {
          this.mostrarMensaje('El ID debe ser un número válido.', 'warning');
          return;
        }

        // =============================
        // 🔗 BUSCAR TVBOX
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

        if (response.ok) {
          const data = await response.json();
          
          // =============================
          // 📝 CARGAR DATOS EN EL FORMULARIO
          // =============================
          this.tvBox.numeroReferencia = data.numeroReferencia;
          this.tvBox.nombre = data.nombre;
          this.tvBox.precio = data.precio ? data.precio.toFixed(2) : '';
          this.tvBox.marca = data.marca;
          
          // Formatear fecha para datetime-local
          const fecha = new Date(data.fechaCreacion);
          const year = fecha.getFullYear();
          const month = String(fecha.getMonth() + 1).padStart(2, '0');
          const day = String(fecha.getDate()).padStart(2, '0');
          const hours = String(fecha.getHours()).padStart(2, '0');
          const minutes = String(fecha.getMinutes()).padStart(2, '0');
          this.tvBox.fechaCreacion = `${year}-${month}-${day}T${hours}:${minutes}`;
          
          // televisorNumeroReferencia es opcional
          if (data.televisorNumeroReferencia !== undefined && data.televisorNumeroReferencia !== null) {
            this.tvBox.televisorAsociado = data.televisorNumeroReferencia.toString();
          } else {
            this.tvBox.televisorAsociado = ''; // No tiene televisor asociado
          }
          
          this.tvBoxCargado = true;
          this.mostrarMensaje('TV Box cargado correctamente', 'success');
        } else {
          this.limpiarFormulario();
          const error = await response.text();
          this.mostrarMensaje('No se encontró el TV Box o hubo un error.\n' + error, 'error');
        }
      } catch (error) {
        this.limpiarFormulario();
        this.mostrarMensaje('Ocurrió un error al buscar: ' + error.message, 'error');
      }
    },

    async actualizar() {
      try {
        // =============================
        // ✅ VALIDACIONES
        // =============================
        if (!this.tvBox.nombre || !this.tvBox.precio || 
            !this.tvBox.marca || !this.tvBox.fechaCreacion) {
          this.mostrarMensaje('Por favor complete todos los campos', 'warning');
          return;
        }

        const numeroReferencia = parseInt(this.codigoBuscar);
        if (isNaN(numeroReferencia)) {
          this.mostrarMensaje('El número de referencia debe ser válido.', 'warning');
          return;
        }

        const nombre = this.tvBox.nombre;
        const marca = this.tvBox.marca;
        const precio = parseFloat(this.tvBox.precio);
        const fechaCreacion = new Date(this.tvBox.fechaCreacion);

        // =============================
        // 📺 VALIDAR TELEVISOR ASOCIADO
        // =============================
        let televisorAsociado = null;

        if (!this.tvBox.televisorAsociado || this.tvBox.televisorAsociado.toString().trim() === '') {
          // → Caso: el campo está vacío → desasignar
          televisorAsociado = null;
        } else {
          // → Caso: el usuario escribió algo
          const tvNuevo = parseInt(this.tvBox.televisorAsociado);
          if (isNaN(tvNuevo)) {
            this.mostrarMensaje('El número de televisor debe ser numérico.', 'warning');
            return;
          }

          // Validar si el televisor existe
          const responseCheck = await fetch(
            `http://localhost:8090/televisor/${tvNuevo}`,
            {
              method: 'GET',
              headers: {
                'Authorization': 'Basic ' + btoa('admin:admin')
              }
            }
          );

          if (!responseCheck.ok) {
            this.mostrarMensaje(`El televisor con número de referencia ${tvNuevo} no existe.`, 'error');
            return;
          }

          // → Televisor existe → asignarlo
          televisorAsociado = tvNuevo;
        }

        // =============================
        // 🧱 ARMAR PAYLOAD DE TVBOX
        // =============================
        const body = {
          id: 0,
          numeroReferencia: numeroReferencia,
          nombre: nombre,
          precio: precio,
          marca: marca,
          fechaCreacion: fechaCreacion.toISOString().slice(0, 19),
          televisorNumeroReferencia: televisorAsociado // ← aquí se asigna o desasigna
        };

        // =============================
        // 🔄 ENVIAR UPDATE AL BACKEND
        // =============================
        const response = await fetch(
          `http://localhost:8090/televisor/tvbox/referencia/${numeroReferencia}`,
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
          this.mostrarMensaje('TV Box actualizado correctamente.', 'success');
        } else {
          const error = await response.text();
          this.mostrarMensaje('Error al actualizar el TV Box:\n' + error, 'error');
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error al actualizar: ' + error.message, 'error');
      }
    },

    limpiarFormulario() {
      this.tvBox = {
        numeroReferencia: '',
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: '',
        televisorAsociado: ''
      };
      this.tvBoxCargado = false;
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
  max-width: 800px;
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

.hint-text {
  font-size: 0.9rem;
  color: #333;
  font-style: italic;
  margin-left: 160px;
  margin-top: -10px;
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

  .hint-text {
    margin-left: 0;
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