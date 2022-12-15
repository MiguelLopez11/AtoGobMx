import axios from 'axios'

export default function ArmoryServices () {
  const BaseUrl = 'http://localhost:5000/api'
  const getWeapons = (callback) => {
    axios.get(`${BaseUrl}/Armeria`).then((response) => {
      callback(response.data)
    })
  }
  const getWeapon = (ArmaId, callback) => {
    axios.get(`${BaseUrl}/Armeria/${ArmaId}`).then((response) => {
      callback(response.data)
    })
  }
  const createWeapon = (data, callback) => {
    axios.post(`${BaseUrl}/Armeria`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateWeapon = (data, callback) => {
    axios.put(`${BaseUrl}/Armeria/${data.armaId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteWeapon = (ArmaId, callback) => {
    axios.delete(`${BaseUrl}/Armeria/${ArmaId}`).then((response) => {
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
