import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PrescriptionServices () {
  const getPrescriptions = (callback) => {
    axiosPrivate.get('/SERMED_Receta').then((response) => {
      callback(response.data)
    })
  }
  const getPrescriptionsPending = (callback) => {
    axiosPrivate.get('/SERMED_Receta/Pendientes').then((response) => {
      callback(response.data)
    })
  }
  const getPrescription = (RecetaId, callback) => {
    axiosPrivate.get(`/SERMED_Receta/${RecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPrescriptionByEmpleadoId = (EmpleadoId, callback) => {
    axiosPrivate.get(`/SERMED_Receta/empleado/${EmpleadoId}`).then((response) => {
      callback(response.data)
    })
  }
  const getProductsPrescriptionByRecetaId = (RecetaId, callback) => {
    axiosPrivate.get(`/SERMED_ProductoReceta/Receta/${RecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPrescription = (data, callback) => {
    axiosPrivate.post('/SERMED_Receta', data).then((response) => {
      callback(response.data)
    })
  }
  const fillPrescription = (RecetaId, callback) => {
    axiosPrivate.post(`/SERMED_Receta/SurtirReceta/${RecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  const updatePrescription = (data, callback) => {
    axiosPrivate.put(`/SERMED_Receta/${data.recetaId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deletePrescription = (RecetaId, callback) => {
    axiosPrivate.delete(`/SERMED_Receta/${RecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  // ProductsPrescription
  const getProductPrescriptions = (callback) => {
    axiosPrivate.get('/SERMED_ProductoReceta').then((response) => {
      callback(response.data)
    })
  }
  const getProductPrescription = (productoRecetaId, callback) => {
    axiosPrivate.get(`/SERMED_ProductoReceta/${productoRecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  const createProductPrescription = (data, callback) => {
    axiosPrivate.post('/SERMED_ProductoReceta', data).then((response) => {
      callback(response.data)
    })
  }
  const updateProductPrescription = (data, callback) => {
    axiosPrivate.put(`/SERMED_ProductoReceta/${data.productoRecetaId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteProductPrescription = (RecetaId, productoRecetaId, callback) => {
    axiosPrivate.delete(`/SERMED_ProductoReceta/Receta/${RecetaId}/${productoRecetaId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPrescriptions,
    getPrescription,
    fillPrescription,
    getPrescriptionsPending,
    getProductsPrescriptionByRecetaId,
    getPrescriptionByEmpleadoId,
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
