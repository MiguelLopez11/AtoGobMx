import axios from 'axios'

export default function ExpedientDigitalServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const getExpedients = (callback) => {
    axios.get(`${BaseUrl}/ExpedienteDigital`).then((response) => {
      callback(response.data)
    })
  }
  const getExpedient = (digitalExpedientId, callback) => {
    axios.get(`${BaseUrl}/ExpedienteDigital/${digitalExpedientId}`).then((response) => {
      callback(response.data)
    })
  }
  const getExpedientByEmpleadoId = (EmpleadoId, callback) => {
    axios.get(`${BaseUrl}/ExpedienteDigital/Empleado/${EmpleadoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createExpedient = (data, callback) => {
    axios.post(`${BaseUrl}/ExpedienteDigital`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateExpedient = (data, callback) => {
    axios.put(`${BaseUrl}/ExpedienteDigital/${data.expedienteDigitalId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteExpedient = (digitalExpedientId, callback) => {
    axios.delete(`${BaseUrl}/ExpedienteDigital/${digitalExpedientId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getExpedients,
    getExpedient,
    getExpedientByEmpleadoId,
    createExpedient,
    updateExpedient,
    deleteExpedient
  }
}
