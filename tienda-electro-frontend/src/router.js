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

// 📺 Televisor
import GUIAddTelevisor from './views/GUITelevisor/GUIAddTelevisor.vue'
import GUIDeleteTelevisor from './views/GUITelevisor/GUIDeleteTelevisor.vue'
import GUIEditTelevisor from './views/GUITelevisor/GUIEditTelevisor.vue'
import GUIFiltrarTelevisor from './views/GUITelevisor/GUIFiltrarTelevisor.vue'
import GUIListTelevisor from './views/GUITelevisor/GUIListTelevisor.vue'
import GUISearchTelevisor from './views/GUITelevisor/GUISearchTelevisor.vue'

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
    path: '/control/editar/:id',
    name: 'editarControl',
    component: GUIEditControl,
    props: true
  },
  {
    path: '/control/eliminar/:id',
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
    path: '/televisor/editar/:id',
    name: 'editarTelevisor',
    component: GUIEditTelevisor,
    props: true
  },
  {
    path: '/televisor/eliminar/:id',
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
    path: '/televisor/buscar',
    name: 'buscarTelevisor',
    component: GUISearchTelevisor
  }
]

// Crear router
const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
