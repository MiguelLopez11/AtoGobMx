import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function RoadService () {
  const getRoad = (callback) => {
    axiosPrivate.get('/Ruta').then((response) => {
      callback(response.data)
    })
  }

  const getRoadById = (rutaId, callback) => {
    axiosPrivate.get(`/Ruta/${rutaId}`).then((response) => {
      callback(response.data)
    })
  }

  const createRoad = (data, callback) => {
    axiosPrivate.post('/Ruta', data).then((response) => {
      callback(response.data)
    })
  }

  const updateRoad = (data, callback) => {
    axiosPrivate.put(`/Ruta/${data.rutaId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteRoad = (rutaId, callback) => {
    axiosPrivate.delete(`/Ruta/${rutaId}`).then((response) => {
      callback(response.data)
    })
  }
  // Coords Road
  const getCoordsRoad = (callback) => {
    axiosPrivate.get('/CoordenadaRuta').then((response) => {
      callback(response.data)
    })
  }

  const getCoordsRoadById = (CoordenadaRutaId, callback) => {
    axiosPrivate.get(`/CoordenadaRuta/${CoordenadaRutaId}`).then((response) => {
      callback(response.data)
    })
  }

  const createCoordsRoad = (data, callback) => {
    axiosPrivate.post('/CoordenadaRuta', data).then((response) => {
      callback(response.data)
    })
  }

  const updateCoordsRoad = (data, callback) => {
    axiosPrivate.put(`/CoordenadaRuta/${data.coordenadaId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteCoordsRoad = (CoordenadaRutaId, callback) => {
    axiosPrivate.delete(`/CoordenadaRuta/${CoordenadaRutaId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getRoad,
    getRoadById,
    createRoad,
    updateRoad,
    deleteRoad,
    getCoordsRoad,
    getCoordsRoadById,
    createCoordsRoad,
    updateCoordsRoad,
    deleteCoordsRoad
  }
}
