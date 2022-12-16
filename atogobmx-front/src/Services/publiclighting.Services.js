import { axiosPrivate } from '@/common/axiosPrivate.js'
export default function publiclightingServices () {
  const getPublicLighting = (callback) => {
    axiosPrivate
      .get('/Alumbrado')
      .then((response) => {
        callback(response.data)
      })
  }
  const getPublicLightingExpedient = (callback) => {
    axiosPrivate.get('/Alumbrado/ServiciosSinExpediente').then((response) => {
      callback(response.data)
    })
  }
  const getPublicLightingById = (AlumbradoId, callback) => {
    axiosPrivate.get(`/Alumbrado/${AlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPublicLighting = (data, callback) => {
    axiosPrivate.post('/Alumbrado', data).then((response) => {
      callback(response.data)
    })
  }
  const updatePublicLighting = (data, callback) => {
    axiosPrivate.put(`/Alumbrado/${data.alumbradoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deletePublicLighting = (alumbradoId, callback) => {
    axiosPrivate.delete(`/Alumbrado/${alumbradoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPublicLighting,
    getPublicLightingExpedient,
    getPublicLightingById,
    createPublicLighting,
    updatePublicLighting,
    deletePublicLighting
  }
}
