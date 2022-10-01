import axios from 'axios'

export default function publiclightingServices () {
  const getPublicLighting = (callback) => {
    axios
      .get('https://localhost:7065/api/Alumbrado')
      .then((response) => {
        callback(response.data)
      })
  }
  const getPublicLightingById = (AlumbradoId, callback) => {
    axios.get(`https://localhost:7065/api/Alumbrado/${AlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPublicLighting = (data, callback) => {
    axios.post('https://localhost:7065/api/Alumbrado', data).then((response) => {
      callback(response.data)
    })
  }
  const updatePublicLighting = (data, callback) => {
    console.log(data)
    axios.put(`https://localhost:7065/api/Alumbrado/${data.alumbradoId}`, data).then((response) => {
      // console.log(response)
      callback(response.data)
    })
  }
  const deletePublicLighting = (alumbradoId, callback) => {
    axios.delete(`https://localhost:7065/api/Alumbrado/${alumbradoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPublicLighting,
    getPublicLightingById,
    createPublicLighting,
    updatePublicLighting,
    deletePublicLighting
  }
}
