import axios from 'axios'

export default function EmployeeServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const getEmployees = (callback) => {
    axios.get(`${BaseUrl}/Empleados`).then((response) => {
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
    console.log(data.empleadoId)
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
    createEmployee,
    updateEmployee,
    deleteEmployee
  }
}
