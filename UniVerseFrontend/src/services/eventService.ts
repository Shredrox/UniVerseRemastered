import axios from "../axios/axios"

export const getEvents = async () =>{
  const response = await axios.get('GroupEvent');
  return response.data;
}

export const getTrendingEvents = async () =>{
  const response = await axios.get('GroupEvent/trending');
  return response.data;
}

export const getIsAttending = async (eventId, username) => {
  const response = await axios.get(`GroupEvent/${eventId}/is-attending/${username}`);
  return response.data;
}

export const attendEvent = async ({eventId, username}) =>{
  const response = await axios.post(`GroupEvent/${eventId}/attend/${username}`);
  return response.data;
}

export const removeAttending = async ({eventId, username}) =>{
  const response = await axios.post(`GroupEvent/${eventId}/remove-attending/${username}`);
  return response.data;
}

export const addEvent = async (event) =>{
  return await axios.post('GroupEvent/create-event', event);
}

export const updateEvent = async (event) =>{
  return await axios.patch(`GroupEvent/${event.id}`, event);
}

export const deleteEvent = async (id) =>{
  return await axios.delete(`GroupEvent/${id}`, id);
}
