import axios from 'axios'

export default function AddressCementeryService () {
  const getAddressCementery = (callback) => {
    axios.get('http://localhost:5000/api/DireccionCementerio').then((response) => {
      callback(response.data)
    })
  }

  const getAddressCementeryById = (direccionId, callback) => {
    axios.get(`https://localhost:5000/api/DireccionCementerio/${direccionId}`).then((response) => {
      callback(response.data)
    })
  }

  const createAddressCementery = (data, callback) => {
    axios.post('http://localhost:5000/api/DireccionCementerio', data).then((response) => {
      callback(response.data)
    })
  }

  const updateAddressCementery = (data, callback) => {
    axios.put(`https://localhost:5000/api/DireccionCementerio/${data.direccionId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteAddressCementery = (direccionId, callback) => {
    axios.delete(`https://localhost:5000/api/DireccionCementerio/${direccionId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getAddressCementery,
    getAddressCementeryById,
    createAddressCementery,
    updateAddressCementery,
    deleteAddressCementery
  }
}
