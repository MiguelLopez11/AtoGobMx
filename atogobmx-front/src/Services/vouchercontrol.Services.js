import axios from 'axios'

export default function VoucherControlServices () {
  const getVoucherControl = (callback) => {
    axios.get('http://localhost:5000/api/ControlDeVales').then((response) => {
      callback(response.data)
    })
  }

  const getVoucherControlById = (controlValeId, callback) => {
    axios
      .get(`https://localhost:5000/api/ControlDeVales/${controlValeId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createVoucherControl = (data, callback) => {
    axios.post('http://localhost:5000/api/ControlDeVales', data).then((response) => {
      callback(response.data)
    })
  }

  const updateVoucherControl = (data, callback) => {
    axios.put(`https://localhost:5000/api/ControlDeVales/${data.controlValeId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteVoucherControl = (controlValeId, callback) => {
    axios.delete(`https://localhost:5000/api/ControlDeVales/${controlValeId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getVoucherControl,
    getVoucherControlById,
    createVoucherControl,
    updateVoucherControl,
    deleteVoucherControl
  }
}
