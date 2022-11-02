import axios from 'axios'

export default function VehiclesServices () {
  const BaseUrl = 'https://localhost:7065/api'
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
    axios.delete(`${BaseUrl}/PAT_Vehiculos/${vehiculoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getVehicles,
    getVehicle,
    createVehicle,
    updateVehicle,
    deleteVehicle
  }
}