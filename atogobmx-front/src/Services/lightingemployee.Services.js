import axios from 'axios'

export default function LightingEmployeeService () {
  const getAddressLightingEmployee = (callback) => {
    axios.get('http://localhost:5000/api/EmpleadosAlumbrado').then((response) => {
      callback(response.data)
    })
  }

  const getAddressLightingEmployeeById = (alumbradoempleadoId, callback) => {
    axios.get(`https://localhost:5000/api/EmpleadosAlumbrado/${alumbradoempleadoId}`).then((response) => {
      callback(response.data)
    })
  }

  const createAddressLightingEmployee = (data, callback) => {
    axios.post('http://localhost:5000/api/EmpleadosAlumbrado', data).then((response) => {
      callback(response.data)
    })
  }

  const updateAddressLightingEmployee = (data, callback) => {
    axios.put(`https://localhost:5000/api/EmpleadosAlumbrado/${data.alumbradoempleadoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteAddressLightingEmployee = (alumbradoempleadoId, callback) => {
    axios.delete(`https://localhost:5000/api/EmpleadosAlumbrado/${alumbradoempleadoId}`).then((response) => {
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
