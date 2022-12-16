import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function MunicipalMedicalServices () {
  // Medical Appointments
  const getMedicalAppointments = (callback) => {
    axiosPrivate.get('/SERMED_Cita').then((response) => {
      callback(response.data)
    })
  }
  const getMedicalAppointment = (CitaId, callback) => {
    axiosPrivate.get(`/SERMED_Cita/${CitaId}`).then((response) => {
      callback(response.data)
    })
  }
  const getMedicalAppointmentByEmployee = (EmpleadoId, callback) => {
    axiosPrivate.get(`/SERMED_Cita/Empleado/${EmpleadoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createMedicalAppointment = (data, callback) => {
    axiosPrivate.post('/SERMED_Cita', data).then((response) => {
      callback(response.data)
    })
  }
  const updateMedicalAppointment = (data, callback) => {
    axiosPrivate.put(`/SERMED_Cita/${data.citaId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteMedicalAppointment = (CitaId, callback) => {
    axiosPrivate.delete(`/SERMED_Cita/${CitaId}`).then((response) => {
      callback(response.data)
    })
  }
  // Expedient Medical
  const getExpedientsMedical = (callback) => {
    axiosPrivate.get('/SERMED_ExpedienteMedico').then((response) => {
      callback(response.data)
    })
  }
  const getExpedientMedical = (expedienteMedicoId, callback) => {
    axiosPrivate.get(`/SERMED_ExpedienteMedico/${expedienteMedicoId}`).then((response) => {
      callback(response.data)
    })
  }
  const getExpedientMedicalByEmpleadoId = (EmpleadoId, callback) => {
    axiosPrivate.get(`/SERMED_ExpedienteMedico/Empleado/${EmpleadoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createExpedientMedical = (data, callback) => {
    axiosPrivate.post('/SERMED_ExpedienteMedico', data).then((response) => {
      callback(response.data)
    })
  }
  const updateExpedientMedical = (data, callback) => {
    axiosPrivate.put(`/SERMED_ExpedienteMedico/${data.expedienteMedicoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const unfiledExpedient = (expedienteMedicoId, callback) => {
    axiosPrivate.put(`/SERMED_ExpedienteMedico/Desarchivar/${expedienteMedicoId}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteExpedientMedical = (expedienteMedicoId, callback) => {
    axiosPrivate.delete(`/SERMED_ExpedienteMedico/${expedienteMedicoId}`).then((response) => {
      callback(response.data)
    })
  }
  // Products
  const getMedicalProducts = (callback) => {
    axiosPrivate.get('/SERMED_Productos').then((response) => {
      callback(response.data)
    })
  }
  const getMedicalProduct = (productoId, callback) => {
    axiosPrivate.get(`/SERMED_Productos/${productoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createMedicalProduct = (data, callback) => {
    axiosPrivate.post('/SERMED_Productos', data).then((response) => {
      callback(response.data)
    })
  }
  const updateMedicalProduct = (data, callback) => {
    axiosPrivate.put(`/SERMED_Productos/${data.productoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const supplyMedicalProduct = (productoId, Cantidad, callback) => {
    axiosPrivate.put(`/SERMED_Productos/SurtirProducto/${productoId}/${Cantidad}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteMedicalProduct = (productoId, callback) => {
    axiosPrivate.delete(`/SERMED_Productos/${productoId}`).then((response) => {
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
