import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useEffect } from "react";
import { useSocket } from "./useSocket";
import { getChat } from "../../services/chatService";

const useMessagesData = (loggedUser : string, chatUser: string) =>{
  const { sendMessage, setChatMessages, messages } = useSocket();

  const queryClient = useQueryClient();

  const {data: messagesData, 
    isLoading: isMessagesLoading, 
    isError: isMessagesError, 
    error: messagesError
  } = useQuery({ 
    queryKey: ["chat", loggedUser, chatUser],
    queryFn: () => getChat(loggedUser, chatUser),
  });

  const {mutateAsync: sendMessageMutation} = useMutation({
    mutationFn: sendMessage,
    onSuccess: () =>{
      queryClient.invalidateQueries(["chat", loggedUser, chatUser]);
    },
  });

  useEffect(() => {
    if (messagesData) {
      setChatMessages(messagesData);
    }
  }, [messagesData]);

  return {
    messages, 
    isMessagesLoading, 
    isMessagesError, 
    messagesError,
    sendMessageMutation,
  }
}

export default useMessagesData 
