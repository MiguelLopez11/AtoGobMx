import axios from 'axios'

export default function WorkStationServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const getWorkStations = (callback) => {
    axios.get(`${BaseUrl}/PuestoTrabajo`).then((response) => {
      callback(response.data)
    })
  }
  const getWorkStation = (puestoTrabajoId, callback) => {
    axios.get(`${BaseUrl}/PuestoTrabajo/${puestoTrabajoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createWorkStation = (data, callback) => {
    axios.post(`${BaseUrl}/PuestoTrabajo`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateWorkStation = (data, callback) => {
    axios.put(`${BaseUrl}/PuestoTrabajo/${data.puestoTrabajoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteWorkStation = (puestoTrabajoId, callback) => {
    axios.delete(`${BaseUrl}/PuestoTrabajo/${puestoTrabajoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getWorkStations,
    getWorkStation,
    createWorkStation,
    updateWorkStation,
    deleteWorkStation
  }
}
