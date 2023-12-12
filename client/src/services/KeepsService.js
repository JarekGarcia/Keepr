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
        logger.log(res.data)
    }

    async createKeep(keepData) {
        const res = await api.post(`api/keeps`, keepData)
        const newKeep = new Keep(res.data)
        AppState.keeps.push(newKeep)
        return newKeep
    }

    async deleteKeep(keepId) {
        const res = await api.delete(`api/keeps/${keepId}`)
        logger.log("keeps has been deleted", res.data)
        AppState.keeps = AppState.keeps.filter((keep) => keep.id != keepId)
    }
}

export const keepsService = new KeepsService()