import { useNavigate, useParams } from "react-router-dom"
import useAuth from "../hooks/auth/useAuth";
import { IoBriefcase } from "react-icons/io5";
import { FaLocationDot, FaMoneyBills } from "react-icons/fa6";
import { HiBuildingOffice2 } from "react-icons/hi2";
import Loading from '../components/fallback/Loading'
import useJobData from "../hooks/query/useJobData";

const JobDetails = () => {
  const { jobId } = useParams();
  const { auth } = useAuth();

  const navigate = useNavigate();

  const { 
    jobData, 
    isJobLoading,
    isJobError,
    jobError,
    applyToJobMutation,
    cancelApplicationToJobMutation,
    deleteJobMutation 
  } = useJobData(jobId, auth?.username);

  const handleApply = () =>{
    if(jobData.isApplied){
      cancelApplicationToJobMutation({jobId: jobId, username: auth?.username})
      return;
    }

    applyToJobMutation({jobId: jobId, username: auth?.username})
  }

  const handleDelete = async () =>{
    await deleteJobMutation(jobId);

    navigate('/jobs');
  }

  if(isJobError){
    throw jobError;
  }

  if(isJobLoading){
    return <Loading/>
  }

  return (
    <div className="job-details-container">
      <div className="job-details">
        <h2>{jobData.job?.title}</h2>
        <h3>{jobData.job?.company}</h3>
        <span className="job-text-field"><HiBuildingOffice2 className="job-icon"/>{jobData.job?.requirements}</span>
        <span className="job-text-field"><FaLocationDot className="job-icon"/>{jobData.job?.location}</span>
        <span className="job-text-field"><IoBriefcase className="job-icon"/>{jobData.job?.type}</span>
        <span className="job-text-field"><FaMoneyBills className="job-icon"/>{jobData.job?.salary}</span>
        <p className="job-description">{jobData.job?.description}</p>
        <button 
        onClick={handleApply} 
        className="job-confirm-button">
          {jobData.isApplied ? "Cancel Application" : "Apply"}
        </button>
        {auth?.role === "ADMIN" && 
        <button 
          onClick={handleDelete} 
          className="cancel-button">
            Delete
        </button>
        }
      </div>
    </div>
  )
}

export default JobDetails
