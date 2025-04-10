import React, { useEffect, useState } from "react";
import * as ContributionSettingsHttpClient from "../httpСlients/ContributionSettingsHttpClient";
import * as ContributionHttpClient from "../httpСlients/ContributionHttpClient";
import FullPageLoader from "../components/FullPageLoader";

interface PaymentItem {
  date: Date;
  amount: number;
  isOverdue: boolean;
  isUpcoming: boolean;
  isPaid: boolean;
}

const PaymentPage = () => {
  const [paymentItems, setPaymentItems] = useState<PaymentItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedPayment, setSelectedPayment] = useState<PaymentItem | null>(null);
  const [showModal, setShowModal] = useState(false);
  const personGuid = localStorage.getItem("personGuid") ?? "";

  useEffect(() => {
    const load = async () => {
      const personalSettings = await ContributionSettingsHttpClient.getPersonalContributionSettings();
      const commonSettings = await ContributionSettingsHttpClient.getCommonContributionSettings();
      const settings = personalSettings.find((s) => s.personGuid === personGuid) ?? commonSettings[0];

      if (!settings) {
        alert("Не найдены настройки для расчета взносов");
        return;
      }

      const contributions = await ContributionHttpClient.getByPersonGuid(personGuid);
      const paidDates = contributions.map((c) => new Date(c.date).toDateString());

      const now = new Date();
      const twoWeeksLater = new Date();
      twoWeeksLater.setDate(now.getDate() + 14);

      const result: PaymentItem[] = [];
      const startDate = new Date(settings.dateFrom);
      const endDate = settings.dateTo ? new Date(settings.dateTo) : new Date(now.getFullYear() + 10, 0, 1); // 10 лет вперёд если не указано

      for (let d = new Date(startDate); d <= endDate; d.setMonth(d.getMonth() + 1)) {
        const dueDate = new Date(d);
        const isPaid = paidDates.includes(dueDate.toDateString());
        const isOverdue = dueDate < now && !isPaid;
        const isUpcoming = dueDate >= now && dueDate <= twoWeeksLater && !isPaid;

        if (isOverdue || isUpcoming) {
          result.push({
            date: new Date(dueDate),
            amount: settings.amount,
            isOverdue,
            isUpcoming,
            isPaid,
          });
        }
      }

      setPaymentItems(result);
      setLoading(false);
    };

    load();
  }, [personGuid]);

  const handlePay = async (item: PaymentItem) => {
    await ContributionHttpClient.create({
      id: 0,
      guid: "00000000-0000-0000-0000-000000000000",
      payerGuid: personGuid,
      amount: item.amount,
      date: item.date,
      paymentDate: new Date()
    });

    setPaymentItems((prev) =>
      prev.map((p) =>
        p.date.toDateString() === item.date.toDateString()
          ? { ...p, isPaid: true, isOverdue: false, isUpcoming: false }
          : p
      )
    );
    setShowModal(false);
  };

  const handleShowModal = (item: PaymentItem) => {
    setSelectedPayment(item);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setSelectedPayment(null);
    setShowModal(false);
  };

  return (
    <div className="container mt-4">
      <h2>Оплата паевых взносов</h2>
      {loading && <FullPageLoader />}
      <table className="table mt-3">
        <thead>
          <tr>
            <th>Дата</th>
            <th>Сумма</th>
            <th>Статус</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {paymentItems.map((item, index) => (
            <tr key={index} className={item.isPaid ? "table-success" : item.isOverdue ? "table-danger" : "table-warning"}>
              <td>{new Date(item.date).toLocaleDateString()}</td>
              <td>{item.amount.toFixed(2)}</td>
              <td>
                {item.isPaid
                  ? "Оплачен"
                  : item.isOverdue
                  ? "Просрочен"
                  : item.isUpcoming
                  ? "Ожидается"
                  : ""}
              </td>
              <td>
                {!item.isPaid && (
                  <button className="btn btn-outline-primary btn-sm" onClick={() => handleShowModal(item)}>
                    Оплатить
                  </button>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* Modal for payment confirmation */}
      {showModal && selectedPayment && (
        <>
          {/* Overlay for modal */}
          <div
            className="modal-backdrop fade show"
            style={{ position: "fixed", top: 0, left: 0, right: 0, bottom: 0, backgroundColor: "rgba(0, 0, 0, 1)", zIndex: 1040, transition: "opacity 0.3s ease" }}
            onClick={handleCloseModal}
          ></div>

          <div className="modal fade show" tabIndex={-1} style={{ display: "block" }} aria-labelledby="paymentModalLabel" aria-hidden="false">
            <div className="modal-dialog modal-dialog-centered">
              <div className="modal-content">
                <div className="modal-header">
                  <h5 className="modal-title" id="paymentModalLabel">Подтверждение платежа</h5>
                  <button type="button" className="btn-close" aria-label="Close" onClick={handleCloseModal}></button>
                </div>
                <div className="modal-body">
                  <p>Вы уверены, что хотите оплатить взнос за {new Date(selectedPayment.date).toLocaleDateString()} на сумму {selectedPayment.amount.toFixed(2)}?</p>
                </div>
                <div className="modal-footer">
                  <button type="button" className="btn btn-outline-secondary" onClick={handleCloseModal}>Отменить</button>
                  <button type="button" className="btn btn-outline-primary" onClick={() => handlePay(selectedPayment)}>Оплатить</button>
                </div>
              </div>
            </div>
          </div>
        </>
      )}
    </div>
  );
};

export default PaymentPage;
