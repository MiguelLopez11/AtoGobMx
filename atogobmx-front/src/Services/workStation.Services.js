import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function WorkStationServices () {
  const getWorkStations = (callback) => {
    axiosPrivate.get('/PuestoTrabajo').then((response) => {
      callback(response.data)
    })
  }
  const getWorkStationByDepartament = (DepartamentoId, callback) => {
    axiosPrivate.get(`/PuestoTrabajo/Departamento/${DepartamentoId}`).then((response) => {
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
    getWorkStationByDepartament,
    getWorkStation,
    createWorkStation,
    updateWorkStation,
    deleteWorkStation
  }
}
