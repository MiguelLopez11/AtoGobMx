import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PublickWorksEmployeeService () {
  const getPublickWorksEmployee = (callback) => {
    axiosPrivate.get('/OP_Empleados').then((response) => {
      callback(response.data)
    })
  }

  const getPublickWorksEmployeeById = (empleadoObrasId, callback) => {
    axiosPrivate.get(`/OP_Empleados/${empleadoObrasId}`).then((response) => {
      callback(response.data)
    })
  }

  const createPublickWorksEmployee = (data, callback) => {
    axiosPrivate.post('/OP_Empleados', data).then((response) => {
      callback(response.data)
    })
  }

  const updatePublickWorksEmployee = (data, callback) => {
    axiosPrivate.put(`/OP_Empleados/${data.empleadoObrasId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deletePublickWorksEmployee = (empleadoObrasId, callback) => {
    axiosPrivate.delete(`/OP_Empleados/${empleadoObrasId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getPublickWorksEmployee,
    getPublickWorksEmployeeById,
    createPublickWorksEmployee,
    updatePublickWorksEmployee,
    deletePublickWorksEmployee
  }
}
