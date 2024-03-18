import axios from "../../axios/axios";
import useAuth from "./useAuth";

const useLogout = () =>{
  const { setAuth } = useAuth();

  const logout = async () => {
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
