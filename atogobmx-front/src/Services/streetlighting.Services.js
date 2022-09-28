import axios from 'axios'

export default function streetlightingServices () {
  const getStreetLighting = (callback) => {
    axios
      .get('https://localhost:7065/api/Alumbrado')
      .then((response) => {
        callback(response.data)
      })
  }
  const geStreetLightingg = (AlumbradoId, callback) => {
    axios.get(`https://localhost:7065/api/Alumbrado/${AlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createStreetLighting = (data, callback) => {
    axios.post('https://localhost:7065/api/Alumbrado', data).then((response) => {
      callback(response.data)
    })
  }
  const updateStreetLighting = (data, callback) => {
    console.log(data)
    axios.put(`https://localhost:7065/api/Alumbrado/${data.alumbradoId}`, data).then((response) => {
      // console.log(response)
      callback(response.data)
    })
  }
  const deleteStreetLighting = (alumbradoId, callback) => {
    axios.delete(`https://localhost:7065/api/Alumbrado/${alumbradoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getStreetLighting,
    geStreetLightingg,
    createStreetLighting,
    updateStreetLighting,
    deleteStreetLighting
  }
}
