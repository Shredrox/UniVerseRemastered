import axios from "../axios/axios";

export const getUserNotifications = async (username : string) =>{
  const response = await axios.get(`Notification/${username}`);
  return response.data;
}

export const readUserNotifications = async (username : string) =>{
  const response = await axios.post(`Notification/${username}/set-read`);
  return response.data;
}
