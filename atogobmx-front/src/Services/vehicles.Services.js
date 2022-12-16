import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function VehiclesServices () {
  const getVehicles = (callback) => {
    axiosPrivate.get('/PAT_Vehiculos').then((response) => {
      callback(response.data)
    })
  }
  const getVehicle = (vehiculoId, callback) => {
    axiosPrivate.get(`/PAT_Vehiculos/${vehiculoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createVehicle = (data, callback) => {
    axiosPrivate.post('/PAT_Vehiculos', data).then((response) => {
      callback(response.data)
    })
  }
  const updateVehicle = (data, callback) => {
    axiosPrivate.put(`/PAT_Vehiculos/${data.vehiculoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteVehicle = (vehiculoId, callback) => {
    axiosPrivate.delete(`/PAT_EstatusVehiculo/${vehiculoId}`).then((response) => {
      callback(response.data)
    })
  }
  // Vehicle status
  const getEstatusVehicles = (callback) => {
    axiosPrivate.get('/PAT_EstatusVehiculo').then((response) => {
      callback(response.data)
    })
  }
  const getEstatusVehicle = (estatusVehiculoId, callback) => {
    axiosPrivate.get(`/PAT_EstatusVehiculo/${estatusVehiculoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createEstatusVehicle = (data, callback) => {
    axiosPrivate.post('/PAT_EstatusVehiculo', data).then((response) => {
      callback(response.data)
    })
  }
  const updateEstatusVehicle = (data, callback) => {
    axiosPrivate.put(`/PAT_EstatusVehiculo/${data.estatusVehiculoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteEstatusVehicle = (estatusVehiculoId, callback) => {
    axiosPrivate.delete(`/PAT_EstatusVehiculo/${estatusVehiculoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getVehicles,
    getVehicle,
    createVehicle,
    updateVehicle,
    deleteVehicle,
    // ESTATUSVEHICULOS
    getEstatusVehicles,
    getEstatusVehicle,
    createEstatusVehicle,
    updateEstatusVehicle,
    deleteEstatusVehicle
  }
}
