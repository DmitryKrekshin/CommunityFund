import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

let user: { id: number, fullName: string, email: string, role: string, status: number } = {
  id: 1,
  fullName: "Иванов Иван Иванович",
  email: "IvanovIvan@example.com",
  role: "Пайщик",
  status: 1
};

interface UserInfoProps {
  userId?: number;
}

function userInfo({userId}: UserInfoProps) {
  return (
    <div className="container">
      <h2 className="mb-4">{userId === -1 ? "Добавить пользователя" : "Информация о пользователе"}</h2>

      <form id="userForm">
        <div className="mb-3">
          <label htmlFor="fullName" className="form-label">ФИО</label>
          <input type="text" className="form-control" id="fullName" placeholder="Введите ФИО" value={user.fullName} required/>
        </div>

        <div className="mb-3">
          <label htmlFor="email" className="form-label">Email</label>
          <input type="email" className="form-control" id="email" placeholder="Введите email" value={user.email} required/>
        </div>

        <div className="mb-3">
          <label htmlFor="role" className="form-label">Роль</label>
          <select className="form-select" id="role" value={user.role}>
            <option value="Администратор">Администратор</option>
            <option value="Бухгалтер">Бухгалтер</option>
            <option value="Пайщик">Пайщик</option>
          </select>
        </div>

        <div className="mb-3">
          <label htmlFor="status" className="form-label">Статус</label>
          <select className="form-select" id="status" value={user.status}>
            <option value="1">Активен</option>
            <option value="0">Заблокирован</option>
          </select>
        </div>

        <div className="mb-3">
          <label htmlFor="password" className="form-label">Пароль</label>
          <input type="password" className="form-control" id="password" placeholder="Введите пароль"/>
          <small className="text-muted">Оставьте пустым, если не хотите менять пароль.</small>
        </div>

        <button type="submit" className="btn btn-outline-primary"><i className="fas fa-save"></i> Сохранить</button>
        <a href="#" className="btn btn-outline-secondary ms-2"><i className="fas fa-times"></i> Отмена</a>
      </form>
    </div>
  );
}

export default userInfo;
