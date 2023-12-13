<template>
    <div @click="getKeepById(keepsProp.id), getUserVaults()" class="keeps-bg rounded mb-5" data-bs-toggle="modal"
        data-bs-target="#keepsDetailsModal" role="button">
        <div class="row">
            <div class="col-12 d-flex justify-content-between mt-keep text-white fw-bold">
                <p class="m-0 mx-1 p-2 bg-dark1 rounded">
                    {{ keepsProp.name }}</p>
                <img class="profile-pic rounded-circle mx-2" :src="keepsProp.creator.picture" :alt="keepsProp.name"
                    :title="keepsProp.creator.name">
            </div>
        </div>
    </div>
</template>



<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, KeepAlive } from 'vue';
import { Keep } from '../models/Keep'
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { profilesService } from '../services/ProfilesService';

export default {
    props: { keepsProp: { type: Keep, required: true } },
    setup(props) {

        return {
            keepsCoverImg: computed(() => `url(${props.keepsProp.img})`),
            account: computed(() => AppState.account),

            async getKeepById(keepId) {
                try {
                    const keep = await keepsService.getKeepById(keepId)
                    return keep
                } catch (error) {
                    Pop.error(error)
                }
            },

            async getUserVaults() {
                try {
                    const profileId = AppState.account.id
                    await profilesService.getMyVaults(profileId)
                } catch (error) {
                    Pop.error(error)
                }
            }
        }


    }
};
</script>


<style lang="scss" scoped>
.keeps-bg {
    background-image: v-bind(keepsCoverImg);
    height: 20vh;
    background-position: center;
    object-fit: cover;
}

.profile-pic {
    height: 3vh;
}

.mt-keep {
    margin-top: 13vh;
}

.bg-dark1 {
    background-color: rgba(0, 0, 0, 0.503);
}
</style>