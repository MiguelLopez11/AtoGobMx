import axios from 'axios'

export default function AreaServices () {
  const getAreas = (callback) => {
    axios.get('https://localhost:7065/api/Areas').then((response) => {
      callback(response.data)
    })
  }
  const getArea = (areaId, callback) => {
    axios.get(`https://localhost:7065/api/Areas/${areaId}`).then((response) => {
      callback(response.data)
    })
  }
  const createArea = (data, callback) => {
    axios.post('https://localhost:7065/api/Areas', data).then((response) => {
      callback(response.data)
    })
  }
  const updateArea = (data, callback) => {
    axios.put(`https://localhost:7065/api/Areas/${data.areaId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteArea = (areaId, callback) => {
    axios.delete(`https://localhost:7065/api/Areas/${areaId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getAreas,
    getArea,
    createArea,
    updateArea,
    deleteArea
  }
}
