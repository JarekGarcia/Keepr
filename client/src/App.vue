<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer>
  </footer>

  <KeepsDetailsModal />
  <KeepsCreateModal />
  <VaultsCreateModal />
  <VaultKeepsDetailsModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import KeepsDetailsModal from './components/KeepsDetailsModal.vue'
import KeepsCreateModal from './components/KeepsCreateModal.vue'
import VaultsCreateModal from './components/VaultsCreateModal.vue'
import VaultKeepsDetailsModal from './components/VaultKeepsDetailsModal.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepsDetailsModal, KeepsCreateModal, VaultsCreateModal, VaultKeepsDetailsModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
