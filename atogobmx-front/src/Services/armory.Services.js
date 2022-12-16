import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ArmoryServices () {
  const getWeapons = (callback) => {
    axiosPrivate.get('/Armeria').then((response) => {
      callback(response.data)
    })
  }
  const getWeapon = (ArmaId, callback) => {
    axiosPrivate.get(`/Armeria/${ArmaId}`).then((response) => {
      callback(response.data)
    })
  }
  const createWeapon = (data, callback) => {
    axiosPrivate.post('/Armeria', data).then((response) => {
      callback(response.data)
    })
  }
  const updateWeapon = (data, callback) => {
    axiosPrivate.put(`/Armeria/${data.armaId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteWeapon = (ArmaId, callback) => {
    axiosPrivate.delete(`/Armeria/${ArmaId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getWeapons,
    getWeapon,
    createWeapon,
    updateWeapon,
    deleteWeapon
  }
}
