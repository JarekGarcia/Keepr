<template>
    <router-link :to="{ name: 'Vault', params: { vaultId: vaultsProp.id } }">
        <div class="vaults-bg mt-5 rounded" role="button">
            <div class="row">
                <div class="col-12 d-flex justify-content-between mt-2 text-white fw-bold">
                    <p class="m-0 mx-1 p-2 bg-dark1 rounded">
                        {{ vaultsProp.name }}</p>
                    <i v-if="vaultsProp.isPrivate == true" class="mdi mdi-lock bg-dark11 rounded align-items-center"></i>
                    <img class="profile-pic rounded-circle mx-2" :src="vaultsProp.creator.picture" :alt="vaultsProp.name"
                        :title="vaultsProp.creator.name">
                </div>
            </div>
        </div>
    </router-link>
</template>



<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, KeepAlive } from 'vue';
import { Keep } from '../models/Keep'
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { Vault } from '../models/Vault';

export default {
    props: { vaultsProp: { type: Vault, required: true } },
    setup(props) {

        return {
            vaultsCoverImg: computed(() => `url(${props.vaultsProp.img})`),

        }
    }
};
</script>


<style lang="scss" scoped>
.vaults-bg {
    background-image: v-bind(vaultsCoverImg);
    height: 20vh;
    background-position: center;
    object-fit: cover;
}

.profile-pic {
    height: 3vh;
}

.mt-vault {
    margin-top: 16vh;
}

.bg-dark1 {
    background-color: rgba(0, 0, 0, 0.503);
}

.bg-dark11 {
    background-color: rgba(0, 0, 0, 0.503);
    height: 2vh;
}
</style>