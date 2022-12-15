import axios from 'axios'

export default function CementeryService () {
  const getCementery = (callback) => {
    axios.get('http://localhost:5000/api/Cementerios').then((response) => {
      callback(response.data)
    })
  }

  const getCementeryById = (cementeriosId, callback) => {
    axios.get(`https://localhost:5000/api/Cementerios/${cementeriosId}`).then((response) => {
      callback(response.data)
    })
  }

  const createCementery = (data, callback) => {
    axios.post('http://localhost:5000/api/Cementerios', data).then((response) => {
      callback(response.data)
    })
  }

  const updateCementery = (data, callback) => {
    axios.put(`https://localhost:5000/api/Cementerios/${data.cementeriosId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteCementery = (cementeriosId, callback) => {
    axios.delete(`https://localhost:5000/api/Cementerios/${cementeriosId}`).then((response) => {
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
