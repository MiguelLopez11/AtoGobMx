import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function AreaServices () {
  const getAreas = (callback) => {
    axiosPrivate.get('/Areas').then((response) => {
      callback(response.data)
    })
  }
  const getAreasByDepartament = (departamentId, callback) => {
    axiosPrivate.get(`/Areas/Departamento/${departamentId}`).then((response) => {
      callback(response.data)
    })
  }
  const getArea = (areaId, callback) => {
    axiosPrivate.get(`/Areas/${areaId}`).then((response) => {
      callback(response.data)
    })
  }
  const createArea = (data, callback) => {
    axiosPrivate.post('/Areas', data).then((response) => {
      callback(response.data)
    })
  }
  const updateArea = (data, callback) => {
    axiosPrivate.put(`/Areas/${data.areaId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteArea = (areaId, callback) => {
    axiosPrivate.delete(`/Areas/${areaId}`).then((response) => {
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
