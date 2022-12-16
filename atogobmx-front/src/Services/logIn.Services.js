import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function AreaServices () {
  const LogIn = (user, callback) => {
    axiosPrivate.post('/Authenticate/login', user).then((response) => {
      callback(response.data)
    }).catch((exception) => {
      callback(exception)
    })
  }
  return {
    LogIn
  }
}
