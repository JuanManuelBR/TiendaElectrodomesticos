<template>
  <div class="container">
    <div class="list-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Listar TV Box</h1>
        <button @click="navigateTo('/App')" class="btn-close">✕</button>
      </div>

      <!-- Tabla de datos -->
      <div class="table-container">
        <table class="data-table" v-if="tvBoxes.length > 0">
          <thead>
            <tr>
              <th>N° Referencia</th>
              <th>Nombre</th>
              <th>Marca</th>
              <th>Fecha Creación</th>
              <th>Precio</th>
              <th>Televisor N° Referencia</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(tvBox, index) in tvBoxes" :key="index">
              <td>{{ tvBox.numeroReferencia }}</td>
              <td>{{ tvBox.nombre }}</td>
              <td>{{ tvBox.marca }}</td>
              <td>{{ tvBox.fechaCreacion }}</td>
              <td>{{ formatPrecio(tvBox.precio) }}</td>
              <td>{{ tvBox.televisorNumeroReferencia }}</td>
            </tr>
          </tbody>
        </table>

        <!-- Mensaje cuando no hay datos -->
        <div v-else class="empty-state">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <rect x="3" y="8" width="18" height="12" rx="2" />
            <circle cx="7" cy="14" r="1" fill="currentColor" />
            <circle cx="12" cy="14" r="1" fill="currentColor" />
            <circle cx="17" cy="14" r="1" fill="currentColor" />
            <path d="M8 8v-2a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" />
          </svg>
          <p>No hay TV Boxes para mostrar</p>
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
      tvBoxes: [],
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
        
        // =============================
        // 🔗 OBTENER TODOS LOS TVBOX
        // =============================
        const response = await fetch('http://localhost:8090/televisor/tvbox/all', {
          method: 'GET',
          headers: {
            'Authorization': 'Basic ' + btoa('admin:admin')
          }
        });

        if (response.ok) {
          const data = await response.json();
          
          // Limpiar array antes de cargar (igual que C#)
          this.tvBoxes = [];
          
          // =============================
          // 📝 PROCESAR DATOS (IGUAL QUE C#)
          // =============================
          this.tvBoxes = data.map(tvbox => {
            const numeroReferencia = tvbox.numeroReferencia;
            const nombre = tvbox.nombre;
            const marca = tvbox.marca;
            const fecha = this.formatFecha(tvbox.fechaCreacion);
            const precio = tvbox.precio;
            
            // Manejar televisorNumeroReferencia opcional (igual que en C#)
            let televisorNumeroReferencia = '';
            if (tvbox.televisorNumeroReferencia !== undefined && 
                tvbox.televisorNumeroReferencia !== null) {
              televisorNumeroReferencia = tvbox.televisorNumeroReferencia.toString();
            }
            
            return {
              numeroReferencia,
              nombre,
              marca,
              fechaCreacion: fecha,
              precio,
              televisorNumeroReferencia
            };
          });

          if (this.tvBoxes.length === 0) {
            this.mostrarMensaje('No se encontraron TV Boxes', 'info');
          }
        } else {
          const error = await response.text();
          this.mostrarMensaje('Error: ' + error, 'error');
          this.tvBoxes = [];
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error: ' + error.message, 'error');
        this.tvBoxes = [];
      } finally {
        this.cargando = false;
      }
    },

    formatFecha(fechaISO) {
      if (!fechaISO) return '';
      
      // Formato igual que C#: yyyy-MM-dd HH:mm:ss
      const fecha = new Date(fechaISO);
      const year = fecha.getFullYear();
      const month = String(fecha.getMonth() + 1).padStart(2, '0');
      const day = String(fecha.getDate()).padStart(2, '0');
      const hours = String(fecha.getHours()).padStart(2, '0');
      const minutes = String(fecha.getMinutes()).padStart(2, '0');
      const seconds = String(fecha.getSeconds()).padStart(2, '0');
      
      return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
    },

    formatPrecio(precio) {
      return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        minimumFractionDigits: 2
      }).format(precio);
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

/* Ancho especial para columna fechaCreacion (igual que C#) */
.data-table th:nth-child(4),
.data-table td:nth-child(4) {
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