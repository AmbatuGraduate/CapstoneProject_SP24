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
    <div className="container-fluid m-0 p-0">
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
