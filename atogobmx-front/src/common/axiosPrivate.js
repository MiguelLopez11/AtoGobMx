import axios from 'axios'
import { memoizedRefreshToken } from './refreshToken'

axios.defaults.baseURL = 'http://localhost:5000/api'

axios.interceptors.request.use(
  async (config) => {
    const token = sessionStorage.getItem('Token')
    if (token) {
      config.headers = {
        ...config.headers,
        authorization: `Bearer ${token}`
      }
    }

    return config
  },
  (error) => Promise.reject(error)
)

axios.interceptors.response.use(
  (response) => response,
  async (error) => {
    const config = error?.config

    if (error?.response?.status === 401 && !config?.sent) {
      config.sent = true

      const result = await memoizedRefreshToken()

      if (result) {
        config.headers = {
          ...config.headers,
          authorization: `Bearer ${result}`
        }
      }

      return axios(config)
    }
    return Promise.reject(error)
  }
)

export const axiosPrivate = axios
