import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function UsersServices () {
  const getUsers = (callback) => {
    axiosPrivate.get('/Usuarios').then((response) => {
      callback(response.data)
    })
  }
  const createUser = (data, callback) => {
    axiosPrivate.post('/Authenticate/register', data).then((response) => {
      callback(response.data)
    }).catch(({ response }) => {
      console.log(response.status)
      callback(response)
    })
  }
  const createAdminUser = (data, callback) => {
    axiosPrivate.post('/Authenticate/register-admin', data).then((response) => {
      callback(response.data)
    }).catch(({ data }) => {
      callback(data)
    })
  }
  const deleteUser = (UserName, callback) => {
    axiosPrivate.delete(`/Authenticate/${UserName}`)
      .then((response) => {
        callback(response.data)
      })
  }
  return {
    getUsers,
    createUser,
    createAdminUser,
    deleteUser
  }
}
