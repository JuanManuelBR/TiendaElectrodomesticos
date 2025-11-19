<template>
  <div class="container">
    <div class="form-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Agregar Televisor</h1>
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
              v-model="televisor.numeroReferencia" 
              class="form-input"
              placeholder="Ingrese número de referencia"
            />
          </div>

          <!-- Nombre -->
          <div class="form-group">
            <label class="form-label">Nombre:</label>
            <input 
              type="text" 
              v-model="televisor.nombre" 
              class="form-input"
              placeholder="Ingrese nombre"
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
            />
          </div>

          <!-- Fecha Creación -->
          <div class="form-group">
            <label class="form-label">Fecha Creación:</label>
            <input 
              type="datetime-local" 
              v-model="televisor.fechaCreacion" 
              class="form-input"
            />
          </div>

          <!-- Controles Remotos (múltiples separados por comas) -->
          <div class="form-group">
            <label class="form-label">Controles Remotos:</label>
            <input 
              type="text" 
              v-model="televisor.codigosControles" 
              class="form-input"
              placeholder="Códigos separados por comas (ej: 1,2,3)"
            />
          </div>

          <!-- TVBoxes (múltiples separados por comas) -->
          <div class="form-group">
            <label class="form-label">TVBoxes:</label>
            <input 
              type="text" 
              v-model="televisor.tvBoxes" 
              class="form-input"
              placeholder="Números separados por comas (ej: 10,20,30)"
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
      televisor: {
        numeroReferencia: '',
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: this.getFechaActual(),
        codigosControles: '',
        tvBoxes: ''
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
        // =============================
        // 📺 VALIDAR DATOS DEL TELEVISOR
        // =============================
        if (!this.televisor.numeroReferencia || !this.televisor.nombre || 
            !this.televisor.precio || !this.televisor.marca) {
          this.mostrarMensaje('Por favor complete todos los campos obligatorios', 'error');
          return;
        }

        // =============================
        // 🎮 PROCESAR CÓDIGOS DE CONTROLES
        // =============================
        let codigosControles = [];
        if (this.televisor.codigosControles && this.televisor.codigosControles.trim() !== '') {
          const partes = this.televisor.codigosControles.split(',');
          for (const parte of partes) {
            const codigo = parseInt(parte.trim());
            if (isNaN(codigo)) {
              this.mostrarMensaje(`El valor '${parte}' no es un número válido.`, 'warning');
              return;
            }
            codigosControles.push(codigo);
          }
        }

        // =============================
        // 📦 PROCESAR NÚMEROS DE TVBOX
        // =============================
        let tvBoxes = [];
        if (this.televisor.tvBoxes && this.televisor.tvBoxes.trim() !== '') {
          const partesTv = this.televisor.tvBoxes.split(',');
          for (const parte of partesTv) {
            const tvBoxNum = parseInt(parte.trim());
            if (isNaN(tvBoxNum)) {
              this.mostrarMensaje(`El valor '${parte}' en TVBoxes no es un número válido.`, 'warning');
              return;
            }
            tvBoxes.push(tvBoxNum);
          }
        }

        // =============================
        // ✅ VALIDAR CONTROLES EXISTENTES
        // =============================
        for (const codigoControl of codigosControles) {
          const controlExiste = await this.verificarControlExiste(codigoControl);
          if (!controlExiste) {
            this.mostrarMensaje(`El control con código ${codigoControl} no existe.`, 'warning');
            return;
          }
        }

        // =============================
        // ✅ VALIDAR TVBOX EXISTENTES
        // =============================
        for (const tvBoxNum of tvBoxes) {
          const tvBoxExiste = await this.verificarTvBoxExiste(tvBoxNum);
          if (!tvBoxExiste) {
            this.mostrarMensaje(`El TVBox con número de referencia ${tvBoxNum} no existe.`, 'warning');
            return;
          }
        }

        // =============================
        // 🧱 CREAR TELEVISOR
        // =============================
        const fechaFormateada = new Date(this.televisor.fechaCreacion)
          .toISOString()
          .slice(0, 19);

        const body = {
          tipo: 'televisor',
          numeroReferencia: parseInt(this.televisor.numeroReferencia),
          nombre: this.televisor.nombre,
          marca: this.televisor.marca,
          fechaCreacion: fechaFormateada,
          precio: parseFloat(this.televisor.precio)
        };

        const response = await fetch('http://localhost:8090/televisor', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Basic ' + btoa('admin:admin')
          },
          body: JSON.stringify(body)
        });

        if (!response.ok) {
          const error = await response.text();
          this.mostrarMensaje('Error al crear el televisor: ' + error, 'error');
          return;
        }

        // =============================
        // 🎮 ASIGNAR CONTROLES
        // =============================
        for (const codigoControl of codigosControles) {
          const asignado = await this.asignarControl(codigoControl);
          if (!asignado) {
            // Si falla, eliminar el televisor creado
            await this.eliminarTelevisor(this.televisor.numeroReferencia);
            this.mostrarMensaje(`Error al asignar el control ${codigoControl}. Televisor eliminado.`, 'error');
            return;
          }
        }

        // =============================
        // 📦 ASIGNAR TVBOXES
        // =============================
        for (const tvBoxNum of tvBoxes) {
          const asignado = await this.asignarTvBox(tvBoxNum);
          if (!asignado) {
            // Si falla, eliminar el televisor creado
            await this.eliminarTelevisor(this.televisor.numeroReferencia);
            this.mostrarMensaje(`Error al asignar el TVBox ${tvBoxNum}. Televisor eliminado.`, 'error');
            return;
          }
        }

        // =============================
        // ✅ ÉXITO
        // =============================
        this.mostrarMensaje('Televisor creado y todos los controles y TVBoxes asignados correctamente.', 'success');
        setTimeout(() => {
          this.limpiarFormulario();
        }, 2000);

      } catch (error) {
        this.mostrarMensaje('Ocurrió un error inesperado: ' + error.message, 'error');
      }
    },

    async verificarControlExiste(codigoControl) {
      try {
        const response = await fetch(
          `http://localhost:8090/televisor/control/${codigoControl}`,
          {
            method: 'GET',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );
        return response.ok;
      } catch (error) {
        console.error('Error al verificar control:', error);
        return false;
      }
    },

    async verificarTvBoxExiste(tvBoxNum) {
      try {
        const response = await fetch(
          `http://localhost:8090/televisor/tvbox/numero/${tvBoxNum}`,
          {
            method: 'GET',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );
        return response.ok;
      } catch (error) {
        console.error('Error al verificar TVBox:', error);
        return false;
      }
    },

    async asignarControl(codigoControl) {
      try {
        const response = await fetch(
          `http://localhost:8090/televisor/asignarControl/${this.televisor.numeroReferencia}/${codigoControl}`,
          {
            method: 'PUT',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );
        return response.ok;
      } catch (error) {
        console.error('Error al asignar control:', error);
        return false;
      }
    },

    async asignarTvBox(tvBoxNum) {
      try {
        const response = await fetch(
          `http://localhost:8090/televisor/tvbox/asignarTelevisor/${tvBoxNum}/${this.televisor.numeroReferencia}`,
          {
            method: 'PUT',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );
        return response.ok;
      } catch (error) {
        console.error('Error al asignar TVBox:', error);
        return false;
      }
    },

    async eliminarTelevisor(numeroReferencia) {
      try {
        await fetch(
          `http://localhost:8090/televisor/${numeroReferencia}`,
          {
            method: 'DELETE',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );
      } catch (error) {
        console.error('Error al eliminar televisor:', error);
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
      this.televisor = {
        numeroReferencia: '',
        nombre: '',
        precio: '',
        marca: '',
        fechaCreacion: this.getFechaActual(),
        codigosControles: '',
        tvBoxes: ''
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