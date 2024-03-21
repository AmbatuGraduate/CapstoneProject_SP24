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
    fetch("https://localhost:7024/api/auth/google", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
      },
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

  return (
    <div className="login-page">
      <div className="container">
        <div className="left">
          <img src="/assets/imgs/logo.jpg" alt="Image" />
        </div>
        <div className="right">
          <h1>Xin chào!</h1>
          <button onClick={() => gglogin()}>Đăng nhập với Google</button>
        </div>
      </div>
    </div>
  );
};
