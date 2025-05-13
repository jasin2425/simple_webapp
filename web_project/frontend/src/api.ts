// src/api.ts
import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7186/api',
});

export default api;
