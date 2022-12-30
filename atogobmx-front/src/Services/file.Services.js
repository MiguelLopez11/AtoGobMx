import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function FileServices () {
  const getExpedientPhotoProfile = (ExpedienteDigitalId, callback) => {
    axiosPrivate.get(`/Archivos/FotoPerfil/${ExpedienteDigitalId}`).then((response) => {
      callback(response)
    })
  }
  const getDocuments = (ExpedienteDigitalId, callback) => {
    axiosPrivate.get(`/Archivos/Documentos/${ExpedienteDigitalId}`).then((response) => {
      callback(response.data)
    })
  }
  const createExpedientPhotoProfile = (expedienteDigitalId, file, callback) => {
    axiosPrivate.post(`/Archivos/Imagen/${expedienteDigitalId}`, file).then((response) => {
      callback(response)
    }).catch((exception) => {
      // eslint-disable-next-line node/no-callback-literal
      callback({ message: exception.response, type: exception.response.status })
    })
  }
  const createDocuments = (expedienteDigitalId, files, callback) => {
    axiosPrivate.post(`/Archivos/Documentos/${expedienteDigitalId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }
  const deleteDocument = (ExpedienteDigitalId, ArchivoId, callback) => {
    axiosPrivate.delete(`/Archivos/Documentos/Eliminar/${ExpedienteDigitalId}/${ArchivoId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  // Documentos Equipos Computo
  const getDocumentsEquipmentComputer = (EquipoComputoId, callback) => {
    axiosPrivate.get(`/Archivos/DocumentosEquiposComputo/${EquipoComputoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createDocumentsEquipmentComputer = (EquipoComputoId, files, callback) => {
    axiosPrivate.post(`/Archivos/DocumentosEquiposComputo/${EquipoComputoId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }
  const deleteDocumentEquipmentComputer = (EquipoComputoId, ArchivoId, callback) => {
    axiosPrivate.delete(`/Archivos/DocumentosEquiposComputo/Eliminar/${EquipoComputoId}/${ArchivoId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  // Servicios creados para la carga de archivos arturo
  // ----------- Alumbrado ---------------- //
  const getDocumentsAlumbrado = (AlumbradoId, callback) => {
    axiosPrivate.get(`/Archivos/Documents/AlumbradoPublico/${AlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteDocumentsAlumbrado = (AlumbradoId, ArchivoAlumbradoId, callback) => {
    axiosPrivate.delete(`/Archivos/Documents11/AlumbradoPublico/Eliminar/${AlumbradoId}/${ArchivoAlumbradoId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  const createDocumentsAlumbrado = (AlumbradoId, files, callback) => {
    axiosPrivate.post(`/Archivos/Documents/AlumbradoPublico/${AlumbradoId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }
  // ----------- Cementerios ---------------- //
  const getDocumentsCementerios = (DireccionId, callback) => {
    axiosPrivate.get(`/Archivos/Documents/DireccionCementerio/${DireccionId}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteDocumentsCementerios = (DireccionId, ArchivosCementerioId, callback) => {
    axiosPrivate.delete(`/Archivos/Documents12/Cementerios/Eliminar/${DireccionId}/${ArchivosCementerioId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  const createDocumentsCementerios = (DireccionId, files, callback) => {
    axiosPrivate.post(`/Archivos/Documents/DireccionCementerio/${DireccionId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }
  // ----------- Aseo ---------------- //
  const getDocumentsAseo = (AseoId, callback) => {
    axiosPrivate.get(`/Archivos/Documents/Aseo/${AseoId}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteDocumentsAseo = (AseoId, ArchivosAseoId, callback) => {
    axiosPrivate.delete(`/Archivos/Documents13/Aseo/Eliminar/${AseoId}/${ArchivosAseoId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  const createDocumentsAseo = (AseoId, files, callback) => {
    axiosPrivate.post(`/Archivos/Documents/Aseo/${AseoId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }
  // ----------- Obras publicas ---------------- //
  const getDocumentsObrasPublicas = (ObraId, callback) => {
    axiosPrivate.get(`/Archivos/Documents/ObrasPublicas/${ObraId}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteDocumentsObrasPublicas = (ObraId, ArchivosObrasId, callback) => {
    axiosPrivate.delete(`/Archivos/Documents14/ObrasPublicas/Eliminar/${ObraId}/${ArchivosObrasId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  const createDocumentsObrasPublicas = (ObraId, files, callback) => {
    axiosPrivate.post(`/Archivos/Documents/Obraspublicas/${ObraId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }

  return {
    getExpedientPhotoProfile,
    getDocuments,
    createExpedientPhotoProfile,
    createDocuments,
    deleteDocument,
    // EquipoComputo
    getDocumentsEquipmentComputer,
    createDocumentsEquipmentComputer,
    deleteDocumentEquipmentComputer,
    // -------Arturo--------- //
    getDocumentsAlumbrado,
    deleteDocumentsAlumbrado,
    createDocumentsAlumbrado,
    getDocumentsCementerios,
    deleteDocumentsCementerios,
    createDocumentsCementerios,
    getDocumentsAseo,
    deleteDocumentsAseo,
    createDocumentsAseo,
    getDocumentsObrasPublicas,
    deleteDocumentsObrasPublicas,
    createDocumentsObrasPublicas
  }
}
