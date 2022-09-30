import axios from 'axios'

export default function FileServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const getExpedientPhotoProfile = (ExpedienteDigitalId, callback) => {
    axios.get(`${BaseUrl}/Archivos/FotoPerfil/${ExpedienteDigitalId}`).then((response) => {
      callback(response)
    })
  }
  const createExpedientPhotoProfile = (expedienteDigitalId, file, callback) => {
    console.log(file)
    axios.post(`${BaseUrl}/Archivos/Imagen/${expedienteDigitalId}`, null, {
      params: {
        file
      }
    }).then((response) => {
      callback(response)
    })
  }
  return {
    getExpedientPhotoProfile,
    createExpedientPhotoProfile
  }
}
