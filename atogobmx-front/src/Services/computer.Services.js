import axios from 'axios'

export default function ComputerServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const getComputers = (callback) => {
    axios.get(`${BaseUrl}/EquiposComputos`).then((response) => {
      callback(response.data)
    })
  }
  const getComputer = (EquipoComputoId, callback) => {
    axios.get(`${BaseUrl}/EquiposComputos/${EquipoComputoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createComputer = (data, callback) => {
    axios.post(`${BaseUrl}/EquiposComputos`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateComputer = (data, callback) => {
    axios.put(`${BaseUrl}/EquiposComputos/${data.equipoComputoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteComputer = (EquipoComputoId, callback) => {
    axios.delete(`${BaseUrl}/EquiposComputos/${EquipoComputoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getComputers,
    getComputer,
    createComputer,
    updateComputer,
    deleteComputer
  }
}
