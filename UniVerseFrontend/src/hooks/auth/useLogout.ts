import axios from "../../axios/axios";
import { useSocket } from "../useSocket";
import useAuth from "./useAuth";

const useLogout = () =>{
  const { auth, setAuth } = useAuth();
  const { sendIsOnlineAlert } = useSocket();

  const logout = async () => {
    sendIsOnlineAlert(auth?.username);

    setAuth({}); 
    try{
      await axios.post('Auth/logout',{},{
        withCredentials: true
      });
    }catch(error){
      console.log(error);
    }
  }

  return logout;
}

export default useLogout;
