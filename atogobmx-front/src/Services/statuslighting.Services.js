import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function statusServices () {
  const getStatus = (callback) => {
    axiosPrivate.get('/EstatusAlumbrado').then((response) => {
      callback(response.data)
    })
  }

  const getStatusById = (estatusId, callback) => {
    axiosPrivate
      .get(`/EstatusAlumbrado/${estatusId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createStatusLighting = (data, callback) => {
    axiosPrivate.post('/EstatusAlumbrado', data).then((response) => {
      callback(response.data)
    })
  }

  const updateStatusLighting = (data, callback) => {
    axiosPrivate.put(`/EstatusAlumbrado/${data.estatusAlumbradoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteStatusLighting = (estatusId, callback) => {
    axiosPrivate.delete(`/EstatusAlumbrado/${estatusId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getStatus,
    getStatusById,
    createStatusLighting,
    updateStatusLighting,
    deleteStatusLighting
  }
}
