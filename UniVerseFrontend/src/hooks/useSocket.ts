import { useContext } from "react"
import { SocketContext } from "../contexts/SocketContext";

export const useSocket = () => {
  const context = useContext(SocketContext);
  
  if (context === null) {
    throw new Error("useSocket must be used within an SocketProvider");
  }

  return context;
}
