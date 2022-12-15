import axios from 'axios'

export default function ZoneService () {
  const getZone = (callback) => {
    axios.get('http://localhost:5000/api/Zona').then((response) => {
      callback(response.data)
    })
  }

  const getZoneById = (zonaId, callback) => {
    axios.get(`https://localhost:5000/api/Zona/${zonaId}`).then((response) => {
      callback(response.data)
    })
  }

  const createZone = (data, callback) => {
    axios.post('http://localhost:5000/api/Zona', data).then((response) => {
      callback(response.data)
    })
  }

  const updateZone = (data, callback) => {
    axios.put(`https://localhost:5000/api/Zona/${data.zonaId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteZone = (zonaId, callback) => {
    axios.delete(`https://localhost:5000/api/Zona/${zonaId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getZone,
    getZoneById,
    createZone,
    updateZone,
    deleteZone
  }
}
