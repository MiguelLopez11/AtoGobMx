import axios from 'axios'

export const axiosPublic = axios.create({
  baseURL: 'https://digitalatogobmxwebservices.somee.com/api',
  headers: {
    'Content-Type': 'application/json'
  }
})
