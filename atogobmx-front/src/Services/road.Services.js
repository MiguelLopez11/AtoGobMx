import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function RoadService () {
  const getRoad = (callback) => {
    axiosPrivate.get('/Ruta').then((response) => {
      callback(response.data)
    })
  }

  const getRoadById = (rutaId, callback) => {
    axiosPrivate.get(`/Ruta/${rutaId}`).then((response) => {
      callback(response.data)
    })
  }

  const createRoad = (data, callback) => {
    axiosPrivate.post('/Ruta', data).then((response) => {
      callback(response.data)
    })
  }

  const updateRoad = (data, callback) => {
    axiosPrivate.put(`/Ruta/${data.rutaId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteRoad = (rutaId, callback) => {
    axiosPrivate.delete(`/Ruta/${rutaId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getRoad,
    getRoadById,
    createRoad,
    updateRoad,
    deleteRoad
  }
}
