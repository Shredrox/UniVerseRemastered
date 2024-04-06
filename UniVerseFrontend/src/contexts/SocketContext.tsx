import { ReactNode, createContext, useEffect, useState } from "react"
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import Message from "../interfaces/Message";
import axios from "../axios/axios";
import ChatInterface from "../interfaces/ChatInterface";
import SocketContextType from "../interfaces/SocketContextType";
import Notification from "../interfaces/Notification";
import FriendRequest from "../interfaces/FriendRequest";
import useAuth from "../hooks/auth/useAuth";

interface SocketProviderProps {
  children: ReactNode;
}

export const SocketContext = createContext<SocketContextType | null>(null);

export const SocketProvider = ({ children } : SocketProviderProps) => {
  const [connection, setConnection] = useState<HubConnection | null>(null);

  const { auth } = useAuth();
  
  const [notifications, setNotifications] = useState<Notification[]>([]);
  const [messages, setMessages] = useState<Message[]>([]);
  const [chats, setChats] = useState<ChatInterface[]>([]);
  const [friendRequests, setFriendRequests] = useState<FriendRequest[]>([]);
  const [newOnlineFriend,  setNewOnlineFriend] = useState(false);

  const createHubConnection = () =>{
    const newConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5135/signal-hub")
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }

  const disconnectFromHub = () =>{
    if(connection){
      connection.stop();
      setConnection(null);
      console.log("Disconnected from SignalR hub")
    }
  }

  useEffect(() => {
    if(auth.username !== undefined && connection === null){
      createHubConnection();
    }
  }, [auth.username]);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => console.log('SignalR Connected'))
        .catch((error) => console.log('SignalR Connection Error: ', error));

      connection.on('ReceiveMessage', onMessageReceived);
      connection.on('ChatCreated', onChatCreated);
      connection.on('ReceiveNotification', onNotificationReceived);
      connection.on('ReceiveFriendRequest', onFriendRequestReceived);
      connection.on('ReceiveOnlineAlert', onFriendOnline);
    }
  }, [connection]);

  //Chats
  const onMessageReceived = (message : Message) => {
    setMessages((prevMessages) => [...prevMessages, message]);
  }

  const sendMessage = async ({message, sender, receiver} : 
    { message: string, sender: string, receiver: string }) =>{
    try {
      const chatMessage = {
        sender: sender,
        content: message,
        receiver: receiver
      };

      await axios.post('Chat/send-private-message', chatMessage);
    } catch (error) {
      console.error('Error sending message:', error);
    }
  }

  const setChatMessages = (newMessages : Message[]) =>{
    setMessages(newMessages);
  }

  const onChatCreated = (chat : ChatInterface) =>{
    setChats((prevChats) => [...prevChats, chat]);
  }

  const createChat = async (chat : ChatInterface) =>{
    try {
      const newChat = {
        user1Name: chat.user1,
        user2Name: chat.user2
      }
      await axios.post('Chat/create-chat', newChat);
    } catch (error) {
      console.error('Error creating chat:', error);
    }
  }

  const setUserChats = (newUserChats : ChatInterface[]) =>{
    setChats(newUserChats);
  }

  //Notifications
  const sendNotification = async ({message, type, source, recipientName} : 
    { message: string, type: string, source: string, recipientName : string }) =>{
    try {
      const notification = {
        message,
        type,
        source,
        recipientName
      };

      await axios.post('Notification/send-notification', notification);
    } catch (error) {
      console.error('Error sending notification:', error);
    }
  }

  const onNotificationReceived = (notification : Notification) =>{
    setNotifications((prevNotifications) => [...prevNotifications, notification]);
  } 

  const setUserNotifications = (newNotifications : Notification[]) =>{
    setNotifications(newNotifications);
  }

  //Friend Requests
  const onFriendRequestReceived = (friendRequest : FriendRequest) => {
    setFriendRequests((prevFriendRequests) => [...prevFriendRequests, friendRequest]);
  }

  const sendFriendRequest = async (friendRequest : {sender : string, receiver : string}) =>{
    try {
      await axios.post('Friendship/send-friend-request', friendRequest);
    } catch (error) {
      console.error('Error sending friend request:', error);
    }
  }

  const setUserFriendRequests = (newFriendRequests : FriendRequest[]) =>{
    setFriendRequests(newFriendRequests);
  }

  //Online Friends
  const onFriendOnline = () =>{
    setNewOnlineFriend(!newOnlineFriend);
  }

  const sendIsOnlineAlert = async (username : string) =>{
    try {
      await axios.post('Friendship/send-online-alert', {},
      {
        params: {username}
      });
    } catch (error) {
      console.error('Error sending online alert:', error);
    }
  }

  const contextValue : SocketContextType = {
    notifications, 
    sendNotification,
    setUserNotifications,
    createHubConnection,
    disconnectFromHub,
    messages,
    sendMessage,
    setChatMessages,
    createChat,
    setUserChats,
    chats,
    friendRequests,
    sendFriendRequest,
    setUserFriendRequests,
    newOnlineFriend,
    setNewOnlineFriend,
    sendIsOnlineAlert
  };

  return (
    <SocketContext.Provider value={contextValue}>
      {children}
    </SocketContext.Provider>
  )
}
