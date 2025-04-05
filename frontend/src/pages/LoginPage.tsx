import React, {useState} from "react";
import {useNavigate} from "react-router-dom";
import AuthForm from "../components/AuthForm";
import {login} from "../httpСlients/AuthHttpClient";
import {getPersonInfo} from "../httpСlients/PersonHttpClient";

const LoginPage: React.FC = () => {
  const navigate = useNavigate();
  const [error, setError] = useState<string | null>(null);

  const handleLogin = async (data: { login: string; password: string }) => {
    try {
      const response = await login(data.login, data.password);
      localStorage.setItem("token", response.token);

      if (response.personGuid) {
        localStorage.setItem("personGuid", response.personGuid);
        const personInfo = await getPersonInfo(response.personGuid);
        localStorage.setItem("personInfo", JSON.stringify(personInfo));
      }

      navigate("/home");
    } catch (err) {
      setError("Ошибка авторизации. Проверьте данные.");
    }
  };

  return (
    <div className="flex items-center justify-center h-screen bg-gray-100">
      <AuthForm onSubmit={handleLogin} error={error}/>
    </div>
  );
};

export default LoginPage;
