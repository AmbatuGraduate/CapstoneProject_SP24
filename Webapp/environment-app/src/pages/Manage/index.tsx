import React, { useEffect, useState } from 'react';
import * as signalR from "@microsoft/signalr";
import axios from 'axios';
import './index.scss';
import { useCookies } from 'react-cookie';

export const Manage = () => {
  type Notification = {
    id: number;
    username: string;
    message: string;
    notificationDateTime: string;
  };
  interface Weather {
    current: {
      temp_c: number;
      condition: {
        icon: string;
        text: string;
      };
    };
  }
  const [hubConnection, setHubConnection] = useState<signalR.HubConnection | null>(null);
  const [notifications, setNotifications] = useState<Notification[]>([]);
  const [username, setUsername] = useState('');
  const [page, setPage] = useState(1);
  const [token] = useCookies(["accessToken"]);
  const [weather, setWeather] = useState<Weather | null>(null);

  useEffect(() => {
    const fetchWeather = async () => {
      try {
        const response = await axios.get(`https://api.weatherapi.com/v1/current.json?key=7efc8515bf4145e2b60170817240804&q=Danang&lang=vi&aqi=no`);
        console.log(response.data);
        setWeather(response.data);
      } catch (error) {
        console.error("Error fetching weather data: ", error);
      }
    };

    fetchWeather();
  }, []);


  const loadMore = () => {
    setPage(prevPage => prevPage + 1);
  };

  useEffect(() => {
    const createHubConnection = async () => {
      const hubConnect = new signalR.HubConnectionBuilder()
        .withUrl("https://vesinhdanang.xyz:7024/chatHub")
        // .withUrl("https://localhost:7024/chatHub")
        .build();
      try {
        await hubConnect.start();
        setUsername(JSON.parse(token.accessToken).email);
        console.log("Connection successful!");
        hubConnect.invoke("SaveUserConnection", username)
          .catch(function (error) {
            return console.error(error);
          });
      } catch (err) {
        console.error(err);
      }
      setHubConnection(hubConnect);

    }

    createHubConnection();

    axios.get(`https://vesinhdanang.xyz:7024/api/Notification/GetByUsername/${username}?page=${page}`, {
      // axios.get(`https://localhost:7024/api/Notification/GetByUsername/${username}?page=${page}`, {
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((res) => {
        setNotifications(prevNotifications => {
          const existingIds = new Set(prevNotifications.map((a, index) => ({ id: index, message: a })));
          const newNotifications = res.data.filter(a => !existingIds.has(a.id));
          return [...prevNotifications, ...newNotifications];
        });
      })

      .catch((error) => {
        console.log('There has been a problem with fetch operation: ', error.message);
      });

    return () => {
      hubConnection?.stop();
    };
  }, [username, page]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on("ReceivedNotification", (message) => {
        console.log(message);
        setNotifications(prevNotifications => [...prevNotifications, message]);
      });

      hubConnection.on("ReceivedPersonalNotification", (message, user) => {
        console.log(user + " - " + message);
        setNotifications(prevNotifications => {
          const newNotification = {
            id: prevNotifications.length,
            username: user,
            message: message,
            notificationDateTime: new Date().toLocaleString('en-US'),
          };
          const newNotifications = [newNotification, ...prevNotifications];
          console.log("New notifications: " + newNotifications);
          return newNotifications;
        });
      });
    }
  }, [hubConnection]);


  const todaysNotifications = notifications.filter(notification => {
    const notificationDate = new Date(notification.notificationDateTime);
    const currentDate = new Date();
    return notificationDate.toDateString() === currentDate.toDateString();
  });

  return (

    <div className="notification-wrapper">
      <h2 style={{ fontWeight: 'bold', color: 'grey' }}>
        Thời tiết Đà Nẵng
      </h2>
      {weather && (
        <div className="weather">

          <div className="weather-detail">
            <p className="currentDay">
              {new Date().toLocaleDateString('vi-VN', { weekday: 'long' })}, {new Date().toLocaleDateString('vi-VN')}
            </p>
            <p className="temp">{weather.current.temp_c} <span className="degree">°C</span></p>
          </div>
          <div className="vertical-line"></div>
          <div className="weather-condition">
            <img src={weather.current.condition.icon} alt={weather.current.condition.text} />
            <p>{weather.current.condition.text}</p>
          </div>

        </div>
      )}

      <div className="notification-container">
        <div className="notification-header">
          <p className="notification-title">Thông báo hôm nay</p>
          <div className="notification-count">
            {todaysNotifications.length}
          </div>
        </div>
        {notifications.map((notification, index) => {
          const notificationDate = new Date(notification.notificationDateTime);
          const currentDate = new Date();
          const isToday = notificationDate.toDateString() === currentDate.toDateString();

          return (
            <div key={index} className={`notification ${isToday ? 'today' : ''}`}>
              <div className="message">
                {/* <h6>{notification.username}</h6> */}
                <p>{notification.message}</p>
              </div>
              <div className="notificationDateTime">
                <p style={{ color: 'grey', margin: 'auto' }}>{notification.notificationDateTime}</p>
              </div>
            </div>
          );
        })}
        <button className="loadMore" onClick={loadMore}>Xem thêm</button>
      </div>
    </div>
  );
};