import React, {useState} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

export interface User {
  id: number;
  fullName: string;
  email: string;
  role: string[];
  status: number;
}

let users: { id: number, fullName: string, email: string, role: string, status: number }[] = [
  {id: 1, fullName: "Иванов Иван Иванович", email: "IvanovIvan@example.com", role: "Пайщик", status: 1},
  {id: 2, fullName: "Семенов Семен Семенович", email: "SemenovSemen@example.com", role: "Пайщик", status: 0},
  {id: 3, fullName: "Петров Петр Петрович", email: "PetrovPetr@example.com", role: "Бухгалтер", status: 1},
  {id: 4, fullName: "Иванов Иван Иванович", email: "IvanovIvan@example.com", role: "Администратор", status: 1}
];

function UserList() {
  const[users, setUsers] = useState<Array<User>>();

  // const userListItems = users.map((user) =>
  //   <tr>
  //     <td>{user.id}</td>
  //     <td>{user.fullName}</td>
  //     <td>{user.email}</td>
  //     <td>{user.role}</td>
  //     <td>{user.status === 1 ? <span className="badge bg-success">Активен</span> : <span className="badge bg-danger">Заблокирован</span>}</td>
  //     <td>
  //       <a href="#" className="btn btn-outline-info btn-sm">
  //         <i className="fas fa-edit"></i> Редактировать
  //       </a>
  //       <a href="#" className="btn btn-outline-danger btn-sm ms-2">
  //         <i className="fas fa-trash-alt"></i> Удалить
  //       </a>
  //     </td>
  //   </tr>)

  return (
    <div className="container mt-4">
      <h1 className="mb-4">Пайщики</h1>

      <div className="table-responsive">
        <table className="table table-bordered">
          <thead>
          <tr>
            <th>#</th>
            <th>Имя</th>
            <th>Email</th>
            <th>Роль</th>
            <th>Статус</th>
            <th>Действия</th>
          </tr>
          </thead>
          <tbody>
          {/*{userListItems}*/}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default UserList;
