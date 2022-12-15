import axios from 'axios'

export default function MunicipalMedicalServices () {
  const BaseUrl = 'http://localhost:5000/api'
  // Medical Appointments
  const getMedicalAppointments = (callback) => {
    axios.get(`${BaseUrl}/SERMED_Cita`).then((response) => {
      callback(response.data)
    })
  }
  const getMedicalAppointment = (CitaId, callback) => {
    axios.get(`${BaseUrl}/SERMED_Cita/${CitaId}`).then((response) => {
      callback(response.data)
    })
  }
  const getMedicalAppointmentByEmployee = (EmpleadoId, callback) => {
    axios.get(`${BaseUrl}/SERMED_Cita/Empleado/${EmpleadoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createMedicalAppointment = (data, callback) => {
    axios.post(`${BaseUrl}/SERMED_Cita`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateMedicalAppointment = (data, callback) => {
    axios.put(`${BaseUrl}/SERMED_Cita/${data.citaId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteMedicalAppointment = (CitaId, callback) => {
    axios.delete(`${BaseUrl}/SERMED_Cita/${CitaId}`).then((response) => {
      callback(response.data)
    })
  }
  // Expedient Medical
  const getExpedientsMedical = (callback) => {
    axios.get(`${BaseUrl}/SERMED_ExpedienteMedico`).then((response) => {
      callback(response.data)
    })
  }
  const getExpedientMedical = (expedienteMedicoId, callback) => {
    axios.get(`${BaseUrl}/SERMED_ExpedienteMedico/${expedienteMedicoId}`).then((response) => {
      callback(response.data)
    })
  }
  const getExpedientMedicalByEmpleadoId = (EmpleadoId, callback) => {
    axios.get(`${BaseUrl}/SERMED_ExpedienteMedico/Empleado/${EmpleadoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createExpedientMedical = (data, callback) => {
    axios.post(`${BaseUrl}/SERMED_ExpedienteMedico`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateExpedientMedical = (data, callback) => {
    axios.put(`${BaseUrl}/SERMED_ExpedienteMedico/${data.expedienteMedicoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const unfiledExpedient = (expedienteMedicoId, callback) => {
    axios.put(`${BaseUrl}/SERMED_ExpedienteMedico/Desarchivar/${expedienteMedicoId}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteExpedientMedical = (expedienteMedicoId, callback) => {
    axios.delete(`${BaseUrl}/SERMED_ExpedienteMedico/${expedienteMedicoId}`).then((response) => {
      callback(response.data)
    })
  }
  // Products
  const getMedicalProducts = (callback) => {
    axios.get(`${BaseUrl}/SERMED_Productos`).then((response) => {
      callback(response.data)
    })
  }
  const getMedicalProduct = (productoId, callback) => {
    axios.get(`${BaseUrl}/SERMED_Productos/${productoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createMedicalProduct = (data, callback) => {
    axios.post(`${BaseUrl}/SERMED_Productos`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateMedicalProduct = (data, callback) => {
    axios.put(`${BaseUrl}/SERMED_Productos/${data.productoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const supplyMedicalProduct = (productoId, Cantidad, callback) => {
    axios.put(`${BaseUrl}/SERMED_Productos/SurtirProducto/${productoId}/${Cantidad}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteMedicalProduct = (productoId, callback) => {
    axios.delete(`${BaseUrl}/SERMED_Productos/${productoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getMedicalAppointments,
    getMedicalAppointment,
    getMedicalAppointmentByEmployee,
    createMedicalAppointment,
    updateMedicalAppointment,
    deleteMedicalAppointment,
    // Expedient Medical
    getExpedientsMedical,
    getExpedientMedical,
    getExpedientMedicalByEmpleadoId,
    createExpedientMedical,
    updateExpedientMedical,
    unfiledExpedient,
    deleteExpedientMedical,
    // Product medical
    getMedicalProducts,
    getMedicalProduct,
    createMedicalProduct,
    updateMedicalProduct,
    supplyMedicalProduct,
    deleteMedicalProduct
  }
}
