import { useQuery } from "@tanstack/react-query";
import { getUserProfilePicture } from "../../services/userService";

const useProfilePicture = (queryKey : string, user : string) =>{
  const {data: profilePicture, 
    isLoading: isProfilePictureLoading, 
    isError: isProfilePictureError, 
    error: profilePictureError
	} = useQuery({
    queryKey: [queryKey, user],
    queryFn: () => getUserProfilePicture(user),
  });

  return { 
    profilePicture, 
    isProfilePictureLoading,  
    isProfilePictureError, 
    profilePictureError 
  }
}

export default useProfilePicture
