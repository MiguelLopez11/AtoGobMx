import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function AddressCementeryService () {
  const getAddressCementery = (callback) => {
    axiosPrivate.get('/DireccionCementerio').then((response) => {
      callback(response.data)
    })
  }

  const getAddressCementeryById = (direccionId, callback) => {
    axiosPrivate.get(`/DireccionCementerio/${direccionId}`).then((response) => {
      callback(response.data)
    })
  }

  const createAddressCementery = (data, callback) => {
    axiosPrivate.post('/DireccionCementerio', data).then((response) => {
      callback(response.data)
    })
  }

  const updateAddressCementery = (data, callback) => {
    axiosPrivate.put(`/DireccionCementerio/${data.direccionId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteAddressCementery = (direccionId, callback) => {
    axiosPrivate.delete(`/DireccionCementerio/${direccionId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getAddressCementery,
    getAddressCementeryById,
    createAddressCementery,
    updateAddressCementery,
    deleteAddressCementery
  }
}
