import ChatInterface from "./ChatInterface";
import FriendRequest from "./FriendRequest";
import Message from "./Message";
import Notification from "./Notification";

export default interface SocketContextType {
  notifications: Notification[];
  messages: Message[];
  sendNotification: ({message, type, source, recipientName} : { message: string, type: string, source: string, recipientName : string }) => Promise<void>;
  setUserNotifications: (notifications : Notification[]) => void;
  setUserChats: (newUserChats : ChatInterface[]) => void;
  createHubConnection: () => void;
  disconnectFromHub: () => void;
  setChatMessages: (newMessages : Message[]) => void; //
  sendMessage: ({ message, sender, receiver }: { message: string; sender: string; receiver: string; }) => Promise<void>;
  createChat: (chat: ChatInterface) => Promise<void>;
  chats: ChatInterface[];
  friendRequests: FriendRequest[];
  sendFriendRequest: (friendRequest : {sender : string, receiver : string}) => Promise<void>;
  setUserFriendRequests: (newFriendRequests : FriendRequest[]) => void;
  sendIsOnlineAlert: (username : string) => void;
  newOnlineFriend: boolean;
  setNewOnlineFriend: React.Dispatch<React.SetStateAction<boolean>>;
}
