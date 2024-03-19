import axios from "../../axios/axios";
import useAuth from "./useAuth"

const useRefreshToken = () => {
  const { setAuth } = useAuth();

  const refresh = async () =>{
    const response = await axios.post('Auth/refresh-token',{},{
      withCredentials: true
    });

    setAuth(prev => {
      return {...prev, 
        user: response.data.username,
        accessToken: response.data.accessToken,
        role: response.data.role
      }
    });
    return response.data.accessToken;
  }

  return refresh;
}

export default useRefreshToken
