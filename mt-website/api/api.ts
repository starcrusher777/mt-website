import axios from 'axios';

const API_URL = 'http://http://192.168.0.14/:3000/api';

export const getAllAds = () => axios.get(`${API_URL}/Item/GetItems`);
export const getAdById = (id: string) => axios.get(`${API_URL}/Item/GetItem/1?itemId=/${id}`);
export const createAd = (data: any) => axios.post(`${API_URL}/Item/CreateItem`, data);