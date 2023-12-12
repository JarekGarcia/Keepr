import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
    async getVaultById(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}`)
        AppState.activeVault = res.data
        logger.log(AppState.activeVault)
    }

    async getKeepsByVaultId(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        AppState.vaultKeeps = res.data
        logger.log(res.data)
    }

    async saveKeepInVault(vaultId, keepId) {
        const keepVaultData = { vaultId, keepId }
        const res = await api.post('api/vaultkeeps', keepVaultData)
        logger.log("saved keep in vault", res.data)
    }

    async removeKeepFromVault(vaultKeepId) {
        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        logger.log(res.data)
    }

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        logger.log("this is the created vault!!", res.data)
        AppState.activeProfileVaults = res.data
    }

    async deleteVault(vaultId) {
        const res = await api.delete(`api/vaults/${vaultId}`)
        logger.log(res.data)
    }
}

export const vaultsService = new VaultsService()