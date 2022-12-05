import axios from 'axios'

export default function PublickWorksEmployeeService () {
  const getPublickWorksEmployee = (callback) => {
    axios.get('https://localhost:7065/api/OP_Empleados').then((response) => {
      callback(response.data)
    })
  }

  const getPublickWorksEmployeeById = (empleadoObrasId, callback) => {
    axios.get(`https://localhost:7065/api/OP_Empleados/${empleadoObrasId}`).then((response) => {
      callback(response.data)
    })
  }

  const createPublickWorksEmployee = (data, callback) => {
    axios.post('https://localhost:7065/api/OP_Empleados', data).then((response) => {
      callback(response.data)
    })
  }

  const updatePublickWorksEmployee = (data, callback) => {
    axios.put(`https://localhost:7065/api/OP_Empleados/${data.empleadoObrasId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deletePublickWorksEmployee = (empleadoObrasId, callback) => {
    axios.delete(`https://localhost:7065/api/OP_Empleados/${empleadoObrasId}`).then((response) => {
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
