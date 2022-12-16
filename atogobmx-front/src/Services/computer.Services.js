import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ComputerServices () {
  const getComputers = (callback) => {
    axiosPrivate.get('/EquiposComputos').then((response) => {
      callback(response.data)
    })
  }
  const getComputer = (EquipoComputoId, callback) => {
    axiosPrivate.get(`/EquiposComputos/${EquipoComputoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createComputer = (data, callback) => {
    axiosPrivate.post('/EquiposComputos', data).then((response) => {
      callback(response.data)
    })
  }
  const updateComputer = (data, callback) => {
    axiosPrivate.put(`/EquiposComputos/${data.equipoComputoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteComputer = (EquipoComputoId, callback) => {
    axiosPrivate.delete(`/EquiposComputos/${EquipoComputoId}`).then((response) => {
      callback(response.data)
    })
  }
  // ESTATUS
  const getStatus = (callback) => {
    axiosPrivate.get('/PAT_EstatusEquipo').then((response) => {
      callback(response.data)
    })
  }
  const getStatusById = (estatusEquipoId, callback) => {
    axiosPrivate.get(`/PAT_EstatusEquipo/${estatusEquipoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createStatusComputer = (data, callback) => {
    axiosPrivate.post('/PAT_EstatusEquipo', data).then((response) => {
      callback(response.data)
    })
  }
  const updateStatusComputer = (data, callback) => {
    axiosPrivate.put(`/PAT_EstatusEquipo/${data.estatusEquipoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteStatusComputer = (estatusEquipoId, callback) => {
    axiosPrivate.delete(`/PAT_EstatusEquipo/${estatusEquipoId}`).then((response) => {
      callback(response.data)
    })
  }
  // DISPLAY
  const getDisplays = (callback) => {
    axiosPrivate.get('/PAT_Monitor').then((response) => {
      callback(response.data)
    })
  }
  const createDisplay = (data, callback) => {
    axiosPrivate.post('/PAT_Monitor', data).then((response) => {
      callback(response.data)
    })
  }
  const deleteDisplay = (monitorId, callback) => {
    axiosPrivate.delete(`/PAT_Monitor/${monitorId}`).then((response) => {
      callback(response.data)
    })
  }
  // Keyboards
  const getKeyboard = (callback) => {
    axiosPrivate.get('/PAT_Teclado').then((response) => {
      callback(response.data)
    })
  }
  const createKeyboard = (data, callback) => {
    axiosPrivate.post('/PAT_Teclado', data).then((response) => {
      callback(response.data)
    })
  }
  const deleteKeyboard = (tecladoId, callback) => {
    axiosPrivate.delete(`/PAT_Teclado/${tecladoId}`).then((response) => {
      callback(response.data)
    })
  }
  // Mouse
  const getMouses = (callback) => {
    axiosPrivate.get('/PAT_Mouse').then((response) => {
      callback(response.data)
    })
  }
  const createMouse = (data, callback) => {
    axiosPrivate.post('/PAT_Mouse', data).then((response) => {
      callback(response.data)
    })
  }
  const deleteMouse = (mouseId, callback) => {
    axiosPrivate.delete(`/PAT_Mouse/${mouseId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getComputers,
    getComputer,
    createComputer,
    updateComputer,
    deleteComputer,
    // ESTATUS
    getStatus,
    getStatusById,
    createStatusComputer,
    updateStatusComputer,
    deleteStatusComputer,
    // Displays
    getDisplays,
    createDisplay,
    deleteDisplay,
    // Keyboard
    getKeyboard,
    createKeyboard,
    deleteKeyboard,
    // Mouse
    getMouses,
    createMouse,
    deleteMouse
  }
}
