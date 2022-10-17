import axios from 'axios'

export default function AreaServices () {
  const BaseUrl = 'https://localhost:7065/api'
  const LogIn = (user, callback) => {
    axios.get(`${BaseUrl}/Usuarios/${user.userName}/${user.password}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    LogIn
  }
}
