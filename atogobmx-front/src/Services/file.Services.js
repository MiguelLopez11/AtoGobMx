import axios from 'axios'

export default function FileServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const getExpedientPhotoProfile = (ExpedienteDigitalId, callback) => {
    axios.get(`${BaseUrl}/Archivos/FotoPerfil/${ExpedienteDigitalId}`).then((response) => {
      callback(response)
    })
  }
  const getDocuments = (ExpedienteDigitalId, callback) => {
    axios.get(`${BaseUrl}/Archivos/Documentos/${ExpedienteDigitalId}`).then((response) => {
      callback(response.data)
    })
  }
  const createExpedientPhotoProfile = (expedienteDigitalId, file, callback) => {
    axios.post(`${BaseUrl}/Archivos/Imagen/${expedienteDigitalId}`, file).then((response) => {
      callback(response)
    }).catch((exception) => {
      console.log(exception)
      callback(exception.response.data)
    })
  }
  return {
    getExpedientPhotoProfile,
    getDocuments,
    createExpedientPhotoProfile
  }
}
