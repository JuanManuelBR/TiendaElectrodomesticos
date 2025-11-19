<template>
  <div class="container">
    <div class="list-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Listar Televisor</h1>
        <button @click="navigateTo('/App')" class="btn-close">✕</button>
      </div>

      <!-- Tabla de datos -->
      <div class="table-container">
        <table class="data-table" v-if="televisores.length > 0">
          <thead>
            <tr>
              <th>N° Referencia</th>
              <th>Nombre</th>
              <th>Marca</th>
              <th>Fecha Creación</th>
              <th>Precio</th>
              <th>Controles</th>
              <th>TVBoxes</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(tv, index) in televisores" :key="index">
              <td>{{ tv.numeroReferencia }}</td>
              <td>{{ tv.nombre }}</td>
              <td>{{ tv.marca }}</td>
              <td>{{ tv.fechaCreacion }}</td>
              <td>{{ formatPrecio(tv.precio) }}</td>
              <td>{{ tv.controles }}</td>
              <td>{{ tv.tvboxes }}</td>
            </tr>
          </tbody>
        </table>

        <!-- Mensaje cuando no hay datos -->
        <div v-else class="empty-state">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <rect x="2" y="7" width="20" height="15" rx="2" ry="2"></rect>
            <polyline points="17 2 12 7 7 2"></polyline>
          </svg>
          <p>No hay televisores para mostrar</p>
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
      televisores: [],
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
        
        const response = await fetch('http://localhost:8090/televisor', {
          method: 'GET',
          headers: {
            'Authorization': 'Basic ' + btoa('admin:admin')
          }
        });

        if (response.ok) {
          const data = await response.json();
          
          // Limpiar array antes de cargar
          this.televisores = [];
          
          // Procesar datos siguiendo la lógica del C#
          this.televisores = data.map(televisor => {
            const numeroReferencia = televisor.numeroReferencia;
            const nombre = televisor.nombre;
            const marca = televisor.marca;
            const fecha = this.formatFecha(televisor.fechaCreacion);
            const precio = televisor.precio;
            
            // Manejar controles igual que en C# (array de controles)
            let codigosControles = '';
            if (televisor.controles && Array.isArray(televisor.controles)) {
              const codigos = televisor.controles
                .map(control => {
                  if (control && control.numeroReferencia !== undefined) {
                    return control.numeroReferencia.toString();
                  }
                  return null;
                })
                .filter(codigo => codigo !== null);
              
              codigosControles = codigos.join(', ');
            }
            
            // Manejar TVBoxes igual que en C# (array de números de referencia)
            let codigosTvBoxes = '';
            if (televisor.tvboxNumeroReferencias && Array.isArray(televisor.tvboxNumeroReferencias)) {
              const boxNums = televisor.tvboxNumeroReferencias.map(b => b.toString());
              codigosTvBoxes = boxNums.join(', ');
            }
            
            return {
              numeroReferencia,
              nombre,
              marca,
              fechaCreacion: fecha,
              precio,
              controles: codigosControles,
              tvboxes: codigosTvBoxes || 'No hay TVBoxes asignados'
            };
          });

          if (this.televisores.length === 0) {
            this.mostrarMensaje('No se encontraron televisores', 'info');
          }
        } else {
          const error = await response.text();
          this.mostrarMensaje('Error: ' + error, 'error');
          this.televisores = [];
        }
      } catch (error) {
        this.mostrarMensaje('Ocurrió un error: ' + error.message, 'error');
        this.televisores = [];
      } finally {
        this.cargando = false;
      }
    },

    formatFecha(fechaISO) {
      if (!fechaISO) return '';
      
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
  width: 300%;
  max-width: 1400px;
  margin: 0 auto;
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
  max-height: 400px;
  overflow-y: auto;
  overflow-x: auto;
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
  text-align: center;
  font-weight: 600;
  font-size: 0.95rem;
  border-bottom: 2px solid #2d3748;
}

/* Ancho especial para columnas fechaCreacion y tvboxes (igual que C#) */
.data-table th:nth-child(4),
.data-table td:nth-child(4),
.data-table th:nth-child(7),
.data-table td:nth-child(7) {
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
  text-align: center;
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
  max-width: 300px;
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
  height: 8px;
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