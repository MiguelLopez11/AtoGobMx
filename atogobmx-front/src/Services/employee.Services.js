import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function EmployeeServices () {
  const getEmployees = (callback) => {
    axiosPrivate.get('/Empleados').then((response) => {
      callback(response.data)
    })
  }
  const getEmployeesUnfiled = (callback) => {
    axiosPrivate.get('/Empleados/SinArchivar').then((response) => {
      callback(response.data)
    })
  }
  const getEmployeesWithoutExpedient = (callback) => {
    axiosPrivate.get('/Empleados/SinExpedientes').then((response) => {
      callback(response.data)
    })
  }
  const getEmployeesArmory = (callback) => {
    axiosPrivate.get('/Empleados/Armeria').then((response) => {
      callback(response.data)
    })
  }
  const getEmployee = (EmployeeId, callback) => {
    axiosPrivate.get(`/Empleados/${EmployeeId}`).then((response) => {
      callback(response.data)
    })
  }
  const createEmployee = (data, callback) => {
    axiosPrivate.post('/Empleados', data).then((response) => {
      callback(response.data)
    })
  }
  const updateEmployee = (data, callback) => {
    axiosPrivate.put(`/Empleados/${data.empleadoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteEmployee = (employeeId, callback) => {
    axiosPrivate.delete(`/Empleados/${employeeId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getEmployees,
    getEmployeesUnfiled,
    getEmployeesArmory,
    getEmployeesWithoutExpedient,
    getEmployee,
    createEmployee,
    updateEmployee,
    deleteEmployee
  }
}
