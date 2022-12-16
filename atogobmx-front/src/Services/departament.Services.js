import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function DepartamentServices () {
  const getDepartaments = (callback) => {
    axiosPrivate.get('/Departamentos').then((response) => {
      callback(response.data)
    })
  }
  const getDepartament = (departamentoId, callback) => {
    axiosPrivate.get(`/Departamentos/${departamentoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createDepartament = (data, callback) => {
    axiosPrivate.post('/Departamentos', data).then((response) => {
      callback(response.data)
    })
  }
  const updateDepartament = (data, callback) => {
    axiosPrivate.put(`/Departamentos/${data.departamentoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteDepartament = (departamentoId, callback) => {
    axiosPrivate.delete(`/Departamentos/${departamentoId}`).then((response) => {
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
