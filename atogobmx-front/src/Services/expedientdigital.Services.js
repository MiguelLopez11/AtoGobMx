import axios from 'axios'

export default function ExpedientDigitalServices () {
  const getExpedients = (callback) => {
    axios.get('https://localhost:7065/api/ExpedienteDigital').then((response) => {
      callback(response.data)
    })
  }
  return {
    getExpedients
  }
}