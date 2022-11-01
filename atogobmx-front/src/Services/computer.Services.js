import axios from 'axios'

export default function ComputerServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const getComputers = (callback) => {
    axios.get(`${BaseUrl}/EquiposComputos`).then((response) => {
      callback(response.data)
    })
  }
  const getComputer = (EquipoComputoId, callback) => {
    axios.get(`${BaseUrl}/EquiposComputos/${EquipoComputoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createComputer = (data, callback) => {
    axios.post(`${BaseUrl}/EquiposComputos`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateComputer = (data, callback) => {
    axios.put(`${BaseUrl}/EquiposComputos/${data.equipoComputoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteComputer = (EquipoComputoId, callback) => {
    axios.delete(`${BaseUrl}/EquiposComputos/${EquipoComputoId}`).then((response) => {
      callback(response.data)
    })
  }
  // ESTATUS
  const getStatus = (callback) => {
    axios.get(`${BaseUrl}/PAT_EstatusEquipo`).then((response) => {
      callback(response.data)
    })
  }
  const getStatusById = (estatusEquipoId, callback) => {
    axios.get(`${BaseUrl}/PAT_EstatusEquipo/${estatusEquipoId}`).then((response) => {
      callback(response.data)
    })
  }
  const createStatusComputer = (data, callback) => {
    axios.post(`${BaseUrl}/PAT_EstatusEquipo`, data).then((response) => {
      callback(response.data)
    })
  }
  const updateStatusComputer = (data, callback) => {
    axios.put(`${BaseUrl}/PAT_EstatusEquipo/${data.estatusEquipoId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteStatusComputer = (estatusEquipoId, callback) => {
    axios.delete(`${BaseUrl}/PAT_EstatusEquipo/${estatusEquipoId}`).then((response) => {
      callback(response.data)
    })
  }
  // DISPLAY
  const getDisplays = (callback) => {
    axios.get(`${BaseUrl}/PAT_Monitor`).then((response) => {
      callback(response.data)
    })
  }
  const createDisplay = (data, callback) => {
    axios.post(`${BaseUrl}/PAT_Monitor`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteDisplay = (monitorId, callback) => {
    axios.delete(`${BaseUrl}/PAT_Monitor/${monitorId}`).then((response) => {
      callback(response.data)
    })
  }
  // Keyboards
  const getKeyboard = (callback) => {
    axios.get(`${BaseUrl}/PAT_Teclado`).then((response) => {
      callback(response.data)
    })
  }
  const createKeyboard = (data, callback) => {
    axios.post(`${BaseUrl}/PAT_Teclado`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteKeyboard = (tecladoId, callback) => {
    axios.delete(`${BaseUrl}/PAT_Teclado/${tecladoId}`).then((response) => {
      callback(response.data)
    })
  }
  // Mouse
  const getMouses = (callback) => {
    axios.get(`${BaseUrl}/PAT_Mouse`).then((response) => {
      callback(response.data)
    })
  }
  const createMouse = (data, callback) => {
    axios.post(`${BaseUrl}/PAT_Mouse`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteMouse = (mouseId, callback) => {
    axios.delete(`${BaseUrl}/PAT_Mouse/${mouseId}`).then((response) => {
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
