import axios from 'axios'

export default function AreaServices () {
  const BaseUrl = 'http://localhost:5000/api'
  const LogIn = (user, callback) => {
    axios.post(`${BaseUrl}/Authenticate/login`, user).then((response) => {
      callback(response.data)
    }).catch((exception) => {
      callback(exception)
    })
  }
  return {
    LogIn
  }
}
