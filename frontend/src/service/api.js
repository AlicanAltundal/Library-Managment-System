// src/service/api.js
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5050/api',
});

// 🔥 Her istekten önce token ekle
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
      console.log("🔹 Token header'a eklendi:", token);
    } else {
      console.warn("⚠️ Token bulunamadı, header eklenmedi!");
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export default api;
