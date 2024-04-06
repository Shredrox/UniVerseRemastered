import { useParams } from "react-router-dom"
import useAuth from "../hooks/auth/useAuth";
import Loading from '../components/fallback/Loading'
import { useState } from "react";
import useProfileData from "../hooks/query/useProfileData";
import ProfileEditForm from "../components/profile/ProfileEditForm";
import { FaUserAstronaut } from "react-icons/fa";

const Profile = () => {
  const { auth } = useAuth();
  const { username } = useParams();

  const { 
    profileData, 
    isProfileLoading, 
    isProfileError, 
    profileError,
    addFriendMutation,
    removeFriendMutation,
    updateUserProfileMutation 
  } = useProfileData(username, auth?.username);

  const [isEditOn, setIsEditOn] = useState(false);

  if(isProfileError){
    throw profileError;
  }

  if(isProfileLoading){
    return <Loading/>
  }

  return (
    <div className="profile-page-container">
      <div className="profile-page-tab">
        <div className="profile-page-picture-container">
          {profileData.profilePicture.size > 0 ? 
          <img className='profile-page-profile-picture' src={URL.createObjectURL(profileData.profilePicture)} alt='postImage'/> 
          : 
          <FaUserAstronaut className='profile-picture-placeholder-icon'/>}
        </div>
        <h3>{profileData.user?.username}</h3>
        <div className="profile-social-tab">
          <div className="profile-counts"><span>Friends</span>{profileData.friendsCount}</div>
          <div className="profile-counts"><span>Posts</span>{profileData.postsCount}</div>
        </div>

        {!profileData.loggedInUserProfile && 
        <button 
          className="profile-friend-button" 
          onClick={() => profileData.friendshipStatus === "ACCEPTED" ? 
            removeFriendMutation({ loggedInUser: auth?.username, profileUser: profileData.user?.username }) 
            : 
            addFriendMutation({ sender: auth?.username, receiver: profileData.user?.username })}>
            {profileData.friendshipStatus === "ACCEPTED" ? "Remove Friend" : profileData.friendshipStatus === "PENDING" ? "Pending" : "Add Friend"}
        </button>
        }

        {profileData.loggedInUserProfile && 
        <div className="user-details">
          {profileData.user?.email}
          {isEditOn ? 
          <ProfileEditForm 
            profileUser={profileData.user} 
            updateUserProfileMutation={updateUserProfileMutation}
            setIsEditOn={setIsEditOn}
          />
          :
          <button onClick={() => setIsEditOn(true)} className="confirm-button">Edit</button>
          }
        </div>
        }
      </div>
    </div>
  )
}

export default Profile
