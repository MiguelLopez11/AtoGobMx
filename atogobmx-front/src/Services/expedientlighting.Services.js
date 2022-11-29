import axios from 'axios'

export default function ExpedientLighting () {
  const getExpedientLighting = (callback) => {
    axios.get('https://localhost:7065/api/ExpedienteAlumbrado').then((response) => {
      callback(response.data)
    })
  }

  const getExpedientLightingByAlumbradoId = (AlumbradoId, callback) => {
    axios.get(`https://localhost:7065/api/ExpedienteDigital/Alumbrado/${AlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }

  const getExpedientLightingById = (expedienteAlumbradoId, callback) => {
    axios.get(`https://localhost:7065/api/ExpedienteAlumbrado/${expedienteAlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }

  const createExpedientLighting = (data, callback) => {
    axios.post('https://localhost:7065/api/ExpedienteAlumbrado', data).then((response) => {
      callback(response.data)
    })
  }

  const updatExpedientLighting = (data, callback) => {
    axios.put(`https://localhost:7065/api/ExpedienteAlumbrado/${data.expedienteAlumbradoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteExpedientLighting = (expedienteAlumbradoId, callback) => {
    axios.delete(`https://localhost:7065/api/ExpedienteAlumbrado/${expedienteAlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getExpedientLighting,
    getExpedientLightingById,
    createExpedientLighting,
    updatExpedientLighting,
    getExpedientLightingByAlumbradoId,
    deleteExpedientLighting
  }
}
