import axios from 'axios'

export default function VehiclesServices () {
  const BaseUrl = 'http://localhost:5000/api'
  const getVehicles = (callback) => {
    axios.get(`${BaseUrl}/PAT_Vehiculos`).then((response) => {
      callback(response.data)
    })
  }
  const getVehicle = (vehiculoId, callback) => {
    axios.get(`${BaseUrl}/PAT_Vehiculos/${vehiculoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createVehicle = (data, callback) => {
    axios.post(`${BaseUrl}/PAT_Vehiculos`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateVehicle = (data, callback) => {
    axios.put(`${BaseUrl}/PAT_Vehiculos/${data.vehiculoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteVehicle = (vehiculoId, callback) => {
    axios.delete(`${BaseUrl}/PAT_EstatusVehiculo/${vehiculoId}`).then((response) => {
      callback(response.data)
    })
  }
  // Vehicle status
  const getEstatusVehicles = (callback) => {
    axios.get(`${BaseUrl}/PAT_EstatusVehiculo`).then((response) => {
      callback(response.data)
    })
  }
  const getEstatusVehicle = (estatusVehiculoId, callback) => {
    axios.get(`${BaseUrl}/PAT_EstatusVehiculo/${estatusVehiculoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createEstatusVehicle = (data, callback) => {
    axios.post(`${BaseUrl}/PAT_EstatusVehiculo`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateEstatusVehicle = (data, callback) => {
    axios.put(`${BaseUrl}/PAT_EstatusVehiculo/${data.estatusVehiculoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteEstatusVehicle = (estatusVehiculoId, callback) => {
    axios.delete(`${BaseUrl}/PAT_EstatusVehiculo/${estatusVehiculoId}`).then((response) => {
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
