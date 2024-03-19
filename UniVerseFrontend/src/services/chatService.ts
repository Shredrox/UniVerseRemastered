import axios from "../axios/axios";

export const getChats = async (username : string) => {
  const response = await axios.get('/chats/getUserChats', {
    params: {
      user: username
    }
  });

  return response.data;
}

export const getChat = async (user : string, chatUser: string) => {
  const response = await axios.get('/chats/getMessages', {
    params: {
     user: user,
     chatUser: chatUser
    }
  });

  return response.data;
}
