import { useGoogleLogin } from "@react-oauth/google";
import { useCookies } from "react-cookie";
import "./style.scss";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";

export const Login = () => {
  const [token, setToken] = useCookies(["accessToken"]);
  const navigate = useNavigate();

  useEffect(() => {
    if (token.accessToken) navigate("/");
    return;
  }, [token.accessToken]);

  const handleSuccess = (response: any) => {
    const authCode = response.code;
    fetch("https://vesinhdanang.xyz:7024/api/auth/google", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Credentials": "true"
      },
      credentials: "include",
      body: JSON.stringify({ AuthCode: authCode }),
    })
      .then((res) => {
        if (res.ok) {
          return res.text(); // This returns a Promise
        } else {
          throw new Error(res.statusText);
        }
      })
      .then((accessToken) => {
        // This block will be executed after the Promise resolves
        console.log("Authentication successful, access token: " + accessToken);
        setToken("accessToken", accessToken); // Save the access token
        navigate("/");
      })
      .catch((error) => {
        console.log(error);
        // Handle errors here
      });
  };

  const gglogin = useGoogleLogin({
    onSuccess: handleSuccess,
    flow: "auth-code",
  });

  // const getEvents = () => {
  //   fetch(
  //     "https://localhost:7024/api/ScheduleTreeTrim/GetCalendarEvents/" +
  //       token.accessToken,
  //     {
  //       method: "GET",
  //       headers: {
  //         "Content-Type": "application/json",
  //       },
  //     }
  //   )
  //     .then((res) => {
  //       if (res.ok) {
  //         return res.json(); // This returns a Promise
  //       } else {
  //         console.error("Failed to get events:", res.statusText);
  //         // Handle authentication failure
  //       }
  //     })
  //     .then((events) => {
  //       // This block will be executed after the Promise resolves
  //       console.log("Events:", events);
  //       // Do something with the events
  //     })
  //     .catch((error) => {
  //       console.log(error);
  //       // Handle errors here
  //     });
  // };

  return (
    <div className="login-page">
      <div className="container">
        <div className="left">
          <img src="/assets/imgs/logo.jpg" alt="Image" />
        </div>
        <div className="right">
          <h6>Xin chào!</h6>
          <button onClick={() => gglogin()}>Đăng nhập với Google</button>
        </div>
      </div>
    </div>
  );
};
