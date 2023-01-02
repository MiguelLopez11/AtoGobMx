import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function CleannessEmployeeService () {
  const getCleannessEmployee = (callback) => {
    axiosPrivate.get('/AseoEmpleado').then((response) => {
      callback(response.data)
    })
  }

  const getCleannessEmployeeById = (AseoEmpleadoId, callback) => {
    axiosPrivate.get(`/AseoEmpleado/${AseoEmpleadoId}`).then((response) => {
      callback(response.data)
    })
  }

  const createCleannessEmployee = (data, callback) => {
    axiosPrivate.post('/AseoEmpleado', data).then((response) => {
      callback(response.data)
    })
  }

  const updateCleannessEmployee = (data, callback) => {
    axiosPrivate.put(`/AseoEmpleado/${data.AseoEmpleadoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteCleannessEmployee = (AseoEmpleadoId, callback) => {
    axiosPrivate.delete(`/AseoEmpleado/${AseoEmpleadoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getCleannessEmployee,
    getCleannessEmployeeById,
    createCleannessEmployee,
    updateCleannessEmployee,
    deleteCleannessEmployee
  }
}
