import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {
    async getUsersProfile(profileId) {
        const res = await api.get(`api/profiles/${profileId}`)
        AppState.activeProfile = res.data
        logger.log(AppState.activeProfile)
    }

    async getUserVaults(profileId) {
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        AppState.activeProfileVaults = res.data.map(vault => new Vault(vault))
        logger.log(AppState.activeProfileVaults)
    }

    async getUserKeeps(profileId) {
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        AppState.activeProfileKeeps = res.data.map(keep => new Keep(keep))
    }

}
export const profilesService = new ProfilesService()