import axios from "axios";


const axiosClient = axios.create({
  baseURL: import.meta.env.VITE_BASE_URL + "/api/",
  withCredentials: true,
  headers: {
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Credentials": "true",
  },
});

const getTokenFromCookie = (): string | undefined => {
  const cookies = document.cookie.split(';');
  for (const cookie of cookies) {
    const [name, value] = cookie.trim().split('=');
    if (name === 'accessToken') {
      return decodeURIComponent(value);
    }
  }
  return undefined;
};

axiosClient.interceptors.request.use(
  async function (config) {
    const cookie: string = getTokenFromCookie()!;
    var expire = new Date(JSON.parse(JSON.parse(cookie)).expire_in).getTime();

    console.log('compare - expire > now', expire > Date.now());

    // if (expire < Date.now()) {
      const response = await axios.get('https://vesinhdanang.xyz:7024/api/auth/refresh',
        // const response = await axios.get('https://localhost:7024/api/auth/refresh',
        {
          withCredentials: true,
          headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Credentials": "true",
          },
        }
      )
      // .then((res) => {
      //   console.log(res.data);
      //   document.cookie= "accessToken="+encodeURIComponent(JSON.stringify(res.data));
      // })
    // }

    return config;
  },
  function (error) {
    return Promise.reject(error);
  }
);

axiosClient.interceptors.response.use(
  function (response) {
    return response;
  },
  function (error) {
    return Promise.reject(error);
  }
);

export default axiosClient;
