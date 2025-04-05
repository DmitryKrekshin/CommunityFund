import axios from "axios";

const API_BASE_URL = "https://localhost:8081/api/v1"; // Укажи URL backend

export const authHttpClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {"Content-Type": "application/json"},
});

export const login = async (login: string, password: string) => {
  const response = await authHttpClient.post("/Authentication/Authorize", {login, password});
  return response.data;
};
