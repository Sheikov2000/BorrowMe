import { getToken } from "./authManager";

const apiUrl = "/api/Item";

export const getAllItems = () => {
    return getToken().then((token) => {
      return fetch(apiUrl, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((resp) => {
        if (resp.ok) {
          return resp.json();
        } else {
          throw new Error(
            "An unknown error occurred while trying to get all items."
          );
        }
      });
    });
  };


export const getItemById = (id) => {
    return getToken().then((token) => {
      return fetch(`${apiUrl}/${id}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((resp) => {
        if (resp.ok) {
          return resp.json();
        } else {
          throw new Error(
            "An unknown error occurred while trying to fetch the item with that id."
          );
        }
      });
    });
  };

  export const getAllUserItems = (userId) => {
    return getToken().then((token) => {
      return fetch(`${apiUrl}/User/${userId}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((resp) => {
        if (resp.ok) {
          return resp.json();
        } else {
          throw new Error(
            "An unknown error occurred while trying to fetch the user's items."
          );
        }
      });
    });
  };
  
  export const addItem = (item) => {
    return getToken().then((token) => {
      return fetch(apiUrl, {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((resp) => {
        if (resp.ok) {
          return resp.json();
        } else if (resp.status === 401) {
          throw new Error("Unauthorized");
        } else {
          throw new Error(
            "An unknown error occurred while trying to save a new item."
          );
        }
      });
    });
  };

  export const editItem = (item) => {
    return getToken().then((token) => {
      return fetch(apiUrl, {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((resp) => {
        if (resp.ok) {
          return resp.json();
        } else if (resp.status === 401) {
          throw new Error("Unauthorized");
        } else {
          throw new Error(
            "An unknown error occurred while trying to save a new item."
          );
        }
      });
    });
  };

export const DeleteItem = (id) => {
    return fetch(`${apiUrl}/${id}`, {
        method: "DELETE",
    });

};