import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ProductVoucherServices () {
  const getProductVoucher = (callback) => {
    axiosPrivate.get('/PROV_Producto').then((response) => {
      callback(response.data)
    })
  }

  const getProductVoucherById = (productoId, callback) => {
    axiosPrivate
      .get(`/PROV_Producto/${productoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createProductVoucher = (data, callback) => {
    axiosPrivate.post('/PROV_Producto', data).then((response) => {
      callback(response.data)
    })
  }

  const updateProductVoucher = (data, callback) => {
    axiosPrivate.put(`/PROV_Producto/${data.productoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteProductVoucher = (productoId, callback) => {
    axiosPrivate.delete(`/PROV_Producto/${productoId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getProductVoucher,
    getProductVoucherById,
    createProductVoucher,
    updateProductVoucher,
    deleteProductVoucher
  }
}
