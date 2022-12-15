import axios from 'axios'

export default function RoadService () {
  const getRoad = (callback) => {
    axios.get('http://localhost:5000/api/Ruta').then((response) => {
      callback(response.data)
    })
  }

  const getRoadById = (rutaId, callback) => {
    axios.get(`https://localhost:5000/api/Ruta/${rutaId}`).then((response) => {
      callback(response.data)
    })
  }

  const createRoad = (data, callback) => {
    axios.post('http://localhost:5000/api/Ruta', data).then((response) => {
      callback(response.data)
    })
  }

  const updateRoad = (data, callback) => {
    axios.put(`https://localhost:5000/api/Ruta/${data.rutaId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteRoad = (rutaId, callback) => {
    axios.delete(`https://localhost:5000/api/Ruta/${rutaId}`).then((response) => {
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
