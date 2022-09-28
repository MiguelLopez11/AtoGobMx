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
  const createExpedient = (data, callback) => {
    axios.post(`${BaseUrl}/ExpedienteDigital`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateExpedient = (data, callback) => {
    axios.put(`${BaseUrl}/Empleados/${data.digitalExpedientId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteExpedient = (digitalExpedientId, callback) => {
    axios.delete(`${BaseUrl}/Empleados/${digitalExpedientId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getExpedients,
    getExpedient,
    createExpedient,
    updateExpedient,
    deleteExpedient
  }
}
