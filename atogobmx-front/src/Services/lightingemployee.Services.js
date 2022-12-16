import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function LightingEmployeeService () {
  const getAddressLightingEmployee = (callback) => {
    axiosPrivate.get('/EmpleadosAlumbrado').then((response) => {
      callback(response.data)
    })
  }

  const getAddressLightingEmployeeById = (alumbradoempleadoId, callback) => {
    axiosPrivate.get(`/EmpleadosAlumbrado/${alumbradoempleadoId}`).then((response) => {
      callback(response.data)
    })
  }

  const createAddressLightingEmployee = (data, callback) => {
    axiosPrivate.post('/EmpleadosAlumbrado', data).then((response) => {
      callback(response.data)
    })
  }

  const updateAddressLightingEmployee = (data, callback) => {
    axiosPrivate.put(`/EmpleadosAlumbrado/${data.alumbradoempleadoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteAddressLightingEmployee = (alumbradoempleadoId, callback) => {
    axiosPrivate.delete(`/EmpleadosAlumbrado/${alumbradoempleadoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getAddressLightingEmployee,
    getAddressLightingEmployeeById,
    createAddressLightingEmployee,
    updateAddressLightingEmployee,
    deleteAddressLightingEmployee
  }
}
