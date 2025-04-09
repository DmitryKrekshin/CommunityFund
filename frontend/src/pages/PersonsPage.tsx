import React, { useEffect, useState } from "react";
import { getPersons, Person, expelPerson, readmitPerson } from "../httpСlients/PersonHttpClient";
import { useNavigate } from "react-router-dom";
import FullPageLoader from "../components/FullPageLoader";

const PersonsPage: React.FC = () => {
  const navigate = useNavigate();
  const [persons, setPersons] = useState<Person[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getPersons().then(setPersons).finally(() => setLoading(false));
  }, []);

  const handleEdit = (person: Person) => {
    navigate(`/person/edit/${person.guid}`);
  };

  const handleBlock = async (person: Person) => {
    try {
      setLoading(true);
      await expelPerson(person.guid);
      setPersons((prevPersons) =>
        prevPersons.map((p) =>
          p.guid === person.guid ? { ...p, isExcluded: true } : p
        )
      );
    } catch (error) {
      console.error("Ошибка при блокировке пользователя", error);
    } finally {
      setLoading(false);
    }
  };

  const handleUnblock = async (person: Person) => {
    try {
      setLoading(true);
      await readmitPerson(person.guid);
      setPersons((prevPersons) =>
        prevPersons.map((p) =>
          p.guid === person.guid ? { ...p, isExcluded: false } : p
        )
      );
    } catch (error) {
      console.error("Ошибка при разблокировке пользователя", error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container mt-4">
      <h1 className="mb-4">Пайщики</h1>
      {loading && <FullPageLoader />}
      <button className="btn btn-primary mb-3" onClick={() => navigate("/person/create")}>
        <i className="fas fa-user-plus"></i> Добавить пайщика
      </button>
      <div className="table-responsive">
        <table className="table table-bordered">
          <thead>
            <tr className="bg-gray-100">
              <th>#</th>
              <th>Имя</th>
              <th>Email</th>
              <th>Статус</th>
              <th>Действия</th>
            </tr>
          </thead>
          <tbody>
            {persons.map((person) => (
              <tr key={person.id}>
                <td>{person.id}</td>
                <td>{`${person.surname} ${person.name} ${person.patronymic}`}</td>
                <td>{person.email}</td>
                <td>
                  {person.isExcluded ? (
                    <span className="badge bg-danger">Исключен</span>
                  ) : (
                    <span className="badge bg-success">Активен</span>
                  )}
                </td>
                <td>
                  <button onClick={() => handleEdit(person)} className="btn btn-outline-info btn-sm">
                    <i className="fas fa-edit"></i> Редактировать
                  </button>
                  {
                    person.isExcluded ?
                      <button
                        onClick={() => handleUnblock(person)}
                        className="btn btn-outline-warning btn-sm ms-2"
                      >
                        <i className="fas fa-person-check"></i> Восстановить
                      </button>
                      :
                      <button
                        onClick={() => handleBlock(person)}
                        className="btn btn-outline-danger btn-sm ms-2"
                      >
                        <i className="fas fa-person-lock"></i> Исключить
                      </button>
                  }
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default PersonsPage;
