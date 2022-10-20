import axios from 'axios'

export default function Cementery () {
  const getCementery = (callback) => {
    axios.get('https://localhost:7065/api/Cementerios').then((response) => {
      callback(response.data)
    })
  }

  const getCementeryById = (cementeriosId, callback) => {
    axios.get(`https://localhost:7065/api/Cementerios/${cementeriosId}`).then((response) => {
      callback(response.data)
    })
  }

  const createCementery = (data, callback) => {
    axios.post('https://localhost:7065/api/Cementerios', data).then((response) => {
      callback(response.data)
    })
  }

  const updateCementery = (data, callback) => {
    axios.put(`https://localhost:7065/api/Cementerios/${data.cementeriosId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteCementery = (cementeriosId, callback) => {
    axios.delete(`https://localhost:7065/api/Cementerios/${cementeriosId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getCementery,
    getCementeryById,
    createCementery,
    updateCementery,
    deleteCementery
  }
}
