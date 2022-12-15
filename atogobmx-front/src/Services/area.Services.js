import axios from 'axios'

export default function AreaServices () {
  const BaseUrl = 'http://localhost:5000/api'
  const getAreas = (callback) => {
    axios.get(`${BaseUrl}/Areas`).then((response) => {
      callback(response.data)
    })
  }
  const getAreasByDepartament = (departamentId, callback) => {
    axios.get(`${BaseUrl}/Areas/Departamento/${departamentId}`).then((response) => {
      callback(response.data)
    })
  }
  const getArea = (areaId, callback) => {
    axios.get(`${BaseUrl}/Areas/${areaId}`).then((response) => {
      callback(response.data)
    })
  }
  const createArea = (data, callback) => {
    axios.post(`${BaseUrl}/Areas`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateArea = (data, callback) => {
    axios.put(`${BaseUrl}/Areas/${data.areaId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteArea = (areaId, callback) => {
    axios.delete(`${BaseUrl}/Areas/${areaId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getAreas,
    getAreasByDepartament,
    getArea,
    createArea,
    updateArea,
    deleteArea
  }
}
