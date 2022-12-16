import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function FurnitureServices () {
  const getFurnitures = (callback) => {
    axiosPrivate.get('/PAT_Mobiliario').then((response) => {
      callback(response.data)
    })
  }
  const getFurniture = (mobiliarioId, callback) => {
    axiosPrivate.get(`/PAT_Mobiliario/${mobiliarioId}`).then((response) => {
      callback(response.data)
    })
  }
  const createFurniture = (data, callback) => {
    axiosPrivate.post('/PAT_Mobiliario', data).then((response) => {
      callback(response.data)
    })
  }
  const updateFurniture = (data, callback) => {
    axiosPrivate.put(`/PAT_Mobiliario/${data.mobiliarioId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteFurniture = (mobiliarioId, callback) => {
    axiosPrivate.delete(`/PAT_Mobiliario/${mobiliarioId}`).then((response) => {
      callback(response.data)
    })
  }
  // TYPEFURNITURES
  const getTypeFurnitures = (callback) => {
    axiosPrivate.get('/TipoMobiliario').then((response) => {
      callback(response.data)
    })
  }
  const getTypeFurniture = (tipoMobiliarioId, callback) => {
    axiosPrivate.get(`/TipoMobiliario/${tipoMobiliarioId}`).then((response) => {
      callback(response.data)
    })
  }
  const createTypeFurniture = (data, callback) => {
    axiosPrivate.post('/TipoMobiliario', data).then((response) => {
      callback(response.data)
    })
  }
  const updateTypeFurniture = (data, callback) => {
    axiosPrivate.put(`/TipoMobiliario/${data.tipoMobiliarioId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteTypeFurniture = (tipoMobiliarioId, callback) => {
    axiosPrivate.delete(`/TipoMobiliario/${tipoMobiliarioId}`).then((response) => {
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
