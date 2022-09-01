import axios from 'axios'

export default function lightingfailuresServices () {
  const getLightingFailures = (callback) => {
    axios
      .get('https://localhost:7065/api/FallasAlumbradoPublico')
      .then((response) => {
        callback(response.data)
      })
  }
  const createLightingFailures = (data, callback) => {
    axios.post('https://localhost:7065/api/FallasAlumbradoPublico', data).then((response) => {
      callback(response.data)
    })
  }
  const deleteLightingFailures = (fallaId, callback) => {
    axios.delete(`https://localhost:7065/api/FallasAlumbradoPublico/${fallaId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getLightingFailures,
    createLightingFailures,
    deleteLightingFailures
  }
}
