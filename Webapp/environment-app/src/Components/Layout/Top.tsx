import /*React,*/ { useState } from "react";
import "./top.scss";

// imported Icon
import { IoNotificationsOutline } from "react-icons/io5";
import { useCookies } from "react-cookie";

const Top = () => {
  const [isOpen, setIsOpen] = useState(false);
  const toggleDropdown = () => {
    setIsOpen(!isOpen);
  };
  const [token/*, setToken*/] = useCookies(["accessToken"]);
  const LogOut = async () => {
    await fetch("https://localhost:7024/api/auth/googlelogout", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token.accessToken}`,
      },
    })
      .then((res) => {
        if (res.ok) {
          return res.json(); // This returns a Promise
        } else {
          console.error("Failed to get events:", res.statusText);
          // Handle authentication failure
        }
      })
      .then((events) => {
        // This block will be executed after the Promise resolves
        console.log("Events:", events);
        // Do something with the events
      })
      .catch((error) => {
        console.log(error);
        // Handle errors here
      });
  };

  return (
    <div className="topSection">
      <div className="headerSection flex">
        <div className="title">
          <h1>Chào mừng tới với Ambatu</h1>
          <p>Xin chào Thăng</p>
        </div>

        {/* <div className="searchBar flex">
                    <input type="text" placeholder='Tìm kiếm' />
                    <BiSearchAlt className='icon' />
                </div> */}
        {/* <div className='searchBar flex'>
                    <SearchBar />
                </div> */}

        <div className="adminDiv flex">
          <IoNotificationsOutline className="icon" />
          <div className="adminImage">
            <button onClick={toggleDropdown}>
              <img src="/assets/imgs/adminImg.jpg" alt="Admin Image" />
            </button>
            {isOpen && (
              <div className="dropdown-menu">
                <button>Thông tin cá nhân</button>
                <button>Cài đặt</button>
                <button onClick={LogOut}>Đăng xuất</button>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Top;
