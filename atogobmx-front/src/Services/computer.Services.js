import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ComputerServices () {
  const getComputers = (callback) => {
    axiosPrivate.get('/EquiposComputos').then((response) => {
      callback(response.data)
    })
  }
  const getComputer = (EquipoComputoId, callback) => {
    axiosPrivate.get(`/EquiposComputos/${EquipoComputoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createComputer = (data, callback) => {
    axiosPrivate.post('/EquiposComputos', data).then((response) => {
      callback(response.data)
    })
  }
  const updateComputer = (data, callback) => {
    axiosPrivate.put(`/EquiposComputos/${data.equipoComputoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteComputer = (EquipoComputoId, callback) => {
    axiosPrivate.delete(`/EquiposComputos/${EquipoComputoId}`).then((response) => {
      callback(response.data)
    })
  }
  // ESTATUS
  const getStatus = (callback) => {
    axiosPrivate.get('/PAT_EstatusEquipo').then((response) => {
      callback(response.data)
    })
  }
  const getStatusById = (estatusEquipoId, callback) => {
    axiosPrivate.get(`/PAT_EstatusEquipo/${estatusEquipoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createStatusComputer = (data, callback) => {
    axiosPrivate.post('/PAT_EstatusEquipo', data).then((response) => {
      callback(response.data)
    })
  }
  const updateStatusComputer = (data, callback) => {
    axiosPrivate.put(`/PAT_EstatusEquipo/${data.estatusEquipoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteStatusComputer = (estatusEquipoId, callback) => {
    axiosPrivate.delete(`/PAT_EstatusEquipo/${estatusEquipoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getComputers,
    getComputer,
    createComputer,
    updateComputer,
    deleteComputer,
    // ESTATUS
    getStatus,
    getStatusById,
    createStatusComputer,
    updateStatusComputer,
    deleteStatusComputer
  }
}
