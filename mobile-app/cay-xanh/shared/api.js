import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';

const api = axios.create();

// Add a request interceptor
api.interceptors.request.use(
    async config => {
        const loggedUser = await AsyncStorage.getItem("@user");
        const userData = JSON.parse(loggedUser);
        const { token, token_received_at, expire_in } = userData;
        const currentTime = Date.now() / 1000;
        if (token && (currentTime - token_received_at) >= expire_in) {
            // if token is expired, refresh 
            const refreshToken = await AsyncStorage.getItem("@refreshToken");
            const response = await axios.get(`http://192.168.1.7:45455/api/auth/RefreshMobile`, {
                headers: {
                    'Authorization': `Bearer ${refreshToken}`
                }
            });
            const newTokenData = response.data.value;
            const updatedUser = {
                ...userData,
                token: newTokenData.token,
                token_received_at: currentTime,
                expire_in: newTokenData.expire_in
            };
            await AsyncStorage.setItem("@user", JSON.stringify(updatedUser));
            await AsyncStorage.setItem("@accessToken", newTokenData.token);
            config.headers['Authorization'] = 'Bearer ' + newTokenData.token;
        }
        config.headers['Client-Type'] = 'Mobile';
        config.headers['Content-Type'] = 'application/json';
        return config;
    },
    error => Promise.reject(error)
);

// Add a response interceptor
api.interceptors.response.use(
    response => response,
    error => Promise.reject(error)
);

export { api };