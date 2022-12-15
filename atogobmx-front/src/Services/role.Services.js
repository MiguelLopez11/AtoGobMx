import axios from 'axios'

export default function roleServices () {
  const BaseUrl = 'http://localhost:5000/api'
  const getRoles = (callback) => {
    axios.get(`${BaseUrl}/Role`).then((response) => {
      callback(response.data)
    })
  }
  const getRole = (roleId, callback) => {
    axios.get(`${BaseUrl}/Role/${roleId}`).then((response) => {
      callback(response.data)
    })
  }
  const createRole = (data, callback) => {
    axios.post(`${BaseUrl}/Role`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateRole = (data, callback) => {
    axios.put(`${BaseUrl}/Role/${data.roleId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteRole = (roleId, callback) => {
    axios.delete(`${BaseUrl}/Role/${roleId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getRoles,
    getRole,
    createRole,
    updateRole,
    deleteRole
  }
}
