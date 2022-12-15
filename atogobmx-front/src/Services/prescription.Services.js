import axios from 'axios'

export default function PrescriptionServices () {
  const BaseUrl = 'http://localhost:5000/api'

  const getPrescriptions = (callback) => {
    axios.get(`${BaseUrl}/SERMED_Receta`).then((response) => {
      callback(response.data)
    })
  }
  const getPrescriptionsPending = (callback) => {
    axios.get(`${BaseUrl}/SERMED_Receta/Pendientes`).then((response) => {
      callback(response.data)
    })
  }
  const getPrescription = (RecetaId, callback) => {
    axios.get(`${BaseUrl}/SERMED_Receta/${RecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPrescriptionByEmpleadoId = (EmpleadoId, callback) => {
    axios.get(`${BaseUrl}/SERMED_Receta/empleado/${EmpleadoId}`).then((response) => {
      callback(response.data)
    })
  }
  const getProductsPrescriptionByRecetaId = (RecetaId, callback) => {
    axios.get(`${BaseUrl}/SERMED_ProductoReceta/Receta/${RecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPrescription = (data, callback) => {
    axios.post(`${BaseUrl}/SERMED_Receta`, data).then((response) => {
      callback(response.data)
    })
  }
  const fillPrescription = (RecetaId, callback) => {
    axios.post(`${BaseUrl}/SERMED_Receta/SurtirReceta/${RecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  const updatePrescription = (data, callback) => {
    axios.put(`${BaseUrl}/SERMED_Receta/${data.recetaId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deletePrescription = (RecetaId, callback) => {
    axios.delete(`${BaseUrl}/SERMED_Receta/${RecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  // ProductsPrescription
  const getProductPrescriptions = (callback) => {
    axios.get(`${BaseUrl}/SERMED_ProductoReceta`).then((response) => {
      callback(response.data)
    })
  }
  const getProductPrescription = (productoRecetaId, callback) => {
    axios.get(`${BaseUrl}/SERMED_ProductoReceta/${productoRecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  const createProductPrescription = (data, callback) => {
    axios.post(`${BaseUrl}/SERMED_ProductoReceta`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateProductPrescription = (data, callback) => {
    axios.put(`${BaseUrl}/SERMED_ProductoReceta/${data.productoRecetaId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteProductPrescription = (RecetaId, productoRecetaId, callback) => {
    axios.delete(`${BaseUrl}/SERMED_ProductoReceta/Receta/${RecetaId}/${productoRecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPrescriptions,
    getPrescription,
    fillPrescription,
    getPrescriptionsPending,
    getProductsPrescriptionByRecetaId,
    getPrescriptionByEmpleadoId,
    createPrescription,
    updatePrescription,
    deletePrescription,
    // ProductPrescriptions
    getProductPrescriptions,
    getProductPrescription,
    createProductPrescription,
    updateProductPrescription,
    deleteProductPrescription
  }
}
