import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function LightingVehicleServices () {
  const getLightingVehicle = (callback) => {
    axiosPrivate.get('/VehiculosAlumbrado').then((response) => {
      callback(response.data)
    })
  }

  const getLightingVehicleById = (vehiculoAlumbradoId, callback) => {
    axiosPrivate
      .get(`/VehiculosAlumbrado/${vehiculoAlumbradoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createLightingVehicle = (data, callback) => {
    axiosPrivate.post('/VehiculosAlumbrado', data).then((response) => {
      callback(response.data)
    })
  }

  const updateLightingVehicle = (data, callback) => {
    axiosPrivate.put(`/VehiculosAlumbrado/${data.vehiculoAlumbradoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteLightingVehicle = (vehiculoAlumbradoId, callback) => {
    axiosPrivate.delete(`/VehiculosAlumbrado/${vehiculoAlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getLightingVehicle,
    getLightingVehicleById,
    createLightingVehicle,
    updateLightingVehicle,
    deleteLightingVehicle
  }
}
