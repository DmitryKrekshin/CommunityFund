import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faHome} from '@fortawesome/free-solid-svg-icons';
import {Link, useNavigate} from "react-router-dom";

function Navigation() {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("personGuid");
    localStorage.removeItem("personInfo");
    navigate("/login");
  };

  let personName;
  const personInfo = localStorage.getItem("personInfo");
  if (personInfo) {
    const person = JSON.parse(personInfo);
    personName = `${person.surname} ${person.name} ${person.patronymic}`;
  }

  return (
    <nav className="navbar navbar-expand-lg bg-primary" data-bs-theme="dark">
      <div className="container-fluid">
        <Link className="navbar-brand" to="/home"><FontAwesomeIcon icon={faHome}/></Link>
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
                aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <Link className="nav-link text-white" to="/persons">Пайщики</Link>
            </li>
            <li className="nav-item dropdown">
              <a className="nav-link text-white dropdown-toggle" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                Финансы
              </a>
              <ul className="dropdown-menu">
                <li><Link className="dropdown-item" to="/contribution/commonSettings">Настройка общих паевых взносов</Link></li>
                <li><Link className="dropdown-item" to="/contribution/personalSettings">Настройка персональных паевых взносов</Link></li>
                <li><Link className="dropdown-item" to="/contributions">Взносы</Link></li>
              </ul>
            </li>
          </ul>
        </div>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav ms-auto mb-2 mb-lg-0">
            <span className="navbar-text me-3 text-white">{personName}</span>
            <li className="nav-item">
              <button className="btn btn-outline-light" onClick={handleLogout}>Выйти</button>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
}

export default Navigation;
