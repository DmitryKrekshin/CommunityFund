import commonHttpClient from "./CommonHttpClient";

const API_BASE_URL = "https://localhost:8083/api/v1";

export interface Contribution {
  id: number;
  guid: string;
  payerGuid: string;
  amount: number;
  date: Date;
  paymentDate: Date;
}

export const get = async (): Promise<Contribution[]> => {
  const response = await commonHttpClient.get(`${API_BASE_URL}/Contribution`);
  return response.data;
}

export const getByPersonGuid = async (personGuid: string): Promise<Contribution[]> => {
  const response = await commonHttpClient.get(`${API_BASE_URL}/Contribution?personGuid=${personGuid}`);
  return response.data;
}

export const create = async (contribution: Contribution): Promise<void> => {
  await commonHttpClient.post(`${API_BASE_URL}/Contribution`, contribution);
}
