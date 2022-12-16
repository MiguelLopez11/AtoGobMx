// import { axiosPrivate } from '@/common/axiosPrivate.js'

// export default function ExpedientLighting () {
//   const getInventoryLighting = (callback) => {
//     axiosPrivate.get('http://localhost:5000/api/InventarioAlumbrado').then((response) => {
//       callback(response.data)
//     })
//   }

//   const getInventoryLightingById = (inventarioAlumbradoId, callback) => {
//     axiosPrivate.get(`https://localhost:5000/api/InventarioAlumbrado/${inventarioAlumbradoId}`).then((response) => {
//       callback(response.data)
//     })
//   }

//   const createInventoryLighting = (data, callback) => {
//     axiosPrivate.post('http://localhost:5000/api/InventarioAlumbrado', data).then((response) => {
//       callback(response.data)
//     })
//   }

//   const updatInventoryLighting = (data, callback) => {
//     axiosPrivate.put(`https://localhost:5000/api/InventarioAlumbrado/${data.inventarioAlumbradoId}`, data).then((response) => {
//       callback(response.data)
//     })
//   }

//   const deleteInventoryLighting = (inventarioAlumbradoId, callback) => {
//     axiosPrivate.delete(`https://localhost:5000/api/InventarioAlumbrado/${inventarioAlumbradoId}`).then((response) => {
//       callback(response.data)
//     })
//   }

//   return {
//     getInventoryLighting,
//     getInventoryLightingById,
//     createInventoryLighting,
//     updatInventoryLighting,
//     deleteInventoryLighting
//   }
// }
