import axios from 'axios'

export default function TypeVoucherServices () {
  const getTypeVoucher = (callback) => {
    axios.get('https://localhost:7065/api/TipoVales').then((response) => {
      callback(response.data)
    })
  }

  const getTypeVoucherById = (tipoId, callback) => {
    axios
      .get(`https://localhost:7065/api/TipoVales/${tipoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createTypeVoucher = (data, callback) => {
    axios.post('https://localhost:7065/api/TipoVales', data).then((response) => {
      callback(response.data)
    })
  }

  const updateTypeVoucher = (data, callback) => {
    axios.put(`https://localhost:7065/api/TipoVales/${data.tipoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteTypeVoucher = (tipoId, callback) => {
    axios.delete(`https://localhost:7065/api/TipoVales/${tipoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getTypeVoucher,
    getTypeVoucherById,
    createTypeVoucher,
    updateTypeVoucher,
    deleteTypeVoucher
  }
}
