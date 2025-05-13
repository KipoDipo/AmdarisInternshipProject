import axios from "axios";

export const baseURL = 'https://localhost:7214/' 

export const fetcher = axios.create({
    baseURL: baseURL,
    timeout: 5000,
})

fetcher.interceptors.request.use(
    config => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        } else {
            delete config.headers['Authorization'];
        }
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);

fetcher.interceptors.response.use(
  response => response,
  error => {
    if (error.response && error.response.status === 401) {
        localStorage.removeItem("token");
        window.location.href = "/";
    }
    return Promise.reject(error);
  }
);