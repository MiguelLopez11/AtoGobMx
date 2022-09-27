import axios from 'axios'

export default function ExpedientDigitalServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const getExpedients = (callback) => {
    axios.get(`${BaseUrl}/ExpedienteDigital`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getExpedients
  }
}
