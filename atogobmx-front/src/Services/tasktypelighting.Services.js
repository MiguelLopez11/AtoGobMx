import axios from 'axios'

export default function taskTypeLightingServices () {
  const getTaskTypeLighting = (callback) => {
    axios.get('https://localhost:7065/api/TareaTipoAlumbrado').then((response) => {
      callback(response.data)
    })
  }

  const getTaskTypeLightingById = (tareaTipoId, callback) => {
    axios
      .get(`https://localhost:7065/api/TareaTipoAlumbrado/${tareaTipoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createTaskTypeLighting = (data, callback) => {
    axios.post('https://localhost:7065/api/TareaTipoAlumbrado', data).then((response) => {
      callback(response.data)
    })
  }

  const updateTaskTypeLighting = (data, callback) => {
    axios.put(`https://localhost:7065/api/TareaTipoAlumbrado/${data.tareaTipoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteTaskTypeLighting = (tareaTipoId, callback) => {
    axios.delete(`https://localhost:7065/api/TareaTipoAlumbrado/${tareaTipoId}`).then((response) => {
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