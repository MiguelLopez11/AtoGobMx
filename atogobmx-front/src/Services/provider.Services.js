import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ProviderServices () {
  const getProvider = (callback) => {
    axiosPrivate.get('/PROV_Proveedor').then((response) => {
      callback(response.data)
    })
  }

  const getProviderById = (proveedorId, callback) => {
    axiosPrivate
      .get(`/PROV_Proveedor/${proveedorId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createProvider = (data, callback) => {
    axiosPrivate.post('/PROV_Proveedor', data).then((response) => {
      callback(response.data)
    })
  }

  const updateProvider = (data, callback) => {
    axiosPrivate.put(`/PROV_Proveedor/${data.proveedorId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteProvider = (proveedorId, callback) => {
    axiosPrivate.delete(`/PROV_Proveedor/${proveedorId}`).then((response) => {
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
