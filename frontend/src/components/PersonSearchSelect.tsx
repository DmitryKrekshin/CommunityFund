import { useEffect, useState } from "react";
import {getPersons, Person} from "../httpСlients/PersonHttpClient";

interface Props {
  value?: string;
  onChange: (guid: string | undefined) => void;
}

export default function PersonSearchSelect({ value, onChange }: Props) {
  const [persons, setPersons] = useState<Person[]>([]);

  useEffect(() => {
    getPersons().then(res => {
      setPersons(res);
    });
  }, []);

  return (
    <select className="form-select" value={value ?? ""} onChange={(e) => onChange(e.target.value || undefined)}>
      <option value="">Выберите человека</option>
      {persons.map(p => (
        <option key={p.guid} value={p.guid}>{p.surname} {p.name} {p.patronymic}</option>
      ))}
    </select>
  );
}
