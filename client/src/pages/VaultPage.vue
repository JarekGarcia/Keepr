<template>
    <div class="container-fluid">
        <section class="row justify-content-center">
            <div class="col-4 vault-bg text-center rounded mt-5">
                <div class="vault-index text-white bg-dark1 rounded">
                    <p class="fw-bold fs-1">{{ vault.name }}</p>
                    <p @click="goToProfilePage(vault.creatorId)" role="button">by: {{ vault.creator?.name }}</p>
                </div>
            </div>
        </section>
        <section class="row">
            <div class="col-12 text-center mt-1">
                <button @click="deleteVault(vault.id)" class="btn btn-danger fw-bold">Delete VAULT</button>
            </div>
        </section>
        <section class="row">
            <div v-for="keep in vaultKeeps" :key="keep.id" class="col-12 col-md-3 mt-5">
                <button @click="removeKeepFromVault(keep.vaultKeepId)" class="btn btn-danger fw-bold">remove from
                    vault</button>
                <KeepsCard :keepsProp="keep" />
            </div>
        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { useRoute, useRouter } from 'vue-router';
import { vaultsService } from '../services/VaultsService'
import KeepsCard from '../components/KeepsCard.vue';
export default {
    setup() {
        onMounted(() => {
            getVaultById();
            getKeepsByVaultId()
        });
        const route = useRoute();
        const router = useRouter()

        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId;
                await vaultsService.getVaultById(vaultId);
            }
            catch (error) {
                Pop.error(error);
                if (error.response.data.includes('This vault is Private')) {
                    router.push({ name: 'Home' })
                    Pop.error("That is a private vault!")
                }
            }
        }

        async function getKeepsByVaultId() {
            try {
                const vaultId = route.params.vaultId
                await vaultsService.getKeepsByVaultId(vaultId)
            } catch (error) {
                Pop.error(error)
            }
        }
        return {
            vault: computed(() => AppState.activeVault),
            vaultCoverImg: computed(() => `url(${AppState.activeVault.img})`),
            vaultKeeps: computed(() => AppState.vaultKeeps),

            //NOTE wanna make a push to profile page from vault page
            async goToProfilePage(profileId) {
                try {
                    return profileId
                } catch (error) {
                    Pop.error
                }
            },

            async removeKeepFromVault(vaultKeepId) {
                try {
                    const yes = await Pop.confirm("are you sure you want to remove this keep?")
                    if (!yes) {
                        return
                    }
                    await vaultsService.removeKeepFromVault(vaultKeepId)
                    Pop.success("keep removed!")
                } catch (error) {
                    Pop.error(error)
                }
            },

            async deleteVault(vaultId) {
                try {
                    const yes = await Pop.confirm("are you sure you wanna delete this Vault?")
                    if (!yes) {
                        return
                    }
                    await vaultsService.deleteVault(vaultId)
                    Pop.success("vault has been deleted!")
                } catch (error) {
                    Pop.error(error)
                }
            }
        };
    },
    components: { KeepsCard }
};
</script>


<style lang="scss" scoped>
.vault-bg {
    background-image: v-bind(vaultCoverImg);
    height: 20vh;
    width: 30vw;
    object-fit: cover;
    background-position: center;
    background-size: cover;
}

.vault-index {
    margin-top: 10vh;
}

.bg-dark1 {
    background-color: rgba(0, 0, 0, 0.503);
}
</style>