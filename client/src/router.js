import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard, authSettled } from '@bcwdev/auth0provider-client'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage'),
    beforeEnter: authSettled
  },
  {
    path: '/about',
    name: 'About',
    component: loadPage('AboutPage')
  },
  {
    path: '/account',
    name: 'Account',
    component: loadPage('AccountPage'),
    beforeEnter: authGuard
  },
  {
    path: '/api/vaults/:vaultId',
    name: 'Vault',
    component: loadPage('VaultPage'),
    beforeEnter: authSettled
  },
  {
    path: '/api/profiles/:profileId',
    name: 'ProfilePage',
    component: loadPage('ProfilePage'),
    beforeEnter: authSettled
  },
  {
    path: '/myProfile/:profileId',
    name: 'MyProfilePage',
    component: loadPage('MyProfilePage'),
    beforeEnter: authSettled
  }
]

export const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})
