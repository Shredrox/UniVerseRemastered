import axios from "../axios/axios"

export const getJobs = async () =>{
  const response = await axios.get('Job');
  return response.data;
}

export const getJobById = async (id) =>{
  const response = await axios.get(`Job/${id}`, id);
  return response.data;
}

export const getIsAppliedToJob = async (jobId, username) =>{
  const response = await axios.get(`Job/${jobId}/is-applied/${username}`);
  return response.data;
}

export const applyToJob = async ({jobId, username}) =>{
  const response = await axios.post(`Job/${jobId}/apply/${username}`);
  return response.data;
}

export const cancelApplicationToJob = async ({jobId, username}) =>{
  const response = await axios.post(`Job/${jobId}/cancel-application/${username}`);
  return response.data;
}

export const addJob = async (job) =>{
  return await axios.post('Job/create-job', job);
}

export const updateJob = async (job) =>{
  return await axios.patch(`Job/${job.id}`, job);
}

export const deleteJob = async (id) =>{
  return await axios.delete(`Job/${id}`, id);
}
