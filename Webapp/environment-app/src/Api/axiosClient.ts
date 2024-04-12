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

// Add a request interceptor
axiosClient.interceptors.request.use(
  async function (config) {
    // Do something before request is sent
    const cookie: string = getTokenFromCookie()!;
    var expire = new Date(JSON.parse(JSON.parse(cookie)).expire_in).getTime(); // Convert expire date to miliseconds

    // Check if expire_in is less than now -> refresh token
    console.log('compare - expire > now', expire > Date.now());

    if(expire < Date.now())
    {
      const response = await axios.post('https://vesinhdanang.xyz:7024/api/auth/refresh',
        {
          withCredentials: true,
          headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Credentials": "true",
          },
        }
      )
    }

    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

// Add a response interceptor
axiosClient.interceptors.response.use(
  function (response) {
    // Any status code that lie within the range of 2xx cause this function to trigger
    // Do something with response data
    return response;
  },
  function (error) {
    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error
    return Promise.reject(error);
  }
);

export default axiosClient;
