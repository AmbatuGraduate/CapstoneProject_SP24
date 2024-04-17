import { useCookies } from "react-cookie";
import { Outlet, useNavigate } from "react-router-dom";
import Sidebar from "../SideBar";
import Top from "./Top";
import "./style.scss";
import { useEffect } from "react";

export const Layout = () => {
  const navigate = useNavigate();
  const [token] = useCookies(["accessToken"]);

  useEffect(() => {
    if (!token.accessToken) navigate("/login");
    return;
  }, [token.accessToken]);
  if (!token.accessToken) return;

  return (
    <div className="container-fluid m-0 p-0" style={{
      backgroundImage: `url(https://img.freepik.com/free-vector/gradient-white-monochrome-background_23-2149017048.jpg?w=996&t=st=1713319109~exp=1713319709~hmac=de79af5e9ba392d1e1fa9a6bcbb8642dd12212207bb052054a53b8470827c98b)`,
      backgroundPosition: 'center',
      backgroundSize: 'cover',
      backgroundRepeat: 'no-repeat',
      height: '100vh',
      width: '100vw'
    }}>
      <Sidebar />
      <div className="mainContent">
        <Top />
        <div className="bottom">
          <Outlet />
        </div>
      </div>
    </div>
  );
};
