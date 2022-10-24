import axios from 'axios'

export default function RoadService () {
  const getRoad = (callback) => {
    axios.get('https://localhost:7065/api/Aseo').then((response) => {
      callback(response.data)
    })
  }

  const getRoadById = (rutaId, callback) => {
    axios.get(`https://localhost:7065/api/Aseo/${rutaId}`).then((response) => {
      callback(response.data)
    })
  }

  const createRoad = (data, callback) => {
    axios.post('https://localhost:7065/api/Aseo', data).then((response) => {
      callback(response.data)
    })
  }

  const updateRoad = (data, callback) => {
    axios.put(`https://localhost:7065/api/Aseo/${data.rutaId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteRoad = (rutaId, callback) => {
    axios.delete(`https://localhost:7065/api/Aseo/${rutaId}`).then((response) => {
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
