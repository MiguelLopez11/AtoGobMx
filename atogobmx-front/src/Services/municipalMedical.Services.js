import axios from 'axios'

export default function MunicipalMedicalServices () {
  const BaseUrl = 'https://localhost:7065/api'
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
  const deleteExpedientMedical = (expedienteMedicoId, callback) => {
    axios.delete(`${BaseUrl}/SERMED_ExpedienteMedico/${expedienteMedicoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getMedicalAppointments,
    getMedicalAppointment,
    createMedicalAppointment,
    updateMedicalAppointment,
    deleteMedicalAppointment,
    // Expedient Medical
    getExpedientsMedical,
    getExpedientMedical,
    createExpedientMedical,
    updateExpedientMedical,
    deleteExpedientMedical
  }
}
