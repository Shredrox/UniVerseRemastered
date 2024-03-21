import axios from "../axios/axios";

export const getUsers = async () =>{
  const response = await axios.get('User');
  return response.data;
}

export const getUsersByFilter = async (filter) =>{
  const response = await axios.get(`User/search/${filter}`);
  return response.data;
}

export const userExists = async (username) =>{
  const response = await axios.get(`User/exists/${username}`);
  return response.data;
}

export const getUserByName = async (username) =>{
  const response = await axios.get(`User/${username}`);
  return response.data;
}

export const getUserOnlineFriends = async (username) =>{
  const response = await axios.get(`Friendship/${username}/online-friends`);
  return response.data;
}

export const getUserFriendsCount = async (username) =>{
  const response = await axios.get(`Friendship/${username}/count`);
  return response.data;
}

export const getUserProfilePicture = async (username) =>{
  const response = await axios.get(`User/${username}/profile-picture`, { responseType: 'blob'});
  return response.data;
}

export const getUserFriendRequests = async (username) =>{
  const response = await axios.get(`Friendship/${username}/friend-requests`);
  return response.data;
}

export const checkFriendship = async (user1Name, user2Name) => {
  const response = await axios.get('Friendship/check-friendship', {
    params: {
      user1Name,
      user2Name,
    },
  });

  return response.data;
};

export const addFriend = async ({ loggedInUser, profileUser }) =>{
  const response = await axios.post(`Friendship/${loggedInUser}/add-friend/${profileUser}`);
  return response.data;
}

export const acceptFriendRequest = async (friendshipId) =>{
  const response = await axios.post(`Friendship/accept-friend-request/${friendshipId}`);
  return response.data;
}

export const removeFriend = async ({ loggedInUser, profileUser }) =>{
  const response = await axios.delete(`Friendship/${loggedInUser}/remove-friend/${profileUser}`);
  return response.data;
}

export const rejectFriendRequest = async (friendshipId) =>{
  const response = await axios.delete(`Friendship/reject-friend-request/${friendshipId}`);
  return response.data;
}

export const confirmPassword = async ({ username, password }) =>{
  const details = { username: username, email: "", password: password };

  const response = await axios.post('Auth/confirm-password', details);
  return response.data;
}

export const updateUserProfile = async (data) =>{
  const response = await axios.post('User/update-profile', data);
  return response.data;
}
