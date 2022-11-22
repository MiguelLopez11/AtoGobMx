import axios from 'axios'

export default function VoucherControlServices () {
  const getVoucherControl = (callback) => {
    axios.get('https://localhost:7065/api/ControlDeVales').then((response) => {
      callback(response.data)
    })
  }

  const getVoucherControlById = (controlValeId, callback) => {
    axios
      .get(`https://localhost:7065/api/ControlDeVales/${controlValeId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createVoucherControl = (data, callback) => {
    axios.post('https://localhost:7065/api/ControlDeVales', data).then((response) => {
      callback(response.data)
    })
  }

  const updateVoucherControl = (data, callback) => {
    axios.put(`https://localhost:7065/api/ControlDeVales/${data.controlValeId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteVoucherControl = (controlValeId, callback) => {
    axios.delete(`https://localhost:7065/api/ControlDeVales/${controlValeId}`).then((response) => {
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
