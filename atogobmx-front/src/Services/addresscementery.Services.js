import axios from 'axios'

export default function AddressCementeryService () {
  const getAddressCementery = (callback) => {
    axios.get('https://localhost:7065/api/DireccionCementerio').then((response) => {
      callback(response.data)
    })
  }

  const getAddressCementeryById = (direccionId, callback) => {
    axios.get(`https://localhost:7065/api/DireccionCementerio/${direccionId}`).then((response) => {
      callback(response.data)
    })
  }

  const createAddressCementery = (data, callback) => {
    axios.post('https://localhost:7065/api/DireccionCementerio', data).then((response) => {
      callback(response.data)
    })
  }

  const updateAddressCementery = (data, callback) => {
    axios.put(`https://localhost:7065/api/DireccionCementerio/${data.direccionId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteAddressCementery = (direccionId, callback) => {
    axios.delete(`https://localhost:7065/api/DireccionCementerio/${direccionId}`).then((response) => {
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
