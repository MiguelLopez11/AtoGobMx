import axios from 'axios'

export default function Route () {
  const getZone = (callback) => {
    axios.get('https://localhost:7065/api/Aseo').then((response) => {
      callback(response.data)
    })
  }

  const getZoneById = (zonaId, callback) => {
    axios.get(`https://localhost:7065/api/Aseo/${zonaId}`).then((response) => {
      callback(response.data)
    })
  }

  const createZone = (data, callback) => {
    axios.post('https://localhost:7065/api/Aseo', data).then((response) => {
      callback(response.data)
    })
  }

  const updateZone = (data, callback) => {
    axios.put(`https://localhost:7065/api/Aseo/${data.zonaId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteZone = (zonaId, callback) => {
    axios.delete(`https://localhost:7065/api/Aseo/${zonaId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getZone,
    getZoneById,
    createZone,
    updateZone,
    deleteZone
  }
}
