import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function roleServices () {
  const getRoles = (callback) => {
    axiosPrivate.get('/Role').then((response) => {
      callback(response.data)
    })
  }
  const getRole = (roleId, callback) => {
    axiosPrivate.get(`/Role/${roleId}`).then((response) => {
      callback(response.data)
    })
  }
  const createRole = (data, callback) => {
    axiosPrivate.post('/Role', data).then((response) => {
      callback(response.data)
    })
  }
  const updateRole = (data, callback) => {
    axiosPrivate.put(`/Role/${data.roleId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteRole = (roleId, callback) => {
    axiosPrivate.delete(`/Role/${roleId}`).then((response) => {
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
