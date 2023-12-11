import { AppState } from "../AppState"
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

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        logger.log("this is the created vault!!", res.data)
    }
}

export const vaultsService = new VaultsService()