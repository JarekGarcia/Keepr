<template>
    <div class="modal" id="keepsDetailsModal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-body p-0">
                    <div class="container-fluid">
                        <section class="row">
                            <div class="col-6 p-0">
                                <img class="img-fluid rounded-start keep-img" :src="keep.img" alt="">
                            </div>
                            <div class="col-6">
                                <section class="row">
                                    <div class="col-12 d-flex justify-content-around">
                                        <p class="fw-bold"><i class="mdi mdi-eye-outline"></i>{{ keep.views }}</p>
                                        <p class="fw-bold"><i class="mdi mdi-safe"></i>{{ keep.kept }}</p>
                                    </div>
                                </section>
                                <section class="row">
                                    <div class="col-12 text-center">
                                        <p class="fw-bold">{{ keep.name }}</p>
                                        <p>{{ keep.description }}</p>
                                    </div>
                                </section>
                                <section class="row">
                                    <div class="col-6">
                                        <p class="fw-bold">form placeholder</p>
                                    </div>
                                    <div class="col-6 d-flex">
                                        <img class="rounded-circle profile-pic" :src="keep.creator.picture"
                                            :alt="keep.creator.name" :title="keep.creator.name">
                                        <p>{{ keep.creator.name }}</p>
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
import { computed, reactive, onMounted } from 'vue';
export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep)
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
    height: 3vh;
}
</style>