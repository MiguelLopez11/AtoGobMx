import axios from 'axios'

export default function statusServices () {
  const getStatus = (callback) => {
    axios.get('http://localhost:5000/api/EstatusAlumbrado').then((response) => {
      callback(response.data)
    })
  }

  const getStatusById = (estatusId, callback) => {
    axios
      .get(`https://localhost:5000/api/EstatusAlumbrado/${estatusId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createStatusLighting = (data, callback) => {
    axios.post('http://localhost:5000/api/EstatusAlumbrado', data).then((response) => {
      callback(response.data)
    })
  }

  const updateStatusLighting = (data, callback) => {
    axios.put(`https://localhost:5000/api/EstatusAlumbrado/${data.estatusAlumbradoId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteStatusLighting = (estatusId, callback) => {
    axios.delete(`https://localhost:5000/api/EstatusAlumbrado/${estatusId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getStatus,
    getStatusById,
    createStatusLighting,
    updateStatusLighting,
    deleteStatusLighting
  }
}
