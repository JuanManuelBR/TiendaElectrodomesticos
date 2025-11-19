import { createRouter, createWebHistory } from 'vue-router'

// 🏠 Principal
import PruebaConexion from './views/PruebaConexion.vue'

// 🎮 Control
import GUIAddControl from './views/GUIControl/GUIAddControl.vue'
import GUIDeleteControl from './views/GUIControl/GUIDeleteControl.vue'
import GUIEditControl from './views/GUIControl/GUIEditControl.vue'
import GUIFiltrarDisponibilidadControl from './views/GUIControl/GUIFiltrarDisponibilidadControl.vue'
import GUIFiltrarMarcaControl from './views/GUIControl/GUIFiltrarMarcaControl.vue'
import GUIListControl from './views/GUIControl/GUIListControl.vue'
import GUISearchControl from './views/GUIControl/GUISearchControl.vue'
import GUIControlMarcaAsignadosTV from './views/GUIControl/GUIControlMarcaAsignadosTV.vue'

// 📺 Televisor
import GUIAddTelevisor from './views/GUITelevisor/GUIAddTelevisor.vue'
import GUIDeleteTelevisor from './views/GUITelevisor/GUIDeleteTelevisor.vue'
import GUIEditTelevisor from './views/GUITelevisor/GUIEditTelevisor.vue'
import GUIFiltrarTelevisor from './views/GUITelevisor/GUIFiltrarTelevisor.vue'
import GUIListTelevisor from './views/GUITelevisor/GUIListTelevisor.vue'
import GUISearchTelevisor from './views/GUITelevisor/GUISearchTelevisor.vue'
import GUITelevisoresControlesMismaMarca from './views/GUITelevisor/GUITelevisoresControlesMismaMarca.vue'

// 📺 TvBox
import GUIAddTvBox from './views/GUITvBox/GUIAddTvBox.vue'
import GUIDeleteTvBox from './views/GUITvBox/GUIDeleteTvBox.vue'
import GUIEditTvBox from './views/GUITvBox/GUIEditTvBox.vue'
import GUIFiltrarTvBox from './views/GUITvBox/GUIFiltrarTvBox.vue'
import GUIListTvBox from './views/GUITvBox/GUIListTvBox.vue'
import GUISearchTvBox from './views/GUITvBox/GUISearchTvBox.vue'

// 🧭 Definir rutas
const routes = [
  // Página principal
  
  {
    path: '/pruebaConexion',
    name: 'pruebaConexion',
    component: PruebaConexion
  },

  // 🔹 Rutas de Control
  {
    path: '/control',
    name: 'listarControl',
    component: GUIListControl
  },
  {
    path: '/control/agregar',
    name: 'agregarControl',
    component: GUIAddControl
  },
  {
    path: '/control/editar/',
    name: 'editarControl',
    component: GUIEditControl,
    props: true
  },
  {
    path: '/control/eliminar/',
    name: 'eliminarControl',
    component: GUIDeleteControl,
    props: true
  },
  {
    path: '/control/filtrar-disponibilidad',
    name: 'filtrarDisponibilidadControl',
    component: GUIFiltrarDisponibilidadControl
  },
  {
    path: '/control/filtrar-marca',
    name: 'filtrarMarcaControl',
    component: GUIFiltrarMarcaControl
  },
  {
    path: '/control/buscar',
    name: 'buscarControl',
    component: GUISearchControl
  },
  {
    path: '/control/ControlMarcaAsignadosTV',
    name: 'GUIControlMarcaAsignadosTV',
    component: GUIControlMarcaAsignadosTV
  },
  
  // 🔹 Rutas de Televisor
  {
    path: '/televisor',
    name: 'listarTelevisor',
    component: GUIListTelevisor
  },
  {
    path: '/televisor/agregar',
    name: 'agregarTelevisor',
    component: GUIAddTelevisor
  },
  {
    path: '/televisor/editar/',
    name: 'editarTelevisor',
    component: GUIEditTelevisor,
    props: true
  },
  {
    path: '/televisor/eliminar/',
    name: 'eliminarTelevisor',
    component: GUIDeleteTelevisor,
    props: true
  },
  {
    path: '/televisor/filtrar',
    name: 'filtrarTelevisor',
    component: GUIFiltrarTelevisor
  },
  {
    path: '/televisor/TelevisoresControlesMismaMarca',
    name: 'TelevisoresControlesMismaMarca',
    component: GUITelevisoresControlesMismaMarca
  },
  {
    path: '/televisor/buscar',
    name: 'buscarTelevisor',
    component: GUISearchTelevisor
  },
  // 🔹 Rutas de TvBox
  
  {
    path: '/TvBox',
    name: 'listarTvBox',
    component: GUIListTvBox
  },
  {
    path: '/TvBox/agregar',
    name: 'agregarTvBox',
    component: GUIAddTvBox
  },
  {
    path: '/TvBox/editar/',
    name: 'editarTvBox',
    component: GUIEditTvBox,
    props: true
  },
  {
    path: '/TvBox/eliminar/',
    name: 'eliminarTvBox',
    component: GUIDeleteTvBox,
    props: true
  },
  {
    path: '/TvBox/filtrar',
    name: 'filtrarTvBox',
    component: GUIFiltrarTvBox
  },
  {
    path: '/TvBox/buscar',
    name: 'buscarTvBox',
    component: GUISearchTvBox
  }
]

// Crear router
const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
