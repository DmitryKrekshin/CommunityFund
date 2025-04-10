import commonHttpClient from "./CommonHttpClient";

const API_BASE_URL = "https://localhost:8083/api/v1";

export const downloadFullReport = async () => {
  const response = await commonHttpClient.get(`${API_BASE_URL}/report/all`, {
    responseType: "blob", // важно: чтобы получить PDF как blob
  });

  // Создаем ссылку и инициируем скачивание
  const url = window.URL.createObjectURL(new Blob([response.data]));
  const link = document.createElement("a");
  link.href = url;
  link.setAttribute("download", `contributions_report.pdf`);
  document.body.appendChild(link);
  link.click();
  link.remove();
};
