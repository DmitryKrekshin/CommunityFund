import commonHttpClient from "./CommonHttpClient";

const API_BASE_URL = "https://localhost:8083/api/v1";

export interface ContributionSettings {
  id: number;
  guid: string;
  personGuid?: string;
  amount: number;
  dateFrom: Date;
  dateTo?: Date;
}

export const getCommonContributionSettings = async (): Promise<ContributionSettings[]> => {
  const response = await commonHttpClient.get(`${API_BASE_URL}/ContributionSettings/Common`);
  return response.data;
}

export const getPersonalContributionSettings = async (): Promise<ContributionSettings[]> => {
  const response = await commonHttpClient.get(`${API_BASE_URL}/ContributionSettings/Personal`);
  return response.data;
}

export const createContributionSettings = async (settings: ContributionSettings): Promise<void> => {
  await commonHttpClient.post(`${API_BASE_URL}/ContributionSettings`, settings);
};

export const updateContributionSettings = async (settings: ContributionSettings): Promise<void> => {
  await commonHttpClient.put(`${API_BASE_URL}/ContributionSettings`, settings);
}
