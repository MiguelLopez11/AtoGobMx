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
  // Documentos Vehiculo
  const getDocumentsVehicle = (vehiculoId, callback) => {
    axiosPrivate.get(`/Archivos/DocumentosVehiculo/${vehiculoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createDocumentsVehicle = (vehiculoId, files, callback) => {
    axiosPrivate.post(`/Archivos/DocumentosVehiculo/${vehiculoId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }
  const deleteDocumentVehicle = (vehiculoId, ArchivoId, callback) => {
    axiosPrivate.delete(`/Archivos/DocumentosVehiculo/Eliminar/${vehiculoId}/${ArchivoId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  // Documentos Mobiliario
  const getDocumentsFurniture = (MobiliarioId, callback) => {
    axiosPrivate.get(`/Archivos/DocumentosMobiliario/${MobiliarioId}`).then((response) => {
      callback(response.data)
    })
  }
  const createDocumentsFurniture = (MobiliarioId, files, callback) => {
    axiosPrivate.post(`/Archivos/DocumentosMobiliario/${MobiliarioId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }
  const deleteDocumentFurniture = (MobiliarioId, ArchivoId, callback) => {
    axiosPrivate.delete(`/Archivos/DocumentosMobiliario/Eliminar/${MobiliarioId}/${ArchivoId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  // Documentos Armeria
  const getDocumentsArmary = (ArmaId, callback) => {
    axiosPrivate.get(`/Archivos/DocumentosArmeria/${ArmaId}`).then((response) => {
      callback(response.data)
    })
  }
  const createDocumentsArmary = (ArmaId, files, callback) => {
    axiosPrivate.post(`/Archivos/DocumentosArmeria/${ArmaId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }
  const deleteDocumentArmary = (ArmaId, ArchivoId, callback) => {
    axiosPrivate.delete(`/Archivos/DocumentosArmeria/Eliminar/${ArmaId}/${ArchivoId}`)
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
    axiosPrivate.get(`/Archivos/DocumentosAlumbradoPublico/${AlumbradoId}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteDocumentsAlumbrado = (AlumbradoId, ArchivoId, callback) => {
    axiosPrivate.delete(`/Archivos/DocumentosAlumbradoPublico/Eliminar/${AlumbradoId}/${ArchivoId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  const createDocumentsAlumbrado = (AlumbradoId, files, callback) => {
    axiosPrivate.post(`/Archivos/DocumentosAlumbradoPublico/${AlumbradoId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }
  // ----------- Cementerios ---------------- //
  const getDocumentsCementerios = (CementerioId, callback) => {
    axiosPrivate.get(`/Archivos/DocumentosCementerioPublico/${CementerioId}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteDocumentsCementerios = (CementerioId, ArchivoId, callback) => {
    axiosPrivate.delete(`/Archivos/DocumentosCementerio/Eliminar/${CementerioId}/${ArchivoId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  const createDocumentsCementerios = (CementerioId, files, callback) => {
    axiosPrivate.post(`/Archivos/DocumentosCementerioPublico/${CementerioId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }
  // ----------- Aseo ---------------- //
  const getDocumentsAseo = (AseoId, callback) => {
    axiosPrivate.get(`/Archivos/DocumentosAseoPublico/${AseoId}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteDocumentsAseo = (AseoId, ArchivoId, callback) => {
    axiosPrivate.delete(`/Archivos/DocumentosAseoPublico/Eliminar/${AseoId}/${ArchivoId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  const createDocumentsAseo = (AseoId, files, callback) => {
    axiosPrivate.post(`/Archivos/DocumentosAseoPublico/${AseoId}`, files).then((response) => {
      callback(response)
    }).catch((exception) => {
      callback(exception.response.data)
    })
  }
  // ----------- Obras publicas ---------------- //
  const getDocumentsObrasPublicas = (ObraId, callback) => {
    axiosPrivate.get(`/Archivos/DocumentosObrasPublicas/${ObraId}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteDocumentsObrasPublicas = (ObraId, ArchivoId, callback) => {
    axiosPrivate.delete(`/Archivos/DocumentosObrasPublicas/Eliminar/${ObraId}/${ArchivoId}`)
      .then((response) => {
        callback(response.data)
      })
      .catch((exception) => {
        callback(exception.response)
      })
  }
  const createDocumentsObrasPublicas = (ObraId, files, callback) => {
    axiosPrivate.post(`/Archivos/DocumentosObrasPublicas/${ObraId}`, files).then((response) => {
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
    // Vehiculos
    getDocumentsVehicle,
    createDocumentsVehicle,
    deleteDocumentVehicle,
    // Mobiliario
    getDocumentsFurniture,
    createDocumentsFurniture,
    deleteDocumentFurniture,
    // Armeria
    getDocumentsArmary,
    createDocumentsArmary,
    deleteDocumentArmary,
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
