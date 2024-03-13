import "./style.scss";

// Imported Icons
import { BsQuestionCircle } from "react-icons/bs";
import { FaTreeCity } from "react-icons/fa6";
import { GiTreeBranch } from "react-icons/gi";
import { GrUserManager } from "react-icons/gr";
import { LuTrees } from "react-icons/lu";
import { MdOutlineMap } from "react-icons/md";
import { PiTrashSimpleBold } from "react-icons/pi";
import { TbLayoutDashboard } from "react-icons/tb";

// import router
import { NavLink } from "react-router-dom";

const Sidebar = () => {
  return (
    <div className="sideBar ">
      <div className="logoDiv flex">
        <img src="assets/imgs/logo.jpg" alt="Image_name" />
      </div>
      <hr className="line" />
      <div className="menuDiv p-0">
        <ul className="menuLists grid">
          {/* <li className="listItem">
            <NavLink to="/login" className="menuLink flex">
              <TbLayoutDashboard className="icon" />
              <span className="smallText d-none d-md-inline ">Log in</span>
            </NavLink>
          </li> */}

          <li className="listItem">
            <NavLink to="/" className="menuLink flex">
              <TbLayoutDashboard className="icon" />
              <span className="smallText d-none d-md-inline ">
                Quản lý báo cáo
              </span>
            </NavLink>
          </li>

          <li className="listItem">
            <NavLink to="/manage-employee" className="menuLink flex">
              <GrUserManager className="icon" />
              <span className="smallText d-none d-md-inline">
                Quản lý nhân sự
              </span>
            </NavLink>
          </li>

          <li className="listItem">
            <NavLink to="/manage-tree" className="menuLink flex ">
              <LuTrees className="icon" />
              <span className="smallText d-none d-md-inline">
                Quản lý cây xanh
              </span>
            </NavLink>
          </li>

          <li className="listItem">
            <NavLink to="/manage-route" className="menuLink flex">
              <MdOutlineMap className="icon" />
              <span className="smallText d-none d-md-inline">
                Quản lý tuyến đường
              </span>
            </NavLink>
          </li>

          <li className="listItem">
            <NavLink to="/" className="menuLink flex">
              <GiTreeBranch className="icon" />
              <span className="smallText d-none d-md-inline">
                Quản lý lịch cắt tỉa
              </span>
            </NavLink>
          </li>

          <li className="listItem">
            <NavLink to="/" className="menuLink flex">
              <FaTreeCity className="icon" />
              <span className="smallText d-none d-md-inline">
                Quản lý lịch vệ sinh đô thị
              </span>
            </NavLink>
          </li>

          <li className="listItem">
            <NavLink to="/" className="menuLink flex">
              <PiTrashSimpleBold className="icon" />
              <span className="smallText d-none d-md-inline">
                Quản lý lịch thu gom rác
              </span>
            </NavLink>
          </li>
        </ul>
      </div>

      <div className="sideBarCard">
        <BsQuestionCircle className="icon" />
        <div className="cardContent d-none d-xl-block">
          <div className="circle1"></div>
          <div className="circle2"></div>

          <h3>Trung tâm trợ giúp</h3>
          <p>
            Nếu bạn gặp vấn đề khi sử dụng Ambatu, xin hãy liên hệ với chúng tôi
            để nhận hỗ trợ.
          </p>
          <button className="btn">Liên hệ trung tâm</button>
        </div>
      </div>
    </div>
  );
};

export default Sidebar;
