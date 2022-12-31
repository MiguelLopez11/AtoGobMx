import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function DrawerService () {
  const getDrawer = (callback) => {
    axiosPrivate.get('/Gabetas').then((response) => {
      callback(response.data)
    })
  }

  const getDrawerById = (gabetaId, callback) => {
    axiosPrivate.get(`/Gabetas/${gabetaId}`).then((response) => {
      callback(response.data)
    })
  }

  const createDrawer = (data, callback) => {
    axiosPrivate.post('/Gabetas', data).then((response) => {
      callback(response.data)
    })
  }

  const updateDrawer = (data, callback) => {
    axiosPrivate.put(`/Gabetas/${data.gabetaId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteDrawer = (gabetaId, callback) => {
    axiosPrivate.delete(`/Gabetas/${gabetaId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getDrawer,
    getDrawerById,
    createDrawer,
    updateDrawer,
    deleteDrawer
  }
}
