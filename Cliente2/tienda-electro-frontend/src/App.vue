<template>
  <div class="app-container">

    <!-- 🧭 Header -->
    <div class="header">
      <div class="header-top">
        <div class="title-bar">
          <span class="app-icon">GUI</span>
          <span class="app-title">Principal</span>
        </div>
      </div>

      <!-- 📂 Menu Bar -->
      <div class="menu-bar">
        <div class="menu-items">

          <!-- Archivo -->
          <div class="menu-dropdown">
            <button class="submenu-nested-item" @click="navigateTo('/App')">Archivo</button>
          </div>

          <!-- Producto -->
          <div class="menu-dropdown">
            <button 
              class="menu-item" 
              @click="toggleMenu('producto')"
              :class="{ active: activeMenu === 'producto' }"
            >
              Producto
            </button>

            <!-- Submenu Producto -->
            <div v-if="activeMenu === 'producto'" class="submenu">

              <!-- ========== TELEVISOR ========== -->
              <div class="submenu-item-group">
                <button 
                  class="submenu-item" 
                  @click="toggleSubmenu('televisor')"
                  :class="{ active: activeSubmenu === 'televisor' }"
                >
                  <span>📺 Televisor</span>
                  <span class="arrow">▶</span>
                </button>

                <div v-if="activeSubmenu === 'televisor'" class="submenu-nested">
                  <button class="submenu-nested-item add-btn" @click="navigateTo('/televisor/agregar')">
                    ➕ Add
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/televisor')">
                    📋 List
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/televisor/editar/')">
                    ✏️ Edit
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/televisor/buscar')">
                    🔍 Search
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/televisor/filtrar')">
                    🔎 Filtrar
                  </button>
                  <button class="submenu-nested-item delete-btn" @click="navigateTo('/televisor/eliminar/')">
                    🗑️ Delete
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/televisor/TelevisoresControlesMismaMarca')">
                    TelevisorControlMismaMarca
                  </button>
                </div>
              </div>

              <!-- ========== CONTROL ========== -->
              <div class="submenu-item-group">
                <button 
                  class="submenu-item" 
                  @click="toggleSubmenu('control')"
                  :class="{ active: activeSubmenu === 'control' }"
                >
                  <span>🎮 Control</span>
                  <span class="arrow">▶</span>
                </button>

                <div v-if="activeSubmenu === 'control'" class="submenu-nested">
                  <button class="submenu-nested-item add-btn" @click="navigateTo('/control/agregar')">
                    ➕ Add
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/control')">
                    📋 List
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/control/editar/')">
                    ✏️ Edit
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/control/buscar')">
                    🔍 Search
                  </button>
                  
                  <!-- Filtrar con subnivel -->
                  <div class="submenu-item-group-nested">
                    <button 
                      class="submenu-nested-item" 
                      @click="toggleSubmenuLevel2('filtrar')"
                      :class="{ active: activeSubmenuLevel2 === 'filtrar' }"
                    >
                      🔎 Filtrar <span class="arrow">▶</span>
                    </button>
                    <div v-if="activeSubmenuLevel2 === 'filtrar'" class="submenu-nested-level2">
                      <button class="submenu-nested-item" @click="navigateTo('/control/filtrar-marca')">Marca</button>
                      <button class="submenu-nested-item" @click="navigateTo('/control/filtrar-disponibilidad')">Disponibilidad</button>
                    </div>
                  </div>

                  <button class="submenu-nested-item delete-btn" @click="navigateTo('/control/eliminar/')">
                    🗑️ Delete
                  </button>

                  <button class="submenu-nested-item" @click="navigateTo('/control/ControlMarcaAsignadosTV')">
                    ControlMarcaAsignadosTV
                  </button>

                </div>
              </div>

              <!-- ========== TVBOX ========== -->
              <div class="submenu-item-group">
                <button 
                  class="submenu-item" 
                  @click="toggleSubmenu('tvbox')"
                  :class="{ active: activeSubmenu === 'tvbox' }"
                >
                  <span>📦 TvBox</span>
                  <span class="arrow">▶</span>
                </button>

                <div v-if="activeSubmenu === 'tvbox'" class="submenu-nested">
                  <button class="submenu-nested-item add-btn" @click="navigateTo('/tvbox/agregar')">
                    ➕ Add
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/tvbox')">
                    📋 List
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/tvbox/editar/')">
                    ✏️ Edit
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/tvbox/buscar')">
                    🔍 Search
                  </button>
                  <button class="submenu-nested-item" @click="navigateTo('/tvbox/filtrar')">
                    🔎 Filtrar
                  </button>
                  <button class="submenu-nested-item delete-btn" @click="navigateTo('/tvbox/eliminar/')">
                    🗑️ Delete
                  </button>
                </div>
              </div>

            </div>
          </div>

          <!-- Ayuda -->
          <div class="menu-dropdown">
            <button class="menu-item" @click="mostrarAyuda">Ayuda</button>
          </div>

        </div>
      </div>
    </div>

    <!-- 🖥️ Main Content -->
    <div class="main-content">
      <div class="store-background">
        <div class="welcome-card" v-if="shouldShowCard">
          <router-view></router-view>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'

const router = useRouter()
const route = useRoute()

const activeMenu = ref(null)
const activeSubmenu = ref(null)
const activeSubmenuLevel2 = ref(null)

const shouldShowCard = computed(() => route.path !== '/')

const toggleMenu = (menu) => {
  activeMenu.value = activeMenu.value === menu ? null : menu
  activeSubmenu.value = null
  activeSubmenuLevel2.value = null
}

const toggleSubmenu = (submenu) => {
  activeSubmenu.value = activeSubmenu.value === submenu ? null : submenu
  activeSubmenuLevel2.value = null
}

const toggleSubmenuLevel2 = (submenu) => {
  activeSubmenuLevel2.value = activeSubmenuLevel2.value === submenu ? null : submenu
}

const navigateTo = (path) => {
  router.push(path)
  activeMenu.value = null
  activeSubmenu.value = null
  activeSubmenuLevel2.value = null
}

const mostrarAyuda = () => {
  alert("Desarrollado por:\nJuan Manuel Blandón Ramirez\nMiguel Angel Murillo De Los Rios\nDavid Estiven Mendez Lara\nJaminton Julian Leyton Camacho")
  activeMenu.value = null
}
</script>

<style scoped>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  border-radius: 2px;
}

.app-container {
  min-height: 80vh;
  background: #f0f0f0;
  font-family: Arial, sans-serif;
  text-align: center;
  border-radius: 10px;
}

.header {
  background: linear-gradient(to bottom, #e8e8e8, #d0d0d0);
  border-bottom: 2px solid #888;
  box-shadow: 0 2px 5px rgba(0,0,0,0.1);
}

.header-top {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 8px 12px;
  background: DarkGray;
}

.title-bar {
  display: flex;
  align-items: center;
  gap: 8px;
}

.app-icon {
  background: #cc0000;
  color: white;
  padding: 4px 10px;
  border-radius: 3px;
  font-weight: bold;
  font-size: 12px;
}

.app-title {
  font-weight: 600;
  color: #333;
}

.menu-bar {
  display: flex;
  justify-content: space-between;
  background: white;
  border-top: 1px solid #ccc;
  padding: 0 8px;
}

.menu-items {
  display: flex;
  position: relative;
}

.menu-dropdown {
  position: relative;
}

.menu-item {
  padding: 8px 16px;
  border: none;
  background: transparent;
  cursor: pointer;
  font-weight: 500;
  color: #333;
}

.menu-item:hover,
.menu-item.active {
  background: #e8f4ff;
}

.submenu {
  position: absolute;
  top: 100%;
  left: 0;
  background: white;
  border: 1px solid #ccc;
  box-shadow: 3px 3px 10px rgba(0,0,0,0.2);
  min-width: 200px;
  z-index: 1000;
}

.submenu-item-group {
  position: relative;
}

.submenu-item-group-nested {
  position: relative;
}

.submenu-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
  padding: 10px 16px;
  border: none;
  background: white;
  text-align: left;
  cursor: pointer;
  font-size: 14px;
}

.submenu-item:hover,
.submenu-item.active {
  background: #e8f4ff;
}

.arrow {
  font-size: 10px;
  margin-left: 8px;
}

.submenu-nested {
  position: absolute;
  top: 0;
  left: 100%;
  background: white;
  border: 1px solid #ccc;
  box-shadow: 3px 3px 10px rgba(0,0,0,0.2);
  min-width: 180px;
  z-index: 1001;
}

.submenu-nested-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
  padding: 10px 16px;
  border: none;
  background: white;
  text-align: left;
  cursor: pointer;
  font-size: 14px;
  transition: all 0.2s;
}

.submenu-nested-item:hover {
  background: #f5f5f5;
}

.submenu-nested-item.add-btn {
  background: #f5f5f5;
  font-weight: 600;
}

.submenu-nested-item.add-btn:hover {
  background: rgba(76, 175, 80, 0.3);
}

.submenu-nested-item.delete-btn {
  background: #f5f5f5;
  font-weight: 600;
}

.submenu-nested-item.delete-btn:hover {
  background: #e9131365;
}

.submenu-nested-level2 {
  position: absolute;
  top: 0;
  left: 100%;
  background: white;
  border: 1px solid #ccc;
  box-shadow: 3px 3px 10px rgba(0,0,0,0.2);
  min-width: 180px;
  z-index: 1002;
}

.main-content {
  height: calc(100vh - 100px);
  position: relative;
  overflow: hidden;
}

.store-background {
  width: 100%;
  height: 100%;
  background: url('/ImagenPrincipal.png') center/cover;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.store-background::before {
  content: '';
  position: absolute;
  inset: 0;
  background: rgba(0, 0, 0, 0.3);
}

.welcome-card {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  padding: 50px;
  border-radius: 12px;
  text-align: center;
  z-index: 20;
  max-width: 600px;
}
</style>