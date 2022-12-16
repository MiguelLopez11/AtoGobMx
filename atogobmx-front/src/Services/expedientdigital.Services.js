import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ExpedientDigitalServices () {
  const getExpedients = (callback) => {
    axiosPrivate.get('/ExpedienteDigital').then((response) => {
      callback(response.data)
    })
  }
  const getExpedient = (digitalExpedientId, callback) => {
    axiosPrivate.get(`/ExpedienteDigital/${digitalExpedientId}`).then((response) => {
      callback(response.data)
    })
  }
  const getExpedientByEmpleadoId = (EmpleadoId, callback) => {
    axiosPrivate.get(`/ExpedienteDigital/Empleado/${EmpleadoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createExpedient = (data, callback) => {
    axiosPrivate.post('/ExpedienteDigital', data).then((response) => {
      callback(response.data)
    })
  }
  const updateExpedient = (data, callback) => {
    axiosPrivate.put(`/ExpedienteDigital/${data.expedienteDigitalId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteExpedient = (digitalExpedientId, callback) => {
    axiosPrivate.delete(`/ExpedienteDigital/${digitalExpedientId}`).then((response) => {
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
