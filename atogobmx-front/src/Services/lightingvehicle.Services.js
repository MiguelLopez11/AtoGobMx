import axios from 'axios'

export default function LightingVehicleServices () {
  const getLightingVehicle = (callback) => {
    axios.get('http://localhost:5000/api/VehiculosAlumbrado').then((response) => {
      callback(response.data)
    })
  }

  const getLightingVehicleById = (vehiculoAlumbradoId, callback) => {
    axios
      .get(`https://localhost:5000/api/VehiculosAlumbrado/${vehiculoAlumbradoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createLightingVehicle = (data, callback) => {
    axios.post('http://localhost:5000/api/VehiculosAlumbrado', data).then((response) => {
      callback(response.data)
    })
  }

  const updateLightingVehicle = (data, callback) => {
    axios.put(`https://localhost:5000/api/VehiculosAlumbrado/${data.vehiculoAlumbradoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteLightingVehicle = (vehiculoAlumbradoId, callback) => {
    axios.delete(`https://localhost:5000/api/VehiculosAlumbrado/${vehiculoAlumbradoId}`).then((response) => {
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
