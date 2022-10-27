import axios from 'axios'

export default function LightingEmployeeService () {
  const getAddressLightingEmployee = (callback) => {
    axios.get('https://localhost:7065/api/EmpleadosAlumbrado').then((response) => {
      callback(response.data)
    })
  }

  const getAddressLightingEmployeeById = (alumbradoempleadoId, callback) => {
    axios.get(`https://localhost:7065/api/EmpleadosAlumbrado/${alumbradoempleadoId}`).then((response) => {
      callback(response.data)
    })
  }

  const createAddressLightingEmployee = (data, callback) => {
    axios.post('https://localhost:7065/api/EmpleadosAlumbrado', data).then((response) => {
      callback(response.data)
    })
  }

  const updateAddressLightingEmployee = (data, callback) => {
    axios.put(`https://localhost:7065/api/EmpleadosAlumbrado/${data.alumbradoempleadoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteAddressLightingEmployee = (alumbradoempleadoId, callback) => {
    axios.delete(`https://localhost:7065/api/EmpleadosAlumbrado/${alumbradoempleadoId}`).then((response) => {
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
