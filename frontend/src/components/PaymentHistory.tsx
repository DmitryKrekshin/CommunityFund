import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

function paymentHistory() {
  return (
    <div className="container">
      <h2 className="mb-4 text-center">История платежей</h2>

      <div className="row mb-3">
        <div className="col-md-6">
          <input type="text" id="searchInput" className="form-control" placeholder="🔍 Поиск по платежам..."/>
        </div>
        <div className="col-md-6">
          <select id="statusFilter" className="form-select">
            <option value="">Все статусы</option>
            <option value="paid">Оплачено</option>
            <option value="unpaid">Не оплачено</option>
          </select>
        </div>
      </div>

      <table className="table table-striped table-hover">
        <thead className="table-dark">
        <tr>
          <th>Дата</th>
          <th>Сумма</th>
          <th>Статус</th>
          <th>Квитанция</th>
        </tr>
        </thead>
        <tbody id="paymentTable">
        <tr>
          <td>05.03.2025</td>
          <td>5000 ₽</td>
          <td className="status-paid">Оплачено</td>
          <td><a href="#" className="btn btn-sm btn-outline-success"><i className="fas fa-download"></i> Скачать</a></td>
        </tr>
        <tr>
          <td>05.02.2025</td>
          <td>5000 ₽</td>
          <td className="status-paid">Оплачено</td>
          <td><a href="#" className="btn btn-sm btn-outline-success"><i className="fas fa-download"></i> Скачать</a></td>
        </tr>
        <tr>
          <td>05.01.2025</td>
          <td>5000 ₽</td>
          <td className="status-unpaid">Не оплачено</td>
          <td>-</td>
        </tr>
        </tbody>
      </table>
    </div>
  );
}

export default paymentHistory;
