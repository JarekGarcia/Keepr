<template>
  <div class="container">
    <section class="row">
      <div @click="getUserVaults()" v-for="keep in keeps" :key="keep.id" class="col-12 col-md-3" data-bs-toggle="modal"
        data-bs-target="#keepsDetailsModal" role="button">
        <KeepsCard :keepsProp="keep" />
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import KeepsCard from '../components/KeepsCard.vue';
import { AppState } from '../AppState';
import { profilesService } from '../services/ProfilesService';

export default {
  setup() {
    onMounted(() => {
      getKeeps();
    });
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps),

      async getUserVaults() {
        try {
          const profileId = AppState.account.id
          await profilesService.getUserVaults(profileId)
        } catch (error) {
          Pop.error(error)
        }
      }

    };
  },
  components: { KeepsCard }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
