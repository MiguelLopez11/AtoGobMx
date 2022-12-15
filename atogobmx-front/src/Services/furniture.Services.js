import axios from 'axios'

export default function FurnitureServices () {
  const BaseUrl = 'http://localhost:5000/api'
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
  // TYPEFURNITURES
  const getTypeFurnitures = (callback) => {
    axios.get(`${BaseUrl}/TipoMobiliario`).then((response) => {
      callback(response.data)
    })
  }
  const getTypeFurniture = (tipoMobiliarioId, callback) => {
    axios.get(`${BaseUrl}/TipoMobiliario/${tipoMobiliarioId}`).then((response) => {
      callback(response.data)
    })
  }
  const createTypeFurniture = (data, callback) => {
    axios.post(`${BaseUrl}/TipoMobiliario`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateTypeFurniture = (data, callback) => {
    axios.put(`${BaseUrl}/TipoMobiliario/${data.tipoMobiliarioId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteTypeFurniture = (tipoMobiliarioId, callback) => {
    axios.delete(`${BaseUrl}/TipoMobiliario/${tipoMobiliarioId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getFurnitures,
    getFurniture,
    createFurniture,
    updateFurniture,
    deleteFurniture,
    // TYPEFURNITURES
    getTypeFurnitures,
    getTypeFurniture,
    createTypeFurniture,
    updateTypeFurniture,
    deleteTypeFurniture
  }
}
