<template>
    <div class="container-fluid">
        <section class="row justify-content-center">
            <div class="col-6 d-flex mt-5">
                <img class="cover-img img-fluid rounded"
                    src="https://images.unsplash.com/photo-1682685797277-f2bf89b24017?q=80&w=2670&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                    alt="">
            </div>
        </section>
        <section class="row justify-content-center">
            <div class="col-2 text-center mt-1">
                <img class="rounded-circle" :src="profile.picture" alt="">
            </div>
        </section>
        <section class="row justify-content-center">
            <div class="col-4 text-center">
                <p class="fw-bold fs-2">{{ profile.name }}</p>
            </div>
        </section>
        <section class="row justify-content-center">
            <div class="col-2 text-end">
                <p>0 Vaults</p>
            </div>
            <div class="col-2">
                <p>0 Keeps</p>
            </div>
        </section>
        <section class="row">
            <div class="col-6">
                <p class="fw-bold fs-1">Vaults:</p>
            </div>
        </section>
        <section class="row">
            <div v-for="vault in vaults" :key="vault.id" class="col-2">
                <VaultsCard :vaultsProp="vault" />
            </div>
        </section>
        <section class="row">
            <p class="fw-bold fs-1 mt-5">Keeps:</p>
        </section>
        <section class="row">
            <div v-for="keep in keeps" :key="keep.id" class="col-2">
                <KeepsCard :keepsProp="keep" />
            </div>
        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { useRoute } from 'vue-router';
import { profilesService } from '../services/ProfilesService'
import VaultsCard from '../components/VaultsCard.vue';
import KeepsCard from '../components/KeepsCard.vue';
export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            getUserProfile();
            getUserVaults();
            getUserKeeps();
        });
        async function getUserProfile() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getUsersProfile(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getUserVaults() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getUserVaults(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getUserKeeps() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getUserKeeps(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        return {
            profile: computed(() => AppState.activeProfile),
            vaults: computed(() => AppState.activeProfileVaults),
            keeps: computed(() => AppState.activeProfileKeeps)
        };
    },
    components: { VaultsCard, KeepsCard }
};
</script>


<style lang="scss" scoped>
.cover-img {
    background-size: cover;
    background-position: center;
    object-fit: center;
    height: 30vh;
    width: 100%;
}
</style>