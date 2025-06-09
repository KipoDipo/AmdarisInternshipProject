import axios from "axios";

export const baseURL = 'https://localhost:7214/'

export const fetcher = axios.create({
    baseURL: baseURL,
    timeout: 10000,
})

export const getWithParams = async (endpoint: string, params: unknown = {}) => {
    const response = await fetcher.get(endpoint, { params });
    return response.data;
};

export const fetchPaged = (endpoint: string, pageNumber: number = 1, pageSize: number = 10, extraParams: Record<string, unknown> = {}) => {
    return getWithParams(endpoint, {
        pageNumber,
        pageSize,
        ...extraParams
    });
};

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
        if (error.response && (error.response.status === 401 || error.response.status === 403)) {
            localStorage.removeItem("token");
            window.location.href = "/";
        }
        return Promise.reject(error);
    }
);