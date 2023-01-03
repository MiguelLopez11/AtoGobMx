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
  // Cartridge
  const getCartridges = (armaId, callback) => {
    axiosPrivate.get(`/PAT_ArmeriaCartuchos/Arma/${armaId}`).then((response) => {
      callback(response.data)
    })
  }
  const getCartridge = (cartuchoId, callback) => {
    axiosPrivate.get(`/PAT_ArmeriaCartuchos/${cartuchoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createCartridge = (data, callback) => {
    axiosPrivate.post('/PAT_ArmeriaCartuchos', data).then((response) => {
      callback(response.data)
    })
  }
  const updateCartridge = (data, callback) => {
    axiosPrivate.put(`/PAT_ArmeriaCartuchos/${data.cartuchoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteCartridge = (cartuchoId, callback) => {
    axiosPrivate.delete(`/PAT_ArmeriaCartuchos/${cartuchoId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getWeapons,
    getWeapon,
    createWeapon,
    updateWeapon,
    deleteWeapon,
    // Cartridge
    getCartridges,
    getCartridge,
    createCartridge,
    updateCartridge,
    deleteCartridge
  }
}
