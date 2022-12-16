import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function MedicalProductsServices () {
  const getProducts = (callback) => {
    axiosPrivate.get('/SERMED_Productos').then((response) => {
      callback(response.data)
    })
  }
  const getProduct = (productoId, callback) => {
    axiosPrivate.get(`/SERMED_Productos/${productoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createProduct = (data, callback) => {
    axiosPrivate.post('/SERMED_Productos', data).then((response) => {
      callback(response.data)
    })
  }
  const updateProduct = (data, callback) => {
    axiosPrivate.put(`/SERMED_Productos/${data.productoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteProduct = (productoId, callback) => {
    axiosPrivate.delete(`/SERMED_Productos/${productoId}`).then((response) => {
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
