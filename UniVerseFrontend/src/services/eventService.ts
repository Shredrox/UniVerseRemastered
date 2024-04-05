import axios from "../axios/axios"
import GroupEvent from "../interfaces/GroupEvent";

export const getEvents = async () : Promise<GroupEvent[]> =>{
  const response = await axios.get('GroupEvent');
  return response.data;
}

export const getTrendingEvents = async () : Promise<GroupEvent[]> =>{
  const response = await axios.get('GroupEvent/trending');
  return response.data;
}

export const getIsAttending = async (eventId : number, username : string) => {
  const response = await axios.get(`GroupEvent/${eventId}/is-attending/${username}`);
  return response.data;
}

export const attendEvent = async ({eventId, username} : {eventId: number, username: string}) =>{
  const response = await axios.post(`GroupEvent/${eventId}/attend/${username}`);
  return response.data;
}

export const removeAttending = async ({eventId, username} : {eventId: number, username: string}) =>{
  const response = await axios.post(`GroupEvent/${eventId}/remove-attending/${username}`);
  return response.data;
}

export const addEvent = async (event : GroupEvent) =>{
  return await axios.post('GroupEvent/create-event', event);
}

export const updateEvent = async (event : GroupEvent) =>{
  return await axios.patch(`GroupEvent/${event.id}`, event);
}

export const deleteEvent = async (id : number) =>{
  return await axios.delete(`GroupEvent/${id}`);
}
