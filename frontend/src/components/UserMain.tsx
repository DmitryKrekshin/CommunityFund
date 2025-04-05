import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

function userMain() {
  let personName;
  const personInfo = localStorage.getItem("personInfo");
  if (personInfo) {
    const person = JSON.parse(personInfo);
    personName = `${person.surname} ${person.name} ${person.patronymic}`;
  }

  return (
    <div className="container mb-4">
      <div className="text-center mb-4">
        <h2>Добро пожаловать, <span id="userName">{personName}</span>!</h2>
      </div>

      <div id="paymentInfo">
        <div className="alert alert-danger" id="debtAlert">
          <i className="fas fa-exclamation-triangle"></i> У вас есть задолженность за предыдущий период <strong>5000 ₽</strong>! Пожалуйста, внесите
          оплату до <strong>20.03.2025</strong>.
          В случае не внесения оплаты вы будете автоматически исключены.
        </div>
        <div className="alert alert-info" id="nextPayment">
          <i className="fas fa-calendar-alt"></i> Ближайший платеж: <strong id="paymentDate">20 апреля 2025</strong>, сумма: <strong
          id="paymentAmount">5000 ₽</strong>.
        </div>
      </div>

      <div className="row">
        <div className="col-md-4">
          <a href="#" className="text-decoration-none">
            <div className="card text-center">
              <div className="card-body">
                <i className="fas fa-wallet fa-3x mb-3 text-primary"></i>
                <h5 className="card-title">История платежей</h5>
              </div>
            </div>
          </a>
        </div>

        <div className="col-md-4">
          <a href="#" className="text-decoration-none">
            <div className="card text-center">
              <div className="card-body">
                <i className="fas fa-bell fa-3x mb-3 text-warning"></i>
                <h5 className="card-title">История уведомлений</h5>
              </div>
            </div>
          </a>
        </div>

        <div className="col-md-4">
          <a href="#" className="text-decoration-none">
            <div className="card text-center">
              <div className="card-body">
                <i className="fas fa-cogs fa-3x mb-3 text-secondary"></i>
                <h5 className="card-title">Настройки</h5>
              </div>
            </div>
          </a>
        </div>
      </div>
    </div>
  );
}

export default userMain;
