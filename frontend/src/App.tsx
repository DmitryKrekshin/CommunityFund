import React from 'react';
import {BrowserRouter as Router, Routes, Route, Navigate} from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import HomePage from "./pages/HomePage";
import Layout from "./components/Layout";
import PersonsPage from "./pages/PersonsPage";
import UpsertPersonPage from "./pages/UpsertPersonPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<LoginPage/>}/>
        <Route element={<Layout/>}>
          <Route path="/home" element={<ProtectedRoute><HomePage/></ProtectedRoute>}/>
        </Route>
        <Route element={<Layout/>}>
          <Route path="/persons" element={<ProtectedRoute><PersonsPage/></ProtectedRoute>}/>
        </Route>
        <Route element={<Layout/>}>
          <Route path="/person/edit/:personGuid" element={<ProtectedRoute><UpsertPersonPage/></ProtectedRoute>}/>
        </Route>
        <Route element={<Layout/>}>
          <Route path="/person/create" element={<ProtectedRoute><UpsertPersonPage/></ProtectedRoute>}/>
        </Route>
        <Route path="*" element={<Navigate to="/home"/>}/>
      </Routes>
    </Router>
  );
}

const ProtectedRoute: React.FC<{ children: React.ReactNode }> = ({children}) => {
  const token = localStorage.getItem("token");
  return token ? <>{children}</> : <Navigate to="/login"/>;
};

export default App;
