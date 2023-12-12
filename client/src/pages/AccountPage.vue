<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>
  <form @submit.prevent="editAccount()" class="p-5">
    <div class="form-group">
      <label for="name">Name</label>
      <input v-model="editable.name" type="string" class="form-control" id="name" aria-describedby="name"
        placeholder="name...">
    </div>
    <div class="form-group">
      <label for="picture">Picture</label>
      <input v-model="editable.picture" type="Url" class="form-control" id="picture" placeholder="picture url...">
    </div>
    <div class="form-group">
      <label for="coverImg">Cover Image</label>
      <input v-model="editable.coverImg" type="Url" class="form-control" id="coverImg" placeholder="cover image url...">
    </div>
    <button type="submit" class="btn btn-primary">Submit</button>
  </form>
</template>

<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { accountService } from '../services/AccountService';
export default {
  setup() {
    const editable = ref({})
    return {
      account: computed(() => AppState.account),
      editable,

      async editAccount() {
        try {
          const accountData = editable.value
          await accountService.editAccount(accountData)
        } catch (error) {
          Pop.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
