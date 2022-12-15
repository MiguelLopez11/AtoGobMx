import axios from 'axios'

export default function ProductVoucherServices () {
  const getProductVoucher = (callback) => {
    axios.get('http://localhost:5000/api/PROV_Producto').then((response) => {
      callback(response.data)
    })
  }

  const getProductVoucherById = (productoId, callback) => {
    axios
      .get(`https://localhost:5000/api/PROV_Producto/${productoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createProductVoucher = (data, callback) => {
    axios.post('http://localhost:5000/api/PROV_Producto', data).then((response) => {
      callback(response.data)
    })
  }

  const updateProductVoucher = (data, callback) => {
    axios.put(`https://localhost:5000/api/PROV_Producto/${data.productoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteProductVoucher = (productoId, callback) => {
    axios.delete(`https://localhost:5000/api/PROV_Producto/${productoId}`).then((response) => {
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
