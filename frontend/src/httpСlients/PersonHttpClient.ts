import commonHttpClient from "./CommonHttpClient";

const API_BASE_URL = "https://localhost:8083/api/v1";

export interface Person {
  id: number;
  guid: string;
  surname: string;
  name: string;
  patronymic: string;
  email: string;
  phoneNumber: string;
  isExcluded: boolean;
}

export const getPersonInfo = async (personGuid: string) => {
  const response = await commonHttpClient.get(`${API_BASE_URL}/Person/${personGuid}`);
  return response.data.person;
};

export const getPersons = async (): Promise<Person[]> => {
  const response = await commonHttpClient.get(`${API_BASE_URL}/Person`);
  return response.data;
};

export const createPerson = async (person: Person): Promise<void> => {
  await commonHttpClient.post(`${API_BASE_URL}/Person`, person);
};

export const updatePerson = async (person: Person): Promise<void> => {
  await commonHttpClient.put(`${API_BASE_URL}/Person/${person.guid}`, person);
};
