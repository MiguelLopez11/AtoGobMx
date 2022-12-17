import mem from 'mem'

import { axiosPublic } from './axiosPublic'

const refreshTokenFn = async () => {
  const token = sessionStorage.getItem('Token')
  const refreshTokens = sessionStorage.getItem('RefreshToken')

  try {
    const response = await axiosPublic.post('/Authenticate/refresh-token', {
      accessToken: token,
      refreshToken: refreshTokens
    })

    const { accessToken, refreshToken } = response.data
    if (!accessToken && !refreshToken) {
      sessionStorage.removeItem('Token')
      sessionStorage.removeItem('RefreshToken')
    }

    sessionStorage.setItem('Token', accessToken)
    sessionStorage.setItem('RefreshToken', refreshToken)

    return { accessToken: accessToken, refres: refreshToken }
  } catch (error) {
    sessionStorage.removeItem('session')
    sessionStorage.removeItem('user')
  }
}

const maxAge = 1000000

export const memoizedRefreshToken = mem(refreshTokenFn, {
  maxAge
})
