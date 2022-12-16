import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function TypeVoucherServices () {
  const getTypeVoucher = (callback) => {
    axiosPrivate.get('/TipoVales').then((response) => {
      callback(response.data)
    })
  }

  const getTypeVoucherById = (tipoId, callback) => {
    axiosPrivate
      .get(`/TipoVales/${tipoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createTypeVoucher = (data, callback) => {
    axiosPrivate.post('/TipoVales', data).then((response) => {
      callback(response.data)
    })
  }

  const updateTypeVoucher = (data, callback) => {
    axiosPrivate.put(`/TipoVales/${data.tipoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteTypeVoucher = (tipoId, callback) => {
    axiosPrivate.delete(`/TipoVales/${tipoId}`).then((response) => {
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
