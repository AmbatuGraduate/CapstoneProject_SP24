import { useState } from "react";
import "./top.scss";

// imported Icon
import { IoNotificationsOutline } from "react-icons/io5";
import { useCookies } from "react-cookie";
import { ImProfile } from "react-icons/im";
import { MdLogout } from "react-icons/md";

const Top = () => {
  const [token, setToken] = useCookies(["accessToken"]);
  const [isOpen, setIsOpen] = useState(false);
  const [, , removeCookie] = useCookies(["accessToken"]);

  const toggleDropdown = () => {
    setIsOpen(!isOpen);
  };

  const LogOut = async () => {
    try {
      await fetch("https://localhost:7024/api/auth/googlelogout", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token.accessToken}`,
        },
      });

      removeCookie("accessToken"); // Xóa cookie khi đăng xuất thành công
    } catch (error) {
      console.log(error);
      // Xử lý lỗi
    }
  };

  return (
    <div className="topSection">
      <div className="headerSection flex">
        <div className="title">
          <h1>Chào mừng tới với Ambatu!</h1>
          <p>Xin chào {JSON.parse(token.accessToken).name}</p>
        </div>
        <div className="adminDiv flex">
          <IoNotificationsOutline className="icon" />
          <div className="adminImage">
            <button onClick={toggleDropdown} className="dropbtn">
              <img
                src={JSON.parse(token.accessToken).image}
                alt="Admin Image"
              />
            </button>
            {isOpen && (
              <div className="dropdown-menu">
                <button className="flex">
                  <ImProfile className="dropIcon" /> <h6>Hồ sơ</h6>
                </button>
                <hr className="menuLine" />
                <button className="flex" onClick={LogOut}>
                  <MdLogout className="dropIcon" /> <h6>Đăng xuất</h6>
                </button>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Top;
