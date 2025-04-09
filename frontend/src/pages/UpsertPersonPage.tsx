import React, {useEffect, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";
import {createPerson, getPersonInfo, updatePerson, Person} from "../httpСlients/PersonHttpClient";
import FullPageLoader from "../components/FullPageLoader";

const UpsertPersonPage: React.FC = () => {
  const {personGuid} = useParams();
  const navigate = useNavigate();

  const [person, setPerson] = useState<Person>({
    id: 0,
    guid: "",
    surname: "",
    name: "",
    patronymic: "",
    email: "",
    phoneNumber: "",
    isExcluded: false,
  });

  const [loading, setLoading] = useState(true);

  const isEdit = !!personGuid;

  useEffect(() => {
    const fetchData = async () => {
      if (personGuid) {
        try {
          const data = await getPersonInfo(personGuid);
          setPerson(data);
        } catch (error) {
          console.error("Ошибка загрузки персоны", error);
        } finally {
          setLoading(false);
        }
      } else {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const {name, value} = e.target;
    setPerson((prev) => ({...prev, [name]: value}));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      setLoading(true);
      if (isEdit) {
        await updatePerson(person);
      } else {
        await createPerson(person);
      }
      navigate("/persons");
    } catch {
      console.log('Error when update person')
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container mt-4">
      <h2 className="mb-4">{isEdit ? "Редактирование пайщика" : "Создание пайщика"}</h2>
      {loading && <FullPageLoader/>}
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Фамилия</label>
          <input name="surname" value={person.surname} onChange={handleChange} className="form-control" required/>
        </div>
        <div className="mb-3">
          <label className="form-label">Имя</label>
          <input name="name" value={person.name} onChange={handleChange} className="form-control" required/>
        </div>
        <div className="mb-3">
          <label className="form-label">Отчество</label>
          <input name="patronymic" value={person.patronymic} onChange={handleChange} className="form-control"/>
        </div>
        <div className="mb-3">
          <label className="form-label">Email</label>
          <input type="email" name="email" value={person.email} onChange={handleChange} className="form-control" required/>
        </div>
        <div className="mb-3">
          <label className="form-label">Телефон</label>
          <input name="phoneNumber" value={person.phoneNumber} onChange={handleChange} className="form-control"/>
        </div>

        <button type="submit" className="btn btn-success">
          {isEdit ? "Сохранить изменения" : "Создать"}
        </button>
        <button type="button" onClick={() => navigate("/persons")} className="btn btn-secondary ms-2">
          Отмена
        </button>
      </form>
    </div>
  );
};

export default UpsertPersonPage;
