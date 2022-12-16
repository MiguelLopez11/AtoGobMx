import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function DetailProductServices () {
  const getDetailProduct = (callback) => {
    axiosPrivate.get('/PROV_DetalleProducto').then((response) => {
      callback(response.data)
    })
  }

  const getDetailProductById = (detalleProductoId, callback) => {
    axiosPrivate
      .get(`/PROV_DetalleProducto/${detalleProductoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createDetailProduct = (data, callback) => {
    axiosPrivate.post('/PROV_DetalleProducto', data).then((response) => {
      callback(response.data)
    })
  }

  const updateDetailProduct = (data, callback) => {
    axiosPrivate.put(`/PROV_DetalleProducto/${data.detalleProductoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteDetailProduct = (detalleProductoId, callback) => {
    axiosPrivate.delete(`/PROV_DetalleProducto/${detalleProductoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getDetailProduct,
    getDetailProductById,
    createDetailProduct,
    updateDetailProduct,
    deleteDetailProduct
  }
}
