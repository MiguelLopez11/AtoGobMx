import axios from 'axios'

export default function FileServices () {
  const getExpedientPhotoProfile = (ExpedienteDigitalId, callback) => {
    try {
      axios.get(`https://localhost:7065/api/Archivos/FotoPerfil/${ExpedienteDigitalId}`).then((response) => {
        callback(response)
      })
    } catch (error) {
      callback(null)
    }
  }
  return {
    getExpedientPhotoProfile
  }
}