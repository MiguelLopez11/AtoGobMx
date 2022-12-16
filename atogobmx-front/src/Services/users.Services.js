import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function UsersServices () {
  const getUsers = (callback) => {
    axiosPrivate.get('/Usuarios').then((response) => {
      callback(response.data)
    })
  }
  const getUser = (usuarioId, callback) => {
    axiosPrivate.get(`/Usuarios/${usuarioId}`).then((response) => {
      callback(response.data)
    })
  }
  const createUser = (data, callback) => {
    axiosPrivate.post('/Usuarios', data).then((response) => {
      callback(response.data)
    })
  }
  const updateUser = (data, callback) => {
    axiosPrivate.put(`/Usuarios/${data.usuarioId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteUser = (usuarioId, callback) => {
    axiosPrivate.delete(`/Usuarios/${usuarioId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getUsers,
    getUser,
    createUser,
    updateUser,
    deleteUser
  }
}
