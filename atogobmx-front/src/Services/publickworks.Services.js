import axios from 'axios'

export default function PublicWorksServices () {
  const getPublicWorks = (callback) => {
    axios.get('https://localhost:7065/api/ObrasPublicas').then((response) => {
      callback(response.data)
    })
  }

  const getPublicWorksById = (obraId, callback) => {
    axios
      .get(`https://localhost:7065/api/ObrasPublicas/${obraId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createPublicWorks = (data, callback) => {
    axios.post('https://localhost:7065/api/ObrasPublicas', data).then((response) => {
      callback(response.data)
    })
  }

  const updatePublicWorks = (data, callback) => {
    axios.put(`https://localhost:7065/api/ObrasPublicas/${data.obraId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deletePublicWorks = (obraId, callback) => {
    axios.delete(`https://localhost:7065/api/ObrasPublicas/${obraId}`).then((response) => {
      callback(response.data)
    })
  }

  return {
    getPublicWorks,
    getPublicWorksById,
    createPublicWorks,
    updatePublicWorks,
    deletePublicWorks
  }
}
