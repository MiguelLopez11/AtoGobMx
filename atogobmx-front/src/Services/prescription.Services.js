import axios from 'axios'

export default function PrescriptionServices () {
  const BaseUrl = 'https://localhost:7065/api'

  const getPrescriptions = (callback) => {
    axios.get(`${BaseUrl}/SERMED_Receta`).then((response) => {
      callback(response.data)
    })
  }
  const getPrescription = (RecetaId, callback) => {
    axios.get(`${BaseUrl}/SERMED_Receta/${RecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPrescription = (data, callback) => {
    axios.post(`${BaseUrl}/SERMED_Receta`, data).then((response) => {
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
  const deleteProductPrescription = (productoRecetaId, callback) => {
    axios.delete(`${BaseUrl}/SERMED_ProductoReceta/${productoRecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPrescriptions,
    getPrescription,
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
