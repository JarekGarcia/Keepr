import { api } from "./AxiosService";
import { logger } from '../utils/Logger'
import { AppState } from "../AppState";
import { Keep } from "../models/Keep";

class KeepsService {
    async getKeeps() {
        const res = await api.get("api/keeps")
        AppState.keeps = res.data.map(keepPojo => new Keep(keepPojo))
        logger.log("this is the keeps in AS", AppState.keeps)

    }

    async getKeepById(keepId) {
        const res = await api.get(`api/keeps/${keepId}`)
        AppState.activeKeep = res.data
    }
}

export const keepsService = new KeepsService()