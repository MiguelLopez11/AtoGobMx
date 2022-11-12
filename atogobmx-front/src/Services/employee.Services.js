import axios from 'axios'

export default function EmployeeServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const getEmployees = (callback) => {
    axios.get(`${BaseUrl}/Empleados`).then((response) => {
      callback(response.data)
    })
  }
  const getEmployeesWithoutExpedient = (callback) => {
    axios.get(`${BaseUrl}/Empleados/SinExpedientes`).then((response) => {
      callback(response.data)
    })
  }
  const getEmployeesArmory = (callback) => {
    axios.get(`${BaseUrl}/Empleados/Armeria`).then((response) => {
      callback(response.data)
    })
  }
  const getEmployee = (EmployeeId, callback) => {
    axios.get(`${BaseUrl}/Empleados/${EmployeeId}`).then((response) => {
      callback(response.data)
    })
  }
  const createEmployee = (data, callback) => {
    axios.post(`${BaseUrl}/Empleados`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateEmployee = (data, callback) => {
    axios.put(`${BaseUrl}/Empleados/${data.empleadoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteEmployee = (employeeId, callback) => {
    axios.delete(`${BaseUrl}/Empleados/${employeeId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getEmployees,
    getEmployee,
    getEmployeesArmory,
    createEmployee,
    updateEmployee,
    deleteEmployee,
    getEmployeesWithoutExpedient
  }
}
