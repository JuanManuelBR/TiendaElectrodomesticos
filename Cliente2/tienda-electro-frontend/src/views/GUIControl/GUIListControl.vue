<template>
  <div class="container">
    <div class="list-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Listar Control Remoto</h1>
        <button @click="navigateTo('/App')" class="btn-close">✕</button>
      </div>

      <!-- Tabla de datos -->
      <div class="table-container">
        <table class="data-table" v-if="controles.length > 0">
          <thead>
            <tr>
              <th>N° Referencia</th>
              <th>Nombre</th>
              <th>Alcance (m)</th>
              <th>Marca</th>
              <th>Fecha de Venta</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(control, index) in controles" :key="index">
              <td>{{ control.numeroReferencia }}</td>
              <td>{{ control.nombre }}</td>
              <td>{{ formatNumero(control.alcance) }}</td>
              <td>{{ control.marca }}</td>
              <td>{{ control.fechaVenta }}</td>
            </tr>
          </tbody>
        </table>

        <!-- Mensaje cuando no hay datos -->
        <div v-else class="empty-state">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <rect x="7" y="2" width="10" height="20" rx="2"></rect>
            <circle cx="12" cy="7" r="1.2"></circle>
            <circle cx="12" cy="11" r="1.2"></circle>
            <circle cx="12" cy="15" r="1.2"></circle>
            <rect x="10.2" y="18.2" width="3.6" height="1.6" rx="0.8"></rect>
          </svg>
          <p>No hay controles remotos para mostrar</p>
          <p class="hint">Haz clic en "Listar" para cargar los datos</p>
        </div>
      </div>

      <!-- Botones -->
      <div class="button-group">
        <button @click="listar" class="btn btn-list" :disabled="cargando">
          <span v-if="!cargando">Listar</span>
          <span v-else>Cargando...</span>
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
      controles: [],
      mensaje: '',
      mensajeTipo: '',
      cargando: false
    }
  },
  methods: {
    async listar() {
      try {
        this.cargando = true;
        this.mensaje = '';
        
        const response = await fetch('http://localhost:8090/televisor/control', {
          method: 'GET',
          headers: {
            'Authorization': 'Basic ' + btoa('admin:admin')
          }
        });

        if (response.ok) {
          const responseText = await response.text();
          
          // Verificar que no esté vacío
          if (!responseText || responseText.trim() === '') {
            this.mostrarMensaje('No se encontraron controles remotos', 'info');
            this.controles = [];
            return;
          }

          const data = JSON.parse(responseText);
          
          // Procesar datos siguiendo la lógica del C#
          this.controles = data.map(control => {
            const numeroReferencia = control.numeroReferencia;
            const nombre = control.nombre ?? '';
            const marca = control.marca ?? '';
            const alcance = control.alcance ?? '';
            const fechaVenta = this.formatFecha(control.fechaVenta);

            return { 
              numeroReferencia, 
              nombre, 
              marca, 
              alcance, 
              fechaVenta 
            };
          });

          if (this.controles.length === 0) {
            this.mostrarMensaje('No se encontraron controles remotos', 'info');
          }
        } else {
          const error = await response.text();
          this.mostrarMensaje('Error: ' + error, 'error');
          this.controles = [];
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error: ' + error.message, 'error');
        this.controles = [];
      } finally {
        this.cargando = false;
      }
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

.list-card {
  background: DarkGray;
  padding: 30px;
  border-radius: 10px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.3);
  width: 150%;
  max-width: 800px;
  margin: 1 auto;
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

.table-container {
  background: #f5f5f5;
  border-radius: 8px;
  overflow: hidden;
  margin-bottom: 20px;
  min-height: 300px;
  max-height: 320px;
  overflow-y: auto;
}

.data-table {
  width: 100%;
  border-collapse: collapse;
}

.data-table thead {
  background: #4a5568;
  color: white;
  position: sticky;
  top: 0;
  z-index: 10;
}

.data-table th {
  padding: 12px;
  text-align: left;
  font-weight: 600;
  font-size: 0.95rem;
  border-bottom: 2px solid #2d3748;
}

/* Ancho especial para columna fechaVenta (igual que C#) */
.data-table th:nth-child(5),
.data-table td:nth-child(5) {
  min-width: 150px;
}

.data-table tbody tr {
  transition: background-color 0.2s;
}

.data-table tbody tr:nth-child(even) {
  background: #ffffff;
}

.data-table tbody tr:nth-child(odd) {
  background: #f7fafc;
}

.data-table tbody tr:hover {
  background: #e2e8f0;
}

.data-table td {
  padding: 12px;
  border-bottom: 1px solid #e2e8f0;
  color: #2d3748;
}

.empty-state {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 60px 20px;
  color: #718096;
}

.empty-state svg {
  width: 100px;
  height: 100px;
  margin-bottom: 20px;
  opacity: 0.5;
}

.empty-state p {
  font-size: 1.2rem;
  margin: 5px 0;
}

.empty-state .hint {
  font-size: 0.9rem;
  color: #a0aec0;
}

.button-group {
  display: flex;
  justify-content: space-between;
  gap: 20px;
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
  opacity: 0.6;
  cursor: not-allowed;
}

.btn-list {
  background: #ffff80;
  color: #000;
}

.btn-list:hover:not(:disabled) {
  background: #ffff66;
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(255, 255, 128, 0.4);
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

.info {
  background: #d1ecf1;
  color: #0c5460;
  border: 1px solid #bee5eb;
}

/* Scrollbar personalizado */
.table-container::-webkit-scrollbar {
  width: 8px;
}

.table-container::-webkit-scrollbar-track {
  background: #f1f1f1;
}

.table-container::-webkit-scrollbar-thumb {
  background: #888;
  border-radius: 4px;
}

.table-container::-webkit-scrollbar-thumb:hover {
  background: #555;
}

@media (max-width: 768px) {
  .list-card {
    padding: 20px;
  }

  .title {
    font-size: 1.5rem;
  }

  .table-container {
    overflow-x: auto;
  }

  .data-table {
    min-width: 700px;
  }

  .button-group {
    flex-direction: column;
  }

  .btn {
    width: 100%;
  }
}
</style>