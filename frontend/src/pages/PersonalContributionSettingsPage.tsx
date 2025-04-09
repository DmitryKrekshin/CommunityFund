import React, {useEffect, useState} from "react";
import {
  ContributionSettings,
  createContributionSettings, getPersonalContributionSettings,
} from "../httpСlients/ContributionSettingsHttpClient";
import PersonSearchSelect from "../components/PersonSearchSelect";
import {getPersons} from "../httpСlients/PersonHttpClient";
import FullPageLoader from "../components/FullPageLoader";

export default function PersonalContributionSettingsPage() {
  const [settingsList, setSettingsList] = useState<ContributionSettings[]>([]);
  const [personsMap, setPersonsMap] = useState<Record<string, string>>({});
  const [form, setForm] = useState<Omit<ContributionSettings, "Guid" | "Id">>({
    dateTo: undefined, guid: "", id: 0,
    amount: 0,
    dateFrom: new Date(),
    personGuid: undefined
  });
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchSettings();
    getPersons().then(res => {
      const map: Record<string, string> = {};
      res.forEach(p => map[p.guid] = `${p.surname} ${p.name} ${p.patronymic}`);
      setPersonsMap(map);
    });
  }, []);

  const fetchSettings = async () => {
    try {
      setLoading(true);
      const allSettings = await getPersonalContributionSettings();
      const personal = allSettings.filter(s => s.personGuid);
      setSettingsList(personal);
    } catch {
      console.error("Error when fetch settings")
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!form.personGuid) return alert("Выберите человека");
    await createContributionSettings({...form, guid: crypto.randomUUID(), id: 0});
    await fetchSettings();
  };

  return (
    <div className="container mt-4">
      <h3>Индивидуальные паевые взносы</h3>
      {loading && <FullPageLoader/>}
      <form onSubmit={handleSubmit} className="mb-4">
        <div className="mb-3">
          <label className="form-label">ФИО пайщика</label>
          <PersonSearchSelect value={form.personGuid} onChange={(guid) => setForm({...form, personGuid: guid})}/>
        </div>
        <div className="mb-3">
          <label className="form-label">Сумма (руб.)</label>
          <input
            type="number"
            className="form-control"
            value={form.amount}
            onChange={(e) => setForm({...form, amount: parseFloat(e.target.value)})}
            required
          />
        </div>
        <div className="row mb-3">
          <div className="col">
            <label className="form-label">Дата начала</label>
            <input
              type="date"
              className="form-control"
              value={form.dateFrom.toISOString().split("T")[0]}
              onChange={(e) => setForm({...form, dateFrom: new Date(e.target.value)})}
              required
            />
          </div>
          <div className="col">
            <label className="form-label">Дата окончания</label>
            <input
              type="date"
              className="form-control"
              // value={form.DateTo?.toISOString().split("T")[0]}
              onChange={(e) => setForm({...form, dateTo: new Date(e.target.value)})}
            />
          </div>
        </div>
        <button className="btn btn-primary" type="submit">Сохранить</button>
      </form>

      <table className="table table-bordered">
        <thead>
        <tr>
          <th>ФИО</th>
          <th>Сумма</th>
          <th>Дата начала</th>
          <th>Дата окончания</th>
        </tr>
        </thead>
        <tbody>
        {settingsList.map((s, i) => (
          <tr key={i}>
            <td>{s.personGuid ? personsMap[s.personGuid] : "-"}</td>
            <td>{s.amount}</td>
            <td>{new Date(s.dateFrom).toLocaleDateString()}</td>
            <td>{s.dateTo ? new Date(s.dateTo).toLocaleDateString() : "Бессрочно"}</td>
          </tr>
        ))}
        </tbody>
      </table>
    </div>
  );
}
