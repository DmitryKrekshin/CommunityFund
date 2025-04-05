import {Outlet} from "react-router-dom";
import Navigation from "./Navigation";
import Footer from "./Footer";

const Layout = () => {
  return (
    <div className="flex flex-col min-h-screen">
      <Navigation/>
      <main className="flex-grow p-4">
        <Outlet/>
      </main>
      <Footer/>
    </div>
  );
};

export default Layout;
