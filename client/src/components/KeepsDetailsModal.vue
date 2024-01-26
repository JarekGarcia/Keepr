<template>
    <div class="modal" id="keepsDetailsModal" tabindex="-1" role="dialog">
        <div v-if="keep" class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-body p-0">
                    <div class="container-fluid">
                        <section class="row">
                            <div class="col-6 p-0">
                                <img class="img-fluid rounded-start keep-img" :src="keep.img" alt="">
                            </div>
                            <div class="col-6">
                                <section class="row mb-3">
                                    <div class="col-12 d-flex justify-content-around">
                                        <p class="fw-bold"><i class="mdi mdi-eye-outline"></i>{{ keep.views }}</p>
                                        <p class="fw-bold"><i class="mdi mdi-safe"></i>{{ keep.kept }}</p>
                                    </div>
                                </section>
                                <section class="row mb-3">
                                    <div class="col-12 text-center">
                                        <p class="fw-bold">{{ keep.name }}</p>
                                        <p>{{ keep.description }}</p>
                                    </div>
                                </section>
                                <section class="row mb-3">
                                    <div class="col-6">
                                        <div class="form-group">
                                            <div class="dropdown">
                                                <button class="btn btn-secondary dropdown-toggle" type="button"
                                                    id="dropdownMenuButton" data-bs-toggle="dropdown" aria-haspopup="true"
                                                    aria-expanded="false">
                                                    Save In A Vault?
                                                </button>
                                                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                    <button @click="saveKeepInVault(vault.id, keep.id)"
                                                        class="m-1 rounded dropdown-item" v-for="vault in vaults"
                                                        :key="vault.id">
                                                        {{ vault.name }}
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div role="button" @click="goToProfilePage(keep.creatorId)"
                                        class="col-6 d-flex justify-content-end align-items-center">
                                        <img class="rounded-circle profile-pic" :src="keep.creator?.picture"
                                            :alt="keep.creator?.name" :title="keep.creator?.name">
                                        <p class="fw-bold m-0">{{ keep.creator?.name }}</p>
                                    </div>
                                </section>
                                <section class="row mb-1">
                                    <div class="col-12 d-flex justify-content-center">
                                        <button @click="deleteKeep(keep.id)" v-if="account.id == keep.creatorId"
                                            class="btn btn-danger fw-bold">Delete
                                            Keep</button>
                                    </div>
                                </section>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { Modal } from 'bootstrap';
import { useRoute, useRouter } from 'vue-router';
import { vaultsService } from '../services/VaultsService';
import { profilesService } from '../services/ProfilesService';
import { logger } from '../utils/Logger';
export default {
    setup() {
        const editable = ref({})
        const route = useRoute()
        const router = useRouter()
        onMounted(() => {
        })

        async function getUserVaults() {
            try {
                const profileId = AppState.account.id
                await profilesService.getUserVaults(profileId)
            } catch (error) {
                Pop.error(error)
            }
        }
        return {
            editable,
            keep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),
            vaults: computed(() => AppState.activeProfileVaults),

            async deleteKeep(keepId) {
                try {
                    const yes = await Pop.confirm("Are you sure you want to Delete this Keep?")
                    if (!yes) {
                        return
                    }
                    await keepsService.deleteKeep(keepId)
                    Pop.success("Keep has been Deleted!")
                    Modal.getOrCreateInstance("#keepsDetailsModal").hide()
                } catch (error) {
                    Pop.error(error)
                }
            },

            async getVaultById(vaultId) {
                try {
                    await vaultsService.getVaultById(vaultId)
                } catch (error) {
                    Pop.error(error)
                }
            },

            async goToProfilePage(profileId) {
                try {
                    await router.push(`/api/profiles/${profileId}`)
                    Modal.getOrCreateInstance('#keepsDetailsModal').hide()
                } catch (error) {
                    Pop.error(error)
                }
            },

            async saveKeepInVault(vaultId, keepId) {
                try {
                    await vaultsService.saveKeepInVault(vaultId, keepId)
                    Pop.success("Keep saved in your vault!")
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.keep-img {
    background-size: cover;
    background-position: center;
    object-fit: cover;
    height: 50vh;
    width: 100%;
}

.profile-pic {
    height: 4vh;
    width: 3vw;
}

.scroll {
    overflow-y: scroll;
    height: 10vh;
}
</style>