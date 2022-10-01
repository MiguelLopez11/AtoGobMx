import axios from 'axios'

export default function ExpedientLighting () {
  const getExpedientLighting = (callback) => {
    axios.get('https://localhost:7065/api/ExpedienteAlumbrado').then((response) => {
      callback(response.data)
    })
  }
  return {
    getExpedientLighting
  }
}
