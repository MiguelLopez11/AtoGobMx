import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ExpedientLighting () {
  const getExpedientLighting = (callback) => {
    axiosPrivate.get('/ExpedienteAlumbrado').then((response) => {
      callback(response.data)
    })
  }

  const getExpedientLightingByAlumbradoId = (AlumbradoId, callback) => {
    axiosPrivate.get(`/ExpedienteDigital/Alumbrado/${AlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }

  const getExpedientLightingById = (expedienteAlumbradoId, callback) => {
    axiosPrivate.get(`/ExpedienteAlumbrado/${expedienteAlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }

  const createExpedientLighting = (data, callback) => {
    axiosPrivate.post('/ExpedienteAlumbrado', data).then((response) => {
      callback(response.data)
    })
  }

  const updatExpedientLighting = (data, callback) => {
    axiosPrivate.put(`/ExpedienteAlumbrado/${data.expedienteAlumbradoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteExpedientLighting = (expedienteAlumbradoId, callback) => {
    axiosPrivate.delete(`/ExpedienteAlumbrado/${expedienteAlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getExpedientLighting,
    getExpedientLightingById,
    // getDocumentsDowloadExpedient,
    createExpedientLighting,
    updatExpedientLighting,
    getExpedientLightingByAlumbradoId,
    deleteExpedientLighting
  }
}
