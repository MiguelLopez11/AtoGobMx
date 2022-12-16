import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function WorksStatusServices () {
  const getWorksStatus = (callback) => {
    axiosPrivate.get('/OP_EstatusObras').then((response) => {
      callback(response.data)
    })
  }

  const getWorksStatusById = (estatusObraId, callback) => {
    axiosPrivate
      .get(`/OP_EstatusObras/${estatusObraId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createWorksStatus = (data, callback) => {
    axiosPrivate.post('/OP_EstatusObras', data).then((response) => {
      callback(response.data)
    })
  }

  const updateWorksStatus = (data, callback) => {
    axiosPrivate.put(`/OP_EstatusObras/${data.estatusObraId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteWorksStatus = (estatusObraId, callback) => {
    axiosPrivate.delete(`/OP_EstatusObras/${estatusObraId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getWorksStatus,
    getWorksStatusById,
    createWorksStatus,
    updateWorksStatus,
    deleteWorksStatus
  }
}
