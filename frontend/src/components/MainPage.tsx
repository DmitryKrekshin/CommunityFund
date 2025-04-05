import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import UserList from "./UserList";
import AuthForm from "./AuthForm";
import {useState} from 'react';
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faHome} from '@fortawesome/free-solid-svg-icons';
import UserMainPage from "./UserMain";
import {useNavigate} from "react-router-dom";

// todo удалить если не нужен

function MainPage() {
  const [children, setChildren] = useState(<UserList/>);

  return (
    <div>

      {children}

      {/*Футер*/}
      <footer className="bg-primary text-white py-3 navbar">
        <div className="container-fluid"><p className="m-0">&copy; 2025 ПотребОбщество. Все права защищены.</p></div>
      </footer>
    </div>
  );
}

export default MainPage;
