<template>
  <div class="container">
    <div class="list-card">
      <!-- Encabezado -->
      <h1 class="title">Filtrar Controles Disponibles</h1>

      <!-- Tabla de datos -->
      <div class="table-container">
        <table class="data-table" v-if="controles.length > 0">
          <thead>
            <tr>
              <th>Código</th>
              <th>Nombre</th>
              <th>Alcance</th>
              <th>Marca</th>
              <th>Fecha de Venta</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(control, index) in controles" :key="index">
              <td>{{ control.codigo }}</td>
              <td>{{ control.nombre }}</td>
              <td>{{ control.alcance }}</td>
              <td>{{ control.marca }}</td>
              <td>{{ control.fechaVenta }}</td>
            </tr>
          </tbody>
        </table>

        <!-- Mensaje cuando no hay datos -->
        <div v-else class="empty-state">
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
          <p>No hay controles para mostrar</p>
          <p class="hint">Haz clic en "Filtrar" para cargar los controles disponibles</p>
        </div>
      </div>

      <!-- Botones -->
      <div class="button-group">
        <button @click="filtrar" class="btn btn-filter" :disabled="cargando">
          <span v-if="!cargando">Filtrar</span>
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
    async filtrar() {
      // Método equivalente a btnFiltrar_Click que llama a CargarControlesDisponibles
      await this.cargarControlesDisponibles();
    },

    async cargarControlesDisponibles() {
      try {
        this.cargando = true;
        this.mensaje = '';
        
        const response = await fetch(
          'http://localhost:8090/electrodomestico/control/disponibles',
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
            
            // Limpiar el grid primero (gridControles.Rows.Clear())
            this.controles = [];
            
            // Procesar datos siguiendo la lógica del C#
            this.controles = data.map(c => {
              const codigo = c.codigo;
              const nombre = c.nombre;
              const alcance = c.alcance;
              
              // Manejar marca (puede ser null o string)
              let marca = '';
              if (c.marca !== null && c.marca !== undefined && typeof c.marca === 'string') {
                marca = c.marca;
              }
              
              // Manejar fechaVenta (puede ser null o string)
              let fecha = '';
              if (c.fechaVenta !== null && c.fechaVenta !== undefined && typeof c.fechaVenta === 'string') {
                const fechaObj = new Date(c.fechaVenta);
                const year = fechaObj.getFullYear();
                const month = String(fechaObj.getMonth() + 1).padStart(2, '0');
                const day = String(fechaObj.getDate()).padStart(2, '0');
                const hours = String(fechaObj.getHours()).padStart(2, '0');
                const minutes = String(fechaObj.getMinutes()).padStart(2, '0');
                const seconds = String(fechaObj.getSeconds()).padStart(2, '0');
                fecha = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
              }
              
              return {
                codigo,
                nombre,
                alcance,
                marca,
                fechaVenta: fecha
              };
            });

            if (this.controles.length === 0) {
              this.mostrarMensaje('No hay controles disponibles', 'info');
            }
          } else {
            this.mostrarMensaje('No hay controles disponibles', 'info');
            this.controles = [];
          }
        } else {
          this.mostrarMensaje('No hay controles disponibles', 'info');
          this.controles = [];
        }
      } catch (error) {
        this.mostrarMensaje('Error al obtener controles disponibles: ' + error.message, 'error');
        this.controles = [];
      } finally {
        this.cargando = false;
      }
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

.list-card {
  background: DarkGray;
  padding: 30px;
  border-radius: 10px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.3);
  width: 150%;
  max-width: 700px;
  margin: 1 auto;
  box-sizing: border-box;
}

.title {
  font-size: 2rem;
  font-weight: bold;
  text-align: center;
  margin-bottom: 30px;
  color: #000;
}

.table-container {
  background: #f5f5f5;
  border-radius: 8px;
  overflow: hidden;
  margin-bottom: 20px;
  min-height: 100px;
  max-height: 200px;
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

/* Ancho especial para columna FechaVenta (igual que C#) */
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
  justify-content: center;
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

.btn-filter {
  background: #80ffff;
  color: #000;
}

.btn-filter:hover:not(:disabled) {
  background: #66cccc;
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(128, 255, 255, 0.4);
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

.warning {
  background: #fff3cd;
  color: #856404;
  border: 1px solid #ffeeba;
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
    max-width: 100%;
  }
}
</style>