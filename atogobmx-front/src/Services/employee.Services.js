import axios from 'axios'

export default function EmployeeServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const getEmployees = (callback) => {
    axios.get(`${BaseUrl}/Empleados`).then((response) => {
      callback(response.data)
    })
  }
  const getEmployee = (EmployeeId, callback) => {
    axios.get(`https://localhost:7065/api/Empleados/${EmployeeId}`).then((response) => {
      callback(response.data)
    })
  }
  const createEmployee = (data, callback) => {
    axios.post('https://localhost:7065/api/Empleados', data).then((response) => {
      callback(response.data)
    })
  }
  const updateEmployee = (data, callback) => {
    console.log(data.empleadoId)
    axios.put(`https://localhost:7065/api/Empleados/${data.empleadoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteEmployee = (employeeId, callback) => {
    axios.delete(`https://localhost:7065/api/Empleados/${employeeId}`).then((response) => {
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
