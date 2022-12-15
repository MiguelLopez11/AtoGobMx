import axios from 'axios'

export default function StatusVoucherServices () {
  const getStatusVoucher = (callback) => {
    axios.get('http://localhost:5000/api/PROV_EstatusVale').then((response) => {
      callback(response.data)
    })
  }

  const getStatusVoucherById = (estatusValeId, callback) => {
    axios
      .get(`https://localhost:5000/api/PROV_EstatusVale/${estatusValeId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createStatusVoucher = (data, callback) => {
    axios.post('http://localhost:5000/api/PROV_EstatusVale', data).then((response) => {
      callback(response.data)
    })
  }

  const updateStatusVoucher = (data, callback) => {
    axios.put(`https://localhost:5000/api/PROV_EstatusVale/${data.estatusValeId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteStatusVoucher = (estatusValeId, callback) => {
    axios.delete(`https://localhost:5000/api/PROV_EstatusVale/${estatusValeId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getStatusVoucher,
    getStatusVoucherById,
    createStatusVoucher,
    updateStatusVoucher,
    deleteStatusVoucher
  }
}
