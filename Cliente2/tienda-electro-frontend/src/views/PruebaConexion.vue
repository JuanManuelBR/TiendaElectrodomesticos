
<template >
  <div class="Color-Menu">
    <h2>Prueba de conexión con el backend</h2>
    <button @click="probarConexion">Probar conexión</button>

    <p v-if="respuesta">Respuesta del servidor: {{ respuesta }}</p>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

const respuesta = ref('')

const probarConexion = async () => {
  try {
    const res = await axios.get('http://localhost:8090/electrodomestico/healthCheck', {
      auth: { username: 'admin', password: 'admin' }
    })
    respuesta.value = res.data
  } catch (error) {
    console.error('Error al conectar con el backend:', error)
    respuesta.value = 'Error al conectar con el backend'
  }
}
</script>

<style>
button {
  margin-top: 10px;
  padding: 8px 15px;
  font-size: 16px;
  cursor: pointer;
}

.Color-Menu {
  position: relative;
  top: 50%;
  left: 50%;
  transform: translate(-50%, 10%);
  background: rgba(255, 255, 255, 0.95);
  padding: 50px;
  border-radius: 12px;
  box-shadow: 0 8px 20px rgba(0,0,0,0.3);
  text-align: center;
  z-index: 20;
  max-width: 600px;
}
</style>
