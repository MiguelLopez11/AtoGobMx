import axios from 'axios'

export default function DetailVoucherServices () {
  const getDetailVoucher = (callback) => {
    axios.get('http://localhost:5000/api/PROV_DetalleVale').then((response) => {
      callback(response.data)
    })
  }

  const getDetailVoucherById = (detalleValeId, callback) => {
    axios
      .get(`https://localhost:5000/api/PROV_DetalleVale/${detalleValeId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createDetailVoucher = (data, callback) => {
    axios.post('http://localhost:5000/api/PROV_DetalleVale', data).then((response) => {
      callback(response.data)
    })
  }

  const updateDetailVoucher = (data, callback) => {
    axios.put(`https://localhost:5000/api/PROV_DetalleVale/${data.detalleValeId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteDetailVoucher = (detalleValeId, callback) => {
    axios.delete(`https://localhost:5000/api/PROV_DetalleVale/${detalleValeId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getDetailVoucher,
    getDetailVoucherById,
    createDetailVoucher,
    updateDetailVoucher,
    deleteDetailVoucher
  }
}
