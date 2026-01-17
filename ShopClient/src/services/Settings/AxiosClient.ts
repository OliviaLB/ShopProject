import axios from 'axios';

export const client = () => {
  const baseURL = import.meta.env.VITE_API;

  const instance = axios.create({
    baseURL,
    headers: {
      'Content-Type': 'application/json',
      Accept: '*/*'
    }
  });

  return instance;
};
