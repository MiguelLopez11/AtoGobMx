import axios from 'axios'

export default function Cementery () {
  const getCementery = (callback) => {
    axios.get('').then((response) => {
      callback(response.data)
    })
  }

  const getCementeryById = (cementeriosId, callback) => {
    axios.get(`/${cementeriosId}`).then((response) => {
      callback(response.data)
    })
  }

  const createCementery = (data, callback) => {
    axios.post('', data).then((response) => {
      callback(response.data)
    })
  }

  const updateCementery = (data, callback) => {
    axios.put(`/${data.cementeriosId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteCementery = (cementeriosId, callback) => {
    axios.delete(`/${cementeriosId}`).then((response) => {
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
