import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function taskTypeLightingServices () {
  const getTaskTypeLighting = (callback) => {
    axiosPrivate.get('/TareaTipoAlumbrado').then((response) => {
      callback(response.data)
    })
  }

  const getTaskTypeLightingById = (tareaTipoId, callback) => {
    axiosPrivate
      .get(`/TareaTipoAlumbrado/${tareaTipoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createTaskTypeLighting = (data, callback) => {
    axiosPrivate.post('/TareaTipoAlumbrado', data).then((response) => {
      callback(response.data)
    })
  }

  const updateTaskTypeLighting = (data, callback) => {
    axiosPrivate.put(`/TareaTipoAlumbrado/${data.tareaTipoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteTaskTypeLighting = (tareaTipoId, callback) => {
    axiosPrivate.delete(`/TareaTipoAlumbrado/${tareaTipoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getTaskTypeLighting,
    getTaskTypeLightingById,
    createTaskTypeLighting,
    updateTaskTypeLighting,
    deleteTaskTypeLighting
  }
}
