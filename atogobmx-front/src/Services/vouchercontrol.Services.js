import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function VoucherControlServices () {
  const getVoucherControl = (callback) => {
    axiosPrivate.get('/ControlDeVales').then((response) => {
      callback(response.data)
    })
  }

  const getVoucherControlById = (controlValeId, callback) => {
    axiosPrivate
      .get(`/ControlDeVales/${controlValeId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createVoucherControl = (data, callback) => {
    axiosPrivate.post('/ControlDeVales', data).then((response) => {
      callback(response.data)
    })
  }

  const updateVoucherControl = (data, callback) => {
    axiosPrivate.put(`/ControlDeVales/${data.controlValeId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteVoucherControl = (controlValeId, callback) => {
    axiosPrivate.delete(`/ControlDeVales/${controlValeId}`).then((response) => {
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
