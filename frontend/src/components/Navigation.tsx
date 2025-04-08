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
              <Link className="navbar-brand" to="/persons">Пайщики</Link>
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
