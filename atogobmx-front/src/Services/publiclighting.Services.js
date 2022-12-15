import axios from 'axios'

export default function publiclightingServices () {
  const getPublicLighting = (callback) => {
    axios
      .get('http://localhost:5000/api/Alumbrado')
      .then((response) => {
        callback(response.data)
      })
  }
  const getPublicLightingExpedient = (callback) => {
    axios.get('http://localhost:5000/api/Alumbrado/ServiciosSinExpediente').then((response) => {
      callback(response.data)
    })
  }
  const getPublicLightingById = (AlumbradoId, callback) => {
    axios.get(`https://localhost:5000/api/Alumbrado/${AlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPublicLighting = (data, callback) => {
    axios.post('http://localhost:5000/api/Alumbrado', data).then((response) => {
      callback(response.data)
    })
  }
  const updatePublicLighting = (data, callback) => {
    axios.put(`https://localhost:5000/api/Alumbrado/${data.alumbradoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deletePublicLighting = (alumbradoId, callback) => {
    axios.delete(`https://localhost:5000/api/Alumbrado/${alumbradoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPublicLighting,
    getPublicLightingExpedient,
    getPublicLightingById,
    createPublicLighting,
    updatePublicLighting,
    deletePublicLighting
  }
}
