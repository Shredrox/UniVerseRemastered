import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { FaUserAstronaut } from "react-icons/fa";
import { approveUser, getUserRegistrationRequests, rejectUser } from "../services/userService";

const Admin = () => {
  const queryClient = useQueryClient();

  const { data: registrationRequests } = useQuery({
    queryKey: ["registrationRequests"],
    queryFn: getUserRegistrationRequests
  })

  const {mutateAsync: approveUserMutation} = useMutation({
    mutationFn: approveUser,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["registrationRequests"]
      });
    },
  });

  const {mutateAsync: rejectUserMutation} = useMutation({
    mutationFn: rejectUser,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["registrationRequests"]
      });
    },
  });

  return (
    <div className='admin-page'>
      <h2>User Registration Requests</h2>
      <div className="registration-requests-list">
        {registrationRequests?.length === 0 && <span>No Registration Requests.</span>}
        {registrationRequests?.map((user, index) => (
          <div key={index} className="user-registration-request">
            <div className="user-registration-request-info">
              <div className='profile-picture'>
                <FaUserAstronaut className='profile-picture-placeholer-icon'/>
              </div>
              <div style={{display: "flex", flexDirection: "column"}}>
                <span>Username: {user.username}</span>
                <span>Email: {user.email}</span>
              </div>
            </div>
            
            <div style={{display: "flex", gap: "0.5rem"}}>
              <button 
                className="confirm-button"
                onClick={() => approveUserMutation(user.username)}>
                  Approve
              </button>
              <button 
                className="cancel-button"
                onClick={() => rejectUserMutation(user.username)}>
                  Reject
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}

export default Admin
