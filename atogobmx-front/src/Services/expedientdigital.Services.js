import axios from 'axios'

export default function ExpedientDigitalServices () {
  const getEmployeeExpedients = (callback) => {
    axios.get('https://localhost:7065/api/Empleados/EmpleadoExpedientes').then((response) => {
      callback(response.data)
    })
  }
  return {
    getEmployeeExpedients
  }
}
