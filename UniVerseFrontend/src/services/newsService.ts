import axios from "../axios/axios";

export const getNews = async () =>{
  const response = await axios.get('News');
  return response.data;
}

export const getNewsImage = async (id : number) =>{
  const response = await axios.get(`News/${id}/image`, { responseType: 'blob'});
  return response.data;
}

export const getNewsById = async (newsId : number) =>{
  const response = await axios.get(`News/${newsId}`);
  return response.data;
}

export const addNews = async (news : {title: string, content: string, pinned: boolean, image: File}) =>{
  return await axios.post('News/create-news', news);
}

export const updateNews = async (data : FormData) =>{
  return await axios.post(`News/update`, data);
}

export const deleteNews = async (newsId : number) =>{
  return await axios.delete(`News/delete/${newsId}`);
}
