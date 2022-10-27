import axios from 'axios'

export default function CleannessService () {
  const getCleanness = (callback) => {
    axios.get('https://localhost:7065/api/Aseo').then((response) => {
      callback(response.data)
    })
  }

  const getCleannessById = (aseoId, callback) => {
    axios.get(`https://localhost:7065/api/Aseo/${aseoId}`).then((response) => {
      callback(response.data)
    })
  }

  const createCleanness = (data, callback) => {
    axios.post('https://localhost:7065/api/Aseo', data).then((response) => {
      callback(response.data)
    })
  }

  const updateCleanness = (data, callback) => {
    axios.put(`https://localhost:7065/api/Aseo/${data.aseoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteCleanness = (aseoId, callback) => {
    axios.delete(`https://localhost:7065/api/Aseo/${aseoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getCleanness,
    getCleannessById,
    createCleanness,
    updateCleanness,
    deleteCleanness
  }
}
