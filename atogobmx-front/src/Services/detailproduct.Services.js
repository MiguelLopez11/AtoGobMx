import axios from 'axios'

export default function DetailProductServices () {
  const getDetailProduct = (callback) => {
    axios.get('http://localhost:5000/api/PROV_DetalleProducto').then((response) => {
      callback(response.data)
    })
  }

  const getDetailProductById = (detalleProductoId, callback) => {
    axios
      .get(`https://localhost:5000/api/PROV_DetalleProducto/${detalleProductoId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createDetailProduct = (data, callback) => {
    axios.post('http://localhost:5000/api/PROV_DetalleProducto', data).then((response) => {
      callback(response.data)
    })
  }

  const updateDetailProduct = (data, callback) => {
    axios.put(`https://localhost:5000/api/PROV_DetalleProducto/${data.detalleProductoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteDetailProduct = (detalleProductoId, callback) => {
    axios.delete(`https://localhost:5000/api/PROV_DetalleProducto/${detalleProductoId}`).then((response) => {
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
