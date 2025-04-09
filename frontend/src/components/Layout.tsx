import {Outlet} from "react-router-dom";
import Navigation from "./Navigation";
import Footer from "./Footer";

const Layout = () => {
  return (
    <div style={{
      display: "flex",
      flexDirection: "column",
      minHeight: "100vh"
    }}>
      <Navigation/>
      <main style={{ flexGrow: 1, padding: "1rem" }}>
        <Outlet/>
      </main>
      <Footer/>
    </div>
  );
};

export default Layout;
