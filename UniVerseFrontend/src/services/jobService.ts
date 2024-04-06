import axios from "../axios/axios"
import Job from "../interfaces/Job";

export const getJobs = async () : Promise<Job[]> =>{
  const response = await axios.get('Job');
  return response.data;
}

export const getJobById = async (id : number) : Promise<Job> =>{
  const response = await axios.get(`Job/${id}`);
  return response.data;
}

export const getIsAppliedToJob = async (jobId : number, username : string) =>{
  const response = await axios.get(`Job/${jobId}/is-applied/${username}`);
  return response.data;
}

export const applyToJob = async ({jobId, username} : {jobId:number, username:string}) =>{
  const response = await axios.post(`Job/${jobId}/apply/${username}`);
  return response.data;
}

export const cancelApplicationToJob = async ({jobId, username} : {jobId:number, username:string}) =>{
  const response = await axios.post(`Job/${jobId}/cancel-application/${username}`);
  return response.data;
}

// export const addJob = async (job) =>{
//   return await axios.post('Job/create-job', job);
// }

// export const updateJob = async (job) =>{
//   return await axios.patch(`Job/${job.id}`, job);
// }

export const deleteJob = async (id : number) =>{
  return await axios.delete(`Job/${id}`);
}
