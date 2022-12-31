import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function CementeryService () {
  const getCementery = (callback) => {
    axiosPrivate.get('/Cementerio').then((response) => {
      callback(response.data)
    })
  }

  const getCementeryById = (cementerioId, callback) => {
    axiosPrivate.get(`/Cementerio/${cementerioId}`).then((response) => {
      callback(response.data)
    })
  }

  const createCementery = (data, callback) => {
    axiosPrivate.post('/Cementerio', data).then((response) => {
      callback(response.data)
    })
  }

  const updateCementery = (data, callback) => {
    axiosPrivate.put(`/Cementerio/${data.cementerioId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteCementery = (cementerioId, callback) => {
    axiosPrivate.delete(`/Cementerio/${cementerioId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getCementery,
    getCementeryById,
    createCementery,
    updateCementery,
    deleteCementery
  }
}
