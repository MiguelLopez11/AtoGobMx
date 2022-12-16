import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ZoneService () {
  const getZone = (callback) => {
    axiosPrivate.get('/Zona').then((response) => {
      callback(response.data)
    })
  }

  const getZoneById = (zonaId, callback) => {
    axiosPrivate.get(`/Zona/${zonaId}`).then((response) => {
      callback(response.data)
    })
  }

  const createZone = (data, callback) => {
    axiosPrivate.post('/Zona', data).then((response) => {
      callback(response.data)
    })
  }

  const updateZone = (data, callback) => {
    axiosPrivate.put(`/Zona/${data.zonaId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteZone = (zonaId, callback) => {
    axiosPrivate.delete(`/Zona/${zonaId}`).then((response) => {
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
