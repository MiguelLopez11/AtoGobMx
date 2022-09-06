import axios from 'axios'

export default function UsersServices () {
  const getUsers = (callback) => {
    axios.get('https://localhost:7065/api/Usuarios').then((response) => {
      callback(response.data)
    })
  }
  const getUser = (usuarioId, callback) => {
    axios.get(`https://localhost:7065/api/Usuarios/${usuarioId}`).then((response) => {
      callback(response.data)
    })
  }
  const createUser = (data, callback) => {
    axios.post('https://localhost:7065/api/Usuarios', data).then((response) => {
      callback(response.data)
    })
  }
  const updateUser = (data, callback) => {
    axios.put(`https://localhost:7065/api/Usuarios/${data.usuarioId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteUser = (usuarioId, callback) => {
    axios.delete(`https://localhost:7065/api/Usuarios/${usuarioId}`).then((response) => {
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
