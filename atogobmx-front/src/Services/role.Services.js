import axios from 'axios'

export default function roleServices () {
  const getRoles = (callback) => {
    axios.get('https://localhost:7065/api/Role').then((response) => {
      callback(response.data)
    })
  }
  //   const getUser = (usuarioId, callback) => {
  //     axios.get(`https://localhost:7065/api/Usuarios/${usuarioId}`).then((response) => {
  //       callback(response.data)
  //     })
  //   }
  //   const createUser = (data, callback) => {
  //     axios.post('https://localhost:7065/api/Usuarios', data).then((response) => {
  //       callback(response.data)
  //     })
  //   }
  //   const updateUser = (data, callback) => {
  //     axios.put(`https://localhost:7065/api/Usuarios/${data.usuarioId}`, data).then((response) => {
  //       callback(response.data)
  //     })
  //   }
  //   const deleteUser = (usuarioId, callback) => {
  //     axios.delete(`https://localhost:7065/api/Usuarios/${usuarioId}`).then((response) => {
  //       callback(response.data)
  //     })
  //   }
  return {
    getRoles
    // getUser,
    // createUser,
    // updateUser,
    // deleteUser
  }
}
