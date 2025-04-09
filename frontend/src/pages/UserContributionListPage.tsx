import {useEffect, useState} from "react";
import {Contribution, getByPersonGuid as getContributions} from "../httpСlients/ContributionHttpClient";
import {getPersonInfo} from "../httpСlients/PersonHttpClient";
import {Table, Spinner} from "react-bootstrap";

interface ContributionWithName extends Contribution {
  payerName: string;
}

const UserContributionListPage = () => {
  const [contributions, setContributions] = useState<ContributionWithName[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadContributions = async () => {
      try {
        const personGuid = localStorage.getItem("personGuid");
        if (!personGuid) {
          return;
        }

        const data = await getContributions(personGuid);
        const withNames = await Promise.all(
          data.map(async (c) => {
            const person = await getPersonInfo(c.payerGuid);
            const payerName = `${person.surname} ${person.name} ${person.patronymic ?? ""}`.trim();
            return {...c, payerName};
          })
        );
        setContributions(withNames);
      } catch (error) {
        console.error("Ошибка при загрузке платежей:", error);
      } finally {
        setLoading(false);
      }
    };

    loadContributions();
  }, []);

  return (
    <div className="container mt-4">
      <h2 className="mb-4">Список платежей</h2>
      {loading ? (
        <Spinner animation="border"/>
      ) : (
        <Table striped bordered hover>
          <thead>
          <tr>
            <th>ФИО</th>
            <th>Сумма</th>
            <th>Дата платежа</th>
          </tr>
          </thead>
          <tbody>
          {contributions.map((c) => (
            <tr key={c.guid}>
              <td>{c.payerName}</td>
              <td>{c.amount.toFixed(2)}</td>
              <td>{new Date(c.date).toLocaleString()}</td>
            </tr>
          ))}
          </tbody>
        </Table>
      )}
    </div>
  );
};

export default UserContributionListPage;
