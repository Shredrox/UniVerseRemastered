import { useNavigate, useParams } from "react-router-dom"
import Loading from '../components/fallback/Loading'
import useAuth from "../hooks/auth/useAuth";
import { useState } from "react";
import NewsEditForm from "../components/news/NewsEditForm";
import useNewsData from "../hooks/query/useNewsData";
import useNewsImage from "../hooks/query/useNewsImage";

const NewsDetails = () => {
  const { auth } = useAuth();
  const { newsId } = useParams();

  const navigate = useNavigate();

  const { 
    news, 
    isNewsLoading, 
    isNewsError, 
    newsError, 
    updateNewsMutation,
    deleteNewsMutation
  } = useNewsData(newsId);

  const { newsImage } = useNewsImage(newsId);

  const [isEditOn, setIsEditOn] = useState(false);

  const handleDelete = async () =>{
    await deleteNewsMutation(newsId);

    navigate('/news');
  }

  if(isNewsError){
    throw Error(newsError);
  }

  if(isNewsLoading){
    return <Loading/>
  }

  return (
    <div className="news-details-container">
      {newsImage?.size > 0 && 
      <div className="news-details-image-container">
        <img src={URL.createObjectURL(newsImage)} alt='NewsImage'/> 
      </div>
      }
      <h4>{news.title}</h4>
      {news.date}
      <p>{news.content}</p>
      
      {auth?.role === "ADMIN" && 
      <div>
        <button 
          onClick={() => setIsEditOn(!isEditOn)} 
          className="confirm-button">
            Edit
        </button>
        <button 
          onClick={handleDelete} 
          className="cancel-button">
            Delete
        </button>
      </div>
      }
      {isEditOn && 
      <NewsEditForm 
        news={news} 
        setIsEditOn={setIsEditOn} 
        updateNewsMutation={updateNewsMutation}
      />
      }
    </div>
  )
}

export default NewsDetails
