import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function CementeryService () {
  const getCementery = (callback) => {
    axiosPrivate.get('/Cementerios').then((response) => {
      callback(response.data)
    })
  }

  const getCementeryById = (cementeriosId, callback) => {
    axiosPrivate.get(`/Cementerios/${cementeriosId}`).then((response) => {
      callback(response.data)
    })
  }

  const createCementery = (data, callback) => {
    axiosPrivate.post('/Cementerios', data).then((response) => {
      callback(response.data)
    })
  }

  const updateCementery = (data, callback) => {
    axiosPrivate.put(`/Cementerios/${data.cementeriosId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteCementery = (cementeriosId, callback) => {
    axiosPrivate.delete(`/Cementerios/${cementeriosId}`).then((response) => {
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
