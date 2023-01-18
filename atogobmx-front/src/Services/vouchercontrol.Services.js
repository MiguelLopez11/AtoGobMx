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

  const getVoucherControls = (DetalleValeId, callback) => {
    axiosPrivate.get(`/ControlDeVales/${DetalleValeId}`).then((response) => {
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
  const autorizeVoucherControl = (controlValeId, callback) => {
    axiosPrivate.put(`/ControlDeVales/Autoriza/${controlValeId}`).then((response) => {
      callback(response.data)
    })
  }
  const DeliveryVoucherControl = (controlValeId, callback) => {
    axiosPrivate.put(`/ControlDeVales/Entrega/${controlValeId}`).then((response) => {
      callback(response.data)
    })
  }
  const receiveVoucherControl = (controlValeId, callback) => {
    axiosPrivate.put(`/ControlDeVales/Recibir/${controlValeId}`).then((response) => {
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
    getVoucherControls,
    createVoucherControl,
    updateVoucherControl,
    autorizeVoucherControl,
    deleteVoucherControl,
    DeliveryVoucherControl,
    receiveVoucherControl
  }
}
