import axios from 'axios'

export default function DepartamentServices () {
  const BaseUrl = 'http://localhost:5000/api'
  const getDepartaments = (callback) => {
    axios.get(`${BaseUrl}/Departamentos`).then((response) => {
      callback(response.data)
    })
  }
  const getDepartament = (departamentoId, callback) => {
    axios.get(`${BaseUrl}/Departamentos/${departamentoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createDepartament = (data, callback) => {
    axios.post(`${BaseUrl}/Departamentos`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateDepartament = (data, callback) => {
    axios.put(`${BaseUrl}/Departamentos/${data.departamentoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteDepartament = (departamentoId, callback) => {
    axios.delete(`${BaseUrl}/Departamentos/${departamentoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getDepartaments,
    getDepartament,
    createDepartament,
    updateDepartament,
    deleteDepartament
  }
}
