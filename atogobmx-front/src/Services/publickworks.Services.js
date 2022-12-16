import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PublicWorksServices () {
  const getPublicWorks = (callback) => {
    axiosPrivate.get('/ObrasPublicas').then((response) => {
      callback(response.data)
    })
  }

  const getPublicWorksById = (obraId, callback) => {
    axiosPrivate
      .get(`/ObrasPublicas/${obraId}`)
      .then((response) => {
        callback(response.data)
      })
  }

  const createPublicWorks = (data, callback) => {
    axiosPrivate.post('/ObrasPublicas', data).then((response) => {
      callback(response.data)
    })
  }

  const updatePublicWorks = (data, callback) => {
    axiosPrivate.put(`/ObrasPublicas/${data.obraId}`, data).then((response) => {
      callback(response.data)
    })
  }

  const deletePublicWorks = (obraId, callback) => {
    axiosPrivate.delete(`/ObrasPublicas/${obraId}`).then((response) => {
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
