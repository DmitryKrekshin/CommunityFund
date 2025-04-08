import React, {useEffect, useState} from "react";
import {getPersons, Person} from "../httpСlients/PersonHttpClient";
import {useNavigate} from "react-router-dom";
import FullPageLoader from "../components/FullPageLoader";

const PersonListPage: React.FC = () => {
  const navigate = useNavigate();
  const [persons, setPersons] = useState<Person[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getPersons().then(setPersons).finally(() => setLoading(false));
  }, []);

  const handleEdit = (person: Person) => {
    navigate(`/person/edit/${person.guid}`);
  };

  if (loading) return <FullPageLoader />;

  return (
    <div className="container mt-4">
      <h1 className="mb-4">Пайщики</h1>
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
              <td>{person.isExcluded ? <span className="badge bg-danger">Исключен</span> : <span className="badge bg-success">Активен</span>}</td>
              <td>
                <button onClick={() => handleEdit(person)} className="btn btn-outline-info btn-sm">
                  <i className="fas fa-edit"></i> Редактировать
                </button>
                {
                  person.isExcluded ?
                    <button className="btn btn-outline-warning btn-sm ms-2">
                      <i className="fas fa-person-check"></i> Разблокировать
                    </button>
                    :
                    <button className="btn btn-outline-danger btn-sm ms-2">
                      <i className="fas fa-person-lock"></i> Заблокировать
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

export default PersonListPage;
