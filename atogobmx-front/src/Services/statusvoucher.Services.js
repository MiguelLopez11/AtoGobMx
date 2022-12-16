import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function StatusVoucherServices () {
  const getStatusVoucher = (callback) => {
    axiosPrivate.get('/PROV_EstatusVale').then((response) => {
      callback(response.data)
    })
  }

  const getStatusVoucherById = (estatusValeId, callback) => {
    axiosPrivate
      .get(`/PROV_EstatusVale/${estatusValeId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createStatusVoucher = (data, callback) => {
    axiosPrivate.post('/PROV_EstatusVale', data).then((response) => {
      callback(response.data)
    })
  }

  const updateStatusVoucher = (data, callback) => {
    axiosPrivate.put(`/PROV_EstatusVale/${data.estatusValeId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteStatusVoucher = (estatusValeId, callback) => {
    axiosPrivate.delete(`/PROV_EstatusVale/${estatusValeId}`).then((response) => {
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
