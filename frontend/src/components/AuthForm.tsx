import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import {useForm} from "react-hook-form";

interface AuthFormProps {
  onSubmit: (data: { login: string; password: string }) => void;
  error?: string | null;
}

const AuthForm: React.FC<AuthFormProps> = ({onSubmit, error}) => {
  const {register, handleSubmit} = useForm<{ login: string; password: string }>();

  return (
    <section className="login-section py-5">
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-md-6">
            <div className="card shadow-lg p-4">
              <h3 className="text-center">Вход в систему</h3>
              <form onSubmit={handleSubmit(onSubmit)}>
                <div className="mb-3">
                  <label className="form-label">Email</label>
                  <input {...register("login")} type="text" className="form-control" placeholder="Введите логин" required/>
                </div>
                <div className="mb-3">
                  <label className="form-label">Пароль</label>
                  <input {...register("password")} type="password" className="form-control" placeholder="Введите пароль" required/>
                </div>
                {error && <p className="text-red-500 text-sm text-center">{error}</p>}
                <button type="submit" className="btn btn-primary w-100">Войти</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </section>
  )
}

export default AuthForm;
