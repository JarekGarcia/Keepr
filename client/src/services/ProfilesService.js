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
        logger.log(res.data)
        AppState.activeProfileVaults = res.data
        logger.log("This is the users Vaults!", AppState.activeProfileVaults)
    }

    async getMyVaults(profileId) {
        const res = await api.get(`account/vaults`)
        AppState.activeProfileVaults = res.data
        logger.log(res.data)
    }

    async getUserKeeps(profileId) {
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        AppState.activeProfileKeeps = res.data.map(keep => new Keep(keep))
    }

}
export const profilesService = new ProfilesService()