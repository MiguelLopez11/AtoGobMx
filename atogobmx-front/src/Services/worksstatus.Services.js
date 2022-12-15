import axios from 'axios'

export default function WorksStatusServices () {
  const getWorksStatus = (callback) => {
    axios.get('http://localhost:5000/api/OP_EstatusObras').then((response) => {
      callback(response.data)
    })
  }

  const getWorksStatusById = (estatusObraId, callback) => {
    axios
      .get(`https://localhost:5000/api/OP_EstatusObras/${estatusObraId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createWorksStatus = (data, callback) => {
    axios.post('http://localhost:5000/api/OP_EstatusObras', data).then((response) => {
      callback(response.data)
    })
  }

  const updateWorksStatus = (data, callback) => {
    axios.put(`https://localhost:5000/api/OP_EstatusObras/${data.estatusObraId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deleteWorksStatus = (estatusObraId, callback) => {
    axios.delete(`https://localhost:5000/api/OP_EstatusObras/${estatusObraId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getWorksStatus,
    getWorksStatusById,
    createWorksStatus,
    updateWorksStatus,
    deleteWorksStatus
  }
}
