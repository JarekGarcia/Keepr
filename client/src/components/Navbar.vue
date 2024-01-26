<template>
  <nav class="navbar navbar-expand-sm navbar-dark bg-white px-3 mb-5">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center text-dark">
        <i class="mdi mdi-alpha-k-circle-outline  fs fw-bold text-center">EEPR</i>

      </div>
    </router-link>
    <div v-if="accountId" class="dropdown">
      <button class="btn btn-secondary dropdown-toggle fw-bold" type="button" id="dropdownMenuButton"
        data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
        Create
      </button>
      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
        <a data-bs-toggle="modal" data-bs-target="#keepsCreateModal" class="dropdown-item fw-bold" href="#">Keep</a>
        <a data-bs-toggle="modal" data-bs-target="#vaultsCreateModal" class="dropdown-item fw-bold" href="#">Vault</a>
      </div>
    </div>
    <router-link v-if="accountId" :to="{ name: 'MyProfilePage', params: { profileId: `${accountId}` } }">
      <div class="mx-3">
        <button class="btn btn-primary fw-bold">My Profile</button>
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">

      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { AppState } from '../AppState';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      },
      accountId: computed(() => AppState.account.id)
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }

  .fs {
    font-size: 3rem;

  }
}
</style>
