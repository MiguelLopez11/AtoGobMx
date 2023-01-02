import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function SupplyVehicleServices () {
  const getSupplyVehicle = (callback) => {
    axiosPrivate.get('/PROV_Vehiculo').then((response) => {
      callback(response.data)
    })
  }

  const getSupplyVehicleById = (ProvVehiculoId, callback) => {
    axiosPrivate
      .get(`/PROV_Vehiculo/${ProvVehiculoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createSupplyVehicle = (data, callback) => {
    axiosPrivate.post('/PROV_Vehiculo', data).then((response) => {
      callback(response.data)
    })
  }

  const updateSupplyVehicle = (data, callback) => {
    axiosPrivate.put(`/PROV_Vehiculo/${data.ProvVehiculoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteSupplyVehicle = (ProvVehiculoId, callback) => {
    axiosPrivate.delete(`/PROV_Vehiculo/${ProvVehiculoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getSupplyVehicle,
    getSupplyVehicleById,
    createSupplyVehicle,
    updateSupplyVehicle,
    deleteSupplyVehicle
  }
}
