import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function CleannessService () {
  const getCleanness = (callback) => {
    axiosPrivate.get('/Aseo').then((response) => {
      callback(response.data)
    })
  }

  const getCleannessById = (aseoId, callback) => {
    axiosPrivate.get(`/Aseo/${aseoId}`).then((response) => {
      callback(response.data)
    })
  }

  const createCleanness = (data, callback) => {
    axiosPrivate.post('/Aseo', data).then((response) => {
      callback(response.data)
    })
  }

  const updateCleanness = (data, callback) => {
    axiosPrivate.put(`/Aseo/${data.aseoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteCleanness = (aseoId, callback) => {
    axiosPrivate.delete(`/Aseo/${aseoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getCleanness,
    getCleannessById,
    createCleanness,
    updateCleanness,
    deleteCleanness
  }
}
