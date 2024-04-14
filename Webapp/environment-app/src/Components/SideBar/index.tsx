import React, { useState } from "react";
import "./style.scss";
import { useCookies } from "react-cookie";

// Imported Icons
import { BsQuestionCircle } from "react-icons/bs";
import { FaTreeCity } from "react-icons/fa6";
import { GiTreeBranch } from "react-icons/gi";
import { GrUserManager } from "react-icons/gr";
import { LuTrees } from "react-icons/lu";
import { PiTrashSimpleBold } from "react-icons/pi";
import { TbLayoutDashboard } from "react-icons/tb";
import { IoHomeOutline } from "react-icons/io5";
import { GrGroup } from "react-icons/gr";

// import router
import { NavLink } from "react-router-dom";



const Sidebar = () => {
  const [showCard, setShowCard] = useState(false);
  const [token] = useCookies(["accessToken"]);

  const toggleCard = () => {
    setShowCard(!showCard);
  };

  return (
    <div className="sideBar ">
      <div className="logoDiv flex ">
        <NavLink className="d-none d-md-inline" to="/" style={{ width: '100%' }}>
          <img src="assets/imgs/logoEcoo.png" alt="Image_name" />
        </NavLink>
      </div>
      <hr className="line d-none d-md-block" />
      <div className="menuDiv p-0">
        <ul className="menuLists grid">

          <li className="listItem">
            <NavLink to="/" className="menuLink flex">
              <IoHomeOutline className="icon" />
              <span className="smallText d-none d-md-inline ">Trang Chủ</span>
            </NavLink>
          </li>

          <li className="listItem">
            <NavLink to="/manage-report" className="menuLink flex">
              <TbLayoutDashboard className="icon" />
              <span className="smallText d-none d-md-inline ">
                Quản Lý Báo Cáo
              </span>
            </NavLink>
          </li>

          {(JSON.parse(token.accessToken).role == "Admin" || JSON.parse(token.accessToken).role == "Manager") && (
            <li className="listItem">
              <NavLink to="/manage-group" className="menuLink flex">
                <GrGroup className="icon" />
                <span className="smallText d-none d-md-inline">
                  Quản Lý Bộ Phận
                </span>
              </NavLink>
            </li>
          )}

          <li className="listItem">
            <NavLink to="/manage-employee" className="menuLink flex">
              <GrUserManager className="icon" />
              <span className="smallText d-none d-md-inline">
                Quản Lý Nhân Sự
              </span>
            </NavLink>
          </li>

          {(JSON.parse(token.accessToken).role == "Admin" || (JSON.parse(token.accessToken).role == "Manager" && JSON.parse(token.accessToken).department == "Quan ly cay xanh")) && (
            <li className="listItem">
              <NavLink to="/manage-tree" className="menuLink flex ">
                <LuTrees className="icon" />
                <span className="smallText d-none d-md-inline">
                  Quản Lý Cây Xanh
                </span>
              </NavLink>
            </li>
          )}

          {(JSON.parse(token.accessToken).role == "Admin" || (JSON.parse(token.accessToken).role == "Manager" && JSON.parse(token.accessToken).department == "Quan ly cay xanh")) && (
            <li className="listItem">
              <NavLink to="/manage-treetrim-schedule" className="menuLink flex">
                <GiTreeBranch className="icon" />
                <span className="smallText d-none d-md-inline">
                  Quản Lý Lịch Cắt Tỉa
                </span>
              </NavLink>
            </li>
          )}

          {(JSON.parse(token.accessToken).role == "Admin" || (JSON.parse(token.accessToken).role == "Manager" && JSON.parse(token.accessToken).department == "Quan ly quet don")) && (
            <li className="listItem">
              <NavLink to="/manage-cleaning-schedule" className="menuLink flex">
                <FaTreeCity className="icon" />
                <span className="smallText d-none d-md-inline">
                  Quản Lý Lịch Vệ Sinh Đô Thị
                </span>
              </NavLink>
            </li>
          )}

          {(JSON.parse(token.accessToken).role == "Admin" || (JSON.parse(token.accessToken).role == "Manager" && JSON.parse(token.accessToken).department == "Quan ly thu gom rac")) && (
            <li className="listItem">
              <NavLink
                to="/manage-garbagecollection-schedule"
                className="menuLink flex"
              >
                <PiTrashSimpleBold className="icon" />
                <span className="smallText d-none d-md-inline">
                  Quản Lý Lịch Thu Gom Rác
                </span>
              </NavLink>
            </li>
          )}
        </ul>
      </div>

      <div className="sideBarCard">
        <BsQuestionCircle className="icon" onClick={toggleCard} />
        {showCard && (
          <div className="cardContent">
            <div className="circle1"></div>
            <div className="circle2"></div>
            <h3>Trung tâm trợ giúp</h3>
            <p>
              Nếu bạn gặp vấn đề khi sử dụng Ambatu, xin hãy liên hệ với chúng
              tôi để nhận hỗ trợ.
            </p>
            <button className="btn">Liên hệ trung tâm</button>
          </div>
        )}
      </div>
    </div>
  );
};

export default Sidebar;
