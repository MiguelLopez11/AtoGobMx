import axios from 'axios'

export default function lightingfailuresServices () {
  const getLightingFailures = (callback) => {
    axios
      .get('https://localhost:7065/api/FallasAlumbradoPublico')
      .then((response) => {
        callback(response.data)
      })
  }
  return {
    getLightingFailures
  }
}
