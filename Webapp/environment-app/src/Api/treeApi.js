import axiosClient from "./axiosClient";

const treeApi = {
    getAll() {
        const url = 'http://localhost:5500';
        return axiosClient.get(url);
    },

    get(id) {
        const url = `https://localhost:7024/api/Tree/${id}`;
        return axiosClient.get(url);
    },

    add(data) {
        const url = 'https://localhost:7024/api/Tree';
        return axiosClient.post(url, data);
    },

    update(data) {
        const url = `https://localhost:7024/api/Tree/${data.id}`;
        return axiosClient.patch(url, data);
    },

    remove(id) {
        const url = `https://localhost:7024/api/Tree/${id}`;
        return axiosClient.delete(url);
    }
};

export default treeApi;