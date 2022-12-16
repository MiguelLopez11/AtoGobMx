import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function WorkStationServices () {
  const getWorkStations = (callback) => {
    axiosPrivate.get('/PuestoTrabajo').then((response) => {
      callback(response.data)
    })
  }
  const getWorkStationByArea = (AreaId, callback) => {
    axiosPrivate.get(`/PuestoTrabajo/Area/${AreaId}`).then((response) => {
      callback(response.data)
    })
  }
  const getWorkStation = (puestoTrabajoId, callback) => {
    axiosPrivate.get(`/PuestoTrabajo/${puestoTrabajoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createWorkStation = (data, callback) => {
    axiosPrivate.post('/PuestoTrabajo', data).then((response) => {
      callback(response.data)
    })
  }
  const updateWorkStation = (data, callback) => {
    axiosPrivate.put(`/PuestoTrabajo/${data.puestoTrabajoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteWorkStation = (puestoTrabajoId, callback) => {
    axiosPrivate.delete(`/PuestoTrabajo/${puestoTrabajoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getWorkStations,
    getWorkStationByArea,
    getWorkStation,
    createWorkStation,
    updateWorkStation,
    deleteWorkStation
  }
}
