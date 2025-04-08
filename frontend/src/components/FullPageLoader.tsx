import React from "react";
import "./FullPageLoader.css"; // не забудь создать этот файл

const FullPageLoader: React.FC = () => {
  return (
    <div className="loader-overlay">
      <div className="spinner" />
    </div>
  );
};

export default FullPageLoader;
