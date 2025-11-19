<template>
  <div class="container">
    <div class="list-card">
      <!-- Encabezado con botón cerrar -->
      <div class="header">
        <h1 class="title">Filtrar Televisor</h1>
        <button @click="navigateTo('/App')" class="btn-close">✕</button>
      </div>

      <!-- Selector de marca -->
      <div class="filter-section">
        <label class="filter-label">Seleccione la marca:</label>
        <select v-model="marcaSeleccionada" class="filter-select">
          <option value="">-- Seleccione una marca --</option>
          <option v-for="(marca, index) in marcas" :key="index" :value="marca">
            {{ marca }}
          </option>
        </select>
      </div>

      <!-- Tabla de datos -->
      <div class="table-container">
        <table class="data-table" v-if="televisores.length > 0">
          <thead>
            <tr>
              <th>N° Referencia</th>
              <th>Nombre</th>
              <th>Precio</th>
              <th>Marca</th>
              <th>Fecha de Creación</th>
              <th>Controles</th>
              <th>TVBoxes</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(tv, index) in televisores" :key="index">
              <td>{{ tv.numeroReferencia }}</td>
              <td>{{ tv.nombre }}</td>
              <td>{{ tv.precio }}</td>
              <td>{{ tv.marca }}</td>
              <td>{{ tv.fechaCreacion }}</td>
              <td>{{ tv.codigosControles }}</td>
              <td>{{ tv.codigosTvBoxes }}</td>
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
          <p class="hint">Seleccione una marca y haga clic en "Filtrar"</p>
        </div>
      </div>

      <!-- Botones -->
      <div class="button-group">
        <button @click="filtrar" class="btn btn-filter" :disabled="cargando || !marcaSeleccionada">
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
      marcas: [],
      marcaSeleccionada: '',
      televisores: [],
      mensaje: '',
      mensajeTipo: '',
      cargando: false
    }
  },
  mounted() {
    this.cargarMarcas();
  },
  methods: {
    async cargarMarcas() {
      try {
        const response = await fetch('http://localhost:8090/televisor/marcas', {
          method: 'GET',
          headers: {
            'Authorization': 'Basic ' + btoa('admin:admin')
          }
        });

        if (response.ok) {
          const data = await response.json();
          this.marcas = data;
        } else {
          this.mostrarMensaje('Error al cargar marcas: ' + response.statusText, 'error');
        }
      } catch (error) {
        this.mostrarMensaje('Excepción al cargar marcas: ' + error.message, 'error');
      }
    },

    async filtrar() {
      if (!this.marcaSeleccionada) {
        this.mostrarMensaje('Seleccione una marca primero.', 'warning');
        return;
      }

      try {
        this.cargando = true;
        this.mensaje = '';
        
        const response = await fetch(
          `http://localhost:8090/televisor/filtrar/${this.marcaSeleccionada}`,
          {
            method: 'GET',
            headers: {
              'Authorization': 'Basic ' + btoa('admin:admin')
            }
          }
        );

        if (response.ok) {
          const data = await response.json();
          
          // Limpiar el grid primero (igual que C#)
          this.televisores = [];
          
          // Procesar datos siguiendo exactamente la lógica del C#
          this.televisores = data.map(tv => {
            const codigo = tv.numeroReferencia;
            const nombre = tv.nombre;
            const precio = tv.precio;
            const marca = tv.marca;
            const fecha = this.formatFecha(tv.fechaCreacion);
            
            // Controles (igual que en C#)
            let codigosControles = '';
            if (tv.controles && Array.isArray(tv.controles)) {
              const codigos = [];
              for (const c of tv.controles) {
                if (c.numeroReferencia !== undefined && c.numeroReferencia !== null) {
                  codigos.push(c.numeroReferencia.toString());
                }
              }
              codigosControles = codigos.join(', ');
            }
            
            // TVBoxes (igual que en C#)
            let codigosTvBoxes = '';
            if (tv.tvboxNumeroReferencias && Array.isArray(tv.tvboxNumeroReferencias)) {
              const boxNums = [];
              for (const b of tv.tvboxNumeroReferencias) {
                boxNums.push(b.toString());
              }
              codigosTvBoxes = boxNums.join(', ');
            }
            
            return {
              numeroReferencia: codigo,
              nombre: nombre,
              precio: precio,
              marca: marca,
              fechaCreacion: fecha,
              codigosControles: codigosControles,
              codigosTvBoxes: codigosTvBoxes || 'No hay TVBoxes'
            };
          });

          if (this.televisores.length === 0) {
            this.mostrarMensaje('No se encontraron televisores de esa marca.', 'info');
          }
        } else {
          this.mostrarMensaje('No se encontraron televisores de esa marca.', 'info');
          this.televisores = [];
        }
      } catch (error) {
        this.mostrarMensaje('Error al filtrar: ' + error.message, 'error');
        this.televisores = [];
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
  max-width: 1000px;
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

.filter-section {
  display: flex;
  align-items: center;
  gap: 15px;
  margin-bottom: 25px;
  padding: 15px;
  background: #d3d3d3;
  border-radius: 8px;
}

.filter-label {
  font-size: 1.1rem;
  font-weight: 500;
  color: #000;
  white-space: nowrap;
}

.filter-select {
  flex: 1;
  padding: 10px 15px;
  font-size: 1rem;
  border: 2px solid #999;
  border-radius: 5px;
  background: #f5f5f5;
  cursor: pointer;
  transition: all 0.3s;
}

.filter-select:focus {
  outline: none;
  border-color: #667eea;
  background: #fff;
}

.table-container {
  background: #f5f5f5;
  border-radius: 8px;
  overflow: hidden;
  margin-bottom: 20px;
  min-height: 100px;
  max-height: 400px;
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

  .filter-section {
    flex-direction: column;
    align-items: flex-start;
  }

  .filter-select {
    width: 100%;
  }

  .table-container {
    overflow-x: auto;
  }

  .data-table {
    min-width: 800px;
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