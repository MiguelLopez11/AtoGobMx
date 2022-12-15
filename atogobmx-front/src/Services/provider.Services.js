import axios from 'axios'

export default function ProviderServices () {
  const getProvider = (callback) => {
    axios.get('http://localhost:5000/api/PROV_Proveedor').then((response) => {
      callback(response.data)
    })
  }

  const getProviderById = (proveedorId, callback) => {
    axios
      .get(`https://localhost:5000/api/PROV_Proveedor/${proveedorId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createProvider = (data, callback) => {
    axios.post('http://localhost:5000/api/PROV_Proveedor', data).then((response) => {
      callback(response.data)
    })
  }

  const updateProvider = (data, callback) => {
    axios.put(`https://localhost:5000/api/PROV_Proveedor/${data.proveedorId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteProvider = (proveedorId, callback) => {
    axios.delete(`https://localhost:5000/api/PROV_Proveedor/${proveedorId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getProvider,
    getProviderById,
    createProvider,
    updateProvider,
    deleteProvider
  }
}
