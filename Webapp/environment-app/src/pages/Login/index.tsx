import { useGoogleLogin } from "@react-oauth/google";
import { useCookies } from "react-cookie";
import "./style.scss";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";

import { FcGoogle } from "react-icons/fc";

export const Login = () => {
  const [token, setToken] = useCookies(["accessToken"]);
  const navigate = useNavigate();

  useEffect(() => {
    if (token.accessToken) navigate("/");
    return;
  }, [token.accessToken]);

  const handleSuccess = (response: any) => {
    const authCode = response.code;
    fetch("https://localhost:7024/api/auth/google", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Credentials": "true",
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
        setToken("accessToken", JSON.stringify(accessToken), { maxAge: 15552000 }); // Save the access token
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
    scope:
      "https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/admin.directory.group https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/admin.directory.user https://www.googleapis.com/auth/userinfo.profile openid profile email https://mail.google.com/ https://www.googleapis.com/auth/gmail.send https://www.googleapis.com/auth/gmail.readonly https://www.googleapis.com/auth/gmail.labels https://www.googleapis.com/auth/gmail.compose",
  });

  return (
    <div className="login-page">
      <div className="container">
        <div className="left">
          <img src="/assets/imgs/logo.jpg" alt="Image" />
        </div>
        <div className="right">
          <h1>Xin chào!</h1>
          <button className="flex" onClick={() => gglogin()}>
            <FcGoogle className="icon" /> <h6>Đăng nhập với Google</h6>
          </button>
        </div>
      </div>
    </div>
  );
};
