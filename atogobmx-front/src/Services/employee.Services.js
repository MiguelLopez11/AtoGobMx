import axios from 'axios'

export default function EmployeeServices () {
  const getEmployees = (callback) => {
    axios.get('https://localhost:7065/api/Empleados').then((response) => {
      callback(response.data)
    })
  }
  const createEmployee = (data, callback) => {
    axios.post('https://localhost:7065/api/Empleados', data).then((response) => {
      callback(response.data)
    })
  }
  return {
    getEmployees,
    createEmployee
  }
}