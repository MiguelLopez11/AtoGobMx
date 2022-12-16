import mem from 'mem'

import { axiosPublic } from './axiosPublic'

const refreshTokenFn = async () => {
  const Token = JSON.parse(localStorage.getItem('session'))

  try {
    const response = await axiosPublic.post('/Authenticate/refresh-token', {
      accessToken: Token?.token,
      refreshToken: Token?.refreshToken
    })

    const { accessToken } = response.data

    if (!accessToken) {
      sessionStorage.removeItem('Token')
      sessionStorage.removeItem('RefreshToken')
    }

    sessionStorage.setItem('Token', JSON.stringify(accessToken))

    return accessToken
  } catch (error) {
    localStorage.removeItem('session')
    localStorage.removeItem('user')
  }
}

const maxAge = 10000

export const memoizedRefreshToken = mem(refreshTokenFn, {
  maxAge
})
