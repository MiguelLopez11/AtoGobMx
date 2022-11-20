import axios from 'axios'

export default function DetailProductServices () {
  const getDetailProduct = (callback) => {
    axios.get('https://localhost:7065/api/PROV_DetalleProducto').then((response) => {
      callback(response.data)
    })
  }

  const getDetailProductById = (detalleProductoId, callback) => {
    axios
      .get(`https://localhost:7065/api/PROV_DetalleProducto/${detalleProductoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createDetailProduct = (data, callback) => {
    axios.post('https://localhost:7065/api/PROV_DetalleProducto', data).then((response) => {
      callback(response.data)
    })
  }

  const updateDetailProduct = (data, callback) => {
    axios.put(`https://localhost:7065/api/PROV_DetalleProducto/${data.detalleProductoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteDetailProduct = (detalleProductoId, callback) => {
    axios.delete(`https://localhost:7065/api/PROV_DetalleProducto/${detalleProductoId}`).then((response) => {
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
