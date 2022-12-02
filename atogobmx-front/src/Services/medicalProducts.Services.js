import axios from 'axios'

export default function MedicalProductsServices () {
  const BaseUrl = 'https://localhost:7065/api'

  const getProducts = (callback) => {
    axios.get(`${BaseUrl}/SERMED_Productos`).then((response) => {
      callback(response.data)
    })
  }
  const getProduct = (productoId, callback) => {
    axios.get(`${BaseUrl}/SERMED_Productos/${productoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createProduct = (data, callback) => {
    axios.post(`${BaseUrl}/SERMED_Productos`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateProduct = (data, callback) => {
    axios.put(`${BaseUrl}/SERMED_Productos/${data.productoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteProduct = (productoId, callback) => {
    axios.delete(`${BaseUrl}/SERMED_Productos/${productoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getProducts,
    getProduct,
    createProduct,
    updateProduct,
    deleteProduct
  }
}
