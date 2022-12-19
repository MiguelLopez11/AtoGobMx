import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function UsersServices () {
  const getUsers = (callback) => {
    axiosPrivate.get('/Usuarios').then((response) => {
      callback(response.data)
    })
  }
  // const getUser = (usuarioId, callback) => {
  //   axiosPrivate.get(`/Usuarios/${usuarioId}`).then((response) => {
  //     callback(response.data)
  //   })
  // }
  const createUser = (data, callback) => {
    axiosPrivate.post('/Authenticate/register', data).then((response) => {
      callback(response.data)
    })
  }
  const createAdminUser = (data, callback) => {
    axiosPrivate.post('/Authenticate/register-admin', data).then((response) => {
      callback(response.data)
    })
  }
  // const updateUser = (data, callback) => {
  //   axiosPrivate.put(`/Usuarios/${data.usuarioId}`, data).then((response) => {
  //     callback(response.data)
  //   })
  // }
  const deleteUser = (UserName, callback) => {
    axiosPrivate.delete(`/Authenticate/${UserName}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getUsers,
    // getUser,
    createUser,
    createAdminUser,
    // updateUser,
    deleteUser
  }
}
