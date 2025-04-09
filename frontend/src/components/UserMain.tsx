import React, {JSX, useEffect, useState} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useNavigate } from "react-router-dom";
import * as ContributionSettingsHttpClient from "../httpСlients/ContributionSettingsHttpClient";
import * as ContributionHttpClient from "../httpСlients/ContributionHttpClient";

const UserMain: React.FC = () => {
  const navigate = useNavigate();
  const personInfo = localStorage.getItem("personInfo");
  const person = personInfo ? JSON.parse(personInfo) : null;
  const personName = person ? `${person.surname} ${person.name} ${person.patronymic}` : "";
  const personGuid: string = person?.guid ?? "";

  const [loading, setLoading] = useState(true);
  const [alerts, setAlerts] = useState<JSX.Element[]>([]);

  useEffect(() => {
    const loadPaymentInfo = async () => {
      if (!personGuid) return;

      try {
        const personalSettings = await ContributionSettingsHttpClient.getPersonalContributionSettings();
        const commonSettings = await ContributionSettingsHttpClient.getCommonContributionSettings();
        const settings = personalSettings.find((s) => s.personGuid === personGuid) ?? commonSettings[0];

        if (!settings) return;

        const contributions = await ContributionHttpClient.getByPersonGuid(personGuid);
        const paidDates = contributions.map((c) => new Date(c.date).toDateString());

        const now = new Date();
        const twoWeeksLater = new Date();
        twoWeeksLater.setDate(now.getDate() + 14);

        const startDate = new Date(settings.dateFrom);
        const endDate = settings.dateTo ? new Date(settings.dateTo) : new Date(now.getFullYear() + 10, 0, 1);

        const upcomingPayments = [];
        const overduePayments = [];

        for (let d = new Date(startDate); d <= endDate; d.setMonth(d.getMonth() + 1)) {
          const dueDate = new Date(d);
          const isPaid = paidDates.includes(dueDate.toDateString());

          if (dueDate < now && !isPaid) {
            let dueDatePlus14 = new Date(dueDate);
            dueDatePlus14.setDate(dueDate.getDate() + 14);
            overduePayments.push({ date: new Date(dueDatePlus14), amount: settings.amount });
          }

          if (dueDate >= now && dueDate <= twoWeeksLater && !isPaid) {
            upcomingPayments.push({ date: new Date(dueDate), amount: settings.amount });
          }
        }

        const generatedAlerts: JSX.Element[] = [];

        if (overduePayments.length > 0) {
          const latestOverdue = overduePayments[overduePayments.length - 1];
          generatedAlerts.push(
            <div className="alert alert-danger" key="overdue">
              <i className="fas fa-exclamation-triangle"></i> У вас есть задолженность за предыдущий период <strong>{latestOverdue.amount.toLocaleString()} ₽</strong>!
              Пожалуйста, внесите оплату до <strong>{latestOverdue.date.toLocaleDateString()}</strong>. В случае не внесения оплаты вы будете автоматически исключены.
              <br/>
              <button onClick={() => navigate("/payment")} className="btn btn-sm btn-warning mt-2">Оплатить сейчас</button>
            </div>
          );
        }

        if (upcomingPayments.length > 0) {
          const nearestUpcoming = upcomingPayments[0];
          generatedAlerts.push(
            <div className="alert alert-info" key="upcoming">
              <i className="fas fa-calendar-alt"></i> Очередной платеж: <strong>{nearestUpcoming.date.toLocaleDateString()}</strong>, сумма: <strong>{nearestUpcoming.amount.toLocaleString()} ₽</strong>.
              <br/>
              <button onClick={() => navigate("/payment")} className="btn btn-sm btn-outline-primary mt-2">Перейти к оплате</button>
            </div>
          );
        }

        setAlerts(generatedAlerts);
      } finally {
        setLoading(false);
      }
    };

    loadPaymentInfo();
  }, [personGuid, navigate]);

  return (
    <div className="container mb-4">
      <div className="text-center mb-4">
        <h2>Добро пожаловать, <span id="userName">{personName}</span>!</h2>
      </div>

      <div id="paymentInfo">
        {loading ? <div>Загрузка...</div> : alerts.length > 0 ? alerts : (
          <div className="alert alert-success">
            У вас нет задолженностей. Все платежи внесены своевременно!
          </div>
        )}
      </div>

      <div className="row">
        <div className="col-md-6">
          <button onClick={() => navigate("/personalContributions")} style={{display: "contents"}} className="text-decoration-none">
            <div className="card text-center">
              <div className="card-body">
                <i className="fas fa-wallet fa-3x mb-3 text-primary"></i>
                <h5 className="card-title">История платежей</h5>
              </div>
            </div>
          </button>
        </div>

        <div className="col-md-6">
          <button onClick={() => navigate(`/person/edit/${personGuid}`)} style={{display: "contents"}} className="text-decoration-none">
            <div className="card text-center">
              <div className="card-body">
                <i className="fas fa-cogs fa-3x mb-3 text-secondary"></i>
                <h5 className="card-title">Настройки</h5>
              </div>
            </div>
          </button>
        </div>
      </div>
    </div>
  );
};

export default UserMain;
