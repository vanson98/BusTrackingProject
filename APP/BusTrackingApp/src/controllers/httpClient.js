import axios from 'axios';

const client = axios.create({
  baseURL: 'http://192.168.0.104:5005/api',
  headers: { 'content-type': 'application/json' },
});

client.interceptors.request.use(
  config => config,
  error => {
    console.warn('Failed to make request with error:', error);
    return Promise.reject(error);
  }
);

client.interceptors.response.use(
  response => response.data,
  error => {
    if (!error.response) {
      throw new Error(error);
    }

    console.warn('Request got response with error:', error);

    return Promise.reject(error);
  }
);

export default client;
