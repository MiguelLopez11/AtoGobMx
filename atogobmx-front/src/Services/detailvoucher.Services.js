import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function DetailVoucherServices () {
  const getDetailVoucher = (ControlValeId, callback) => {
    axiosPrivate.get(`/PROV_DetalleVale/ControlVale/${ControlValeId}`).then((response) => {
      callback(response.data)
    })
  }

  const getDetailVoucherById = (detalleValeId, callback) => {
    axiosPrivate
      .get(`/PROV_DetalleVale/${detalleValeId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createDetailVoucher = (data, callback) => {
    axiosPrivate.post('/PROV_DetalleVale', data).then((response) => {
      callback(response.data)
    })
  }

  const updateDetailVoucher = (data, callback) => {
    axiosPrivate.put(`/PROV_DetalleVale/${data.detalleValeId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteDetailVoucher = (detalleValeId, callback) => {
    axiosPrivate.delete(`/PROV_DetalleVale/${detalleValeId}`).then((response) => {
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
