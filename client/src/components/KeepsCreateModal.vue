<template>
    <div class="modal" tabindex="-1" role="dialog" id="keepsCreateModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Create Keep</h5>
                    <button type="button" class="close btn btn" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createKeep()">
                        <div class="form-group">
                            <label for="Name">Name</label>
                            <input v-model="editable.name" type="name" class="form-control" id="name"
                                aria-describedby="name" placeholder="Name">
                        </div>
                        <div class="form-group mt-1">
                            <label for="description">Description</label>
                            <textarea v-model="editable.description" type="description" class="form-control"
                                id="description" placeholder="Description"></textarea>
                        </div>
                        <div class="form-group mt-1">
                            <label for="img">Image Url</label>
                            <input v-model="editable.img" type="img" class="form-control" id="img" aria-describedby="img"
                                placeholder="URL">
                        </div>
                        <button type="submit" class="btn btn-primary mt-1">Submit</button>
                    </form>
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
export default {
    setup() {
        const editable = ref({});
        return {
            editable,

            async createKeep() {
                try {
                    const keepData = editable.value;
                    const keep = await keepsService.createKeep(keepData);
                    Pop.success("Posted Keep!");
                    editable.value = {};
                    Modal.getOrCreateInstance("#keepsCreateModal").hide();
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>