import commonHttpClient from "./CommonHttpClient";

const API_BASE_URL = "https://localhost:8083/api/v1";

export const getPersonInfo = async (personGuid: string) => {
  const response = await commonHttpClient.get(`${API_BASE_URL}/Person/${personGuid}`);
  return response.data;
};
