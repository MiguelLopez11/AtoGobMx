import axios from 'axios'

export default function VehiclesServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const getFurnitures = (callback) => {
    axios.get(`${BaseUrl}/PAT_Mobiliario`).then((response) => {
      callback(response.data)
    })
  }
  const getFurniture = (mobiliarioId, callback) => {
    axios.get(`${BaseUrl}/PAT_Mobiliario/${mobiliarioId}`).then((response) => {
      callback(response.data)
    })
  }
  const createFurniture = (data, callback) => {
    axios.post(`${BaseUrl}/PAT_Mobiliario`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateFurniture = (data, callback) => {
    axios.put(`${BaseUrl}/PAT_Mobiliario/${data.mobiliarioId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteFurniture = (mobiliarioId, callback) => {
    axios.delete(`${BaseUrl}/PAT_Mobiliario/${mobiliarioId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getFurnitures,
    getFurniture,
    createFurniture,
    updateFurniture,
    deleteFurniture
  }
}
