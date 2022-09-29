import axios from 'axios'

export default function statusServices () {
  const getStatus = (callback) => {
    axios.get('https://localhost:7065/api/Estatus').then((response) => {
      callback(response.data)
    })
  }

  const getStatusById = (estatusId, callback) => {
    axios.get(`https://localhost:7065/api/Estatus/${estatusId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getStatus,
    getStatusById
  }
}
