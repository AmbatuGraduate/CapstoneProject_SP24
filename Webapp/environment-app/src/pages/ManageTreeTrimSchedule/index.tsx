import { useEffect } from "react";
import { useCookies } from "react-cookie";
import { useNavigate } from "react-router-dom";

export const ManageTreeTrimSchedule = () => {
  const [token, setToken] = useCookies(["accessToken"]);
  const navigate = useNavigate();
  useEffect(() => {
    if (token.accessToken) navigate("/");
    return;
  }, [token.accessToken]);

  const getEvents = () => {
    fetch(
      "https://localhost:7024/api/ScheduleTreeTrim/GetCalendarEvents/" +
        token.accessToken,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      }
    )
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
};
