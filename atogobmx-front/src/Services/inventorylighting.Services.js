// import axios from 'axios'

// export default function ExpedientLighting () {
//   const getInventoryLighting = (callback) => {
//     axios.get('https://localhost:7065/api/InventarioAlumbrado').then((response) => {
//       callback(response.data)
//     })
//   }

//   const getInventoryLightingById = (inventarioAlumbradoId, callback) => {
//     axios.get(`https://localhost:7065/api/InventarioAlumbrado/${inventarioAlumbradoId}`).then((response) => {
//       callback(response.data)
//     })
//   }

//   const createInventoryLighting = (data, callback) => {
//     axios.post('https://localhost:7065/api/InventarioAlumbrado', data).then((response) => {
//       callback(response.data)
//     })
//   }

//   const updatInventoryLighting = (data, callback) => {
//     axios.put(`https://localhost:7065/api/InventarioAlumbrado/${data.inventarioAlumbradoId}`, data).then((response) => {
//       callback(response.data)
//     })
//   }

//   const deleteInventoryLighting = (inventarioAlumbradoId, callback) => {
//     axios.delete(`https://localhost:7065/api/InventarioAlumbrado/${inventarioAlumbradoId}`).then((response) => {
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
