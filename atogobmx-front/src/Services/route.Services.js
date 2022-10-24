import axios from 'axios'

export default function Route () {
  const getRoute = (callback) => {
    axios.get('https://localhost:7065/api/Aseo').then((response) => {
      callback(response.data)
    })
  }

  const getRouteById = (rutaId, callback) => {
    axios.get(`https://localhost:7065/api/Aseo/${rutaId}`).then((response) => {
      callback(response.data)
    })
  }

  const createRoute = (data, callback) => {
    axios.post('https://localhost:7065/api/Aseo', data).then((response) => {
      callback(response.data)
    })
  }

  const updateRoute = (data, callback) => {
    axios.put(`https://localhost:7065/api/Aseo/${data.rutaId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteRoute = (rutaId, callback) => {
    axios.delete(`https://localhost:7065/api/Aseo/${rutaId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getRoute,
    getRouteById,
    createRoute,
    updateRoute,
    deleteRoute
  }
}
