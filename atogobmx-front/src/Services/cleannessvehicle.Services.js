import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function CleannessVehicleServices () {
  const getCleannessVehicle = (callback) => {
    axiosPrivate.get('/AseoVehiculo').then((response) => {
      callback(response.data)
    })
  }

  const getCleannessVehicleById = (AseoVehiculoId, callback) => {
    axiosPrivate
      .get(`/AseoVehiculo/${AseoVehiculoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createCleannessVehicle = (data, callback) => {
    axiosPrivate.post('/AseoVehiculo', data).then((response) => {
      callback(response.data)
    })
  }

  const updateCleannessVehicle = (data, callback) => {
    axiosPrivate.put(`/AseoVehiculo/${data.AseoVehiculoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteCleannessVehicle = (AseoVehiculoId, callback) => {
    axiosPrivate.delete(`/AseoVehiculo/${AseoVehiculoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getCleannessVehicle,
    getCleannessVehicleById,
    createCleannessVehicle,
    updateCleannessVehicle,
    deleteCleannessVehicle
  }
}
