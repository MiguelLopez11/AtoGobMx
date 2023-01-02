import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PublicWorkVehicleServices () {
  const getPublicWorkVehicle = (callback) => {
    axiosPrivate.get('/OP_Vehiculos').then((response) => {
      callback(response.data)
    })
  }

  const getPublicWorkVehicleById = (OpVehiculoId, callback) => {
    axiosPrivate
      .get(`/OP_Vehiculos/${OpVehiculoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createPublicWorkVehicle = (data, callback) => {
    axiosPrivate.post('/OP_Vehiculos', data).then((response) => {
      callback(response.data)
    })
  }

  const updatePublicWorkVehicle = (data, callback) => {
    axiosPrivate.put(`/OP_Vehiculos/${data.OpVehiculoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deletePublicWorkVehicle = (OpVehiculoId, callback) => {
    axiosPrivate.delete(`/OP_Vehiculos/${OpVehiculoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getPublicWorkVehicle,
    getPublicWorkVehicleById,
    createPublicWorkVehicle,
    updatePublicWorkVehicle,
    deletePublicWorkVehicle
  }
}
