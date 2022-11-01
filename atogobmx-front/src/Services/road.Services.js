import axios from 'axios'

export default function RoadService () {
  const getRoad = (callback) => {
    axios.get('https://localhost:7065/api/Ruta').then((response) => {
      callback(response.data)
    })
  }

  const getRoadById = (rutaId, callback) => {
    axios.get(`https://localhost:7065/api/Ruta/${rutaId}`).then((response) => {
      callback(response.data)
    })
  }

  const createRoad = (data, callback) => {
    axios.post('https://localhost:7065/api/Ruta', data).then((response) => {
      callback(response.data)
    })
  }

  const updateRoad = (data, callback) => {
    axios.put(`https://localhost:7065/api/Ruta/${data.rutaId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteRoad = (rutaId, callback) => {
    axios.delete(`https://localhost:7065/api/Ruta/${rutaId}`).then((response) => {
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
