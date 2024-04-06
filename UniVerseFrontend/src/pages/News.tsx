import NewsCard from '../components/news/NewsCard'
import Loading from '../components/fallback/Loading'
import useAuth from '../hooks/auth/useAuth';
import { useState } from 'react';
import useNewsListData from '../hooks/query/useNewsListData';
import CreateNewsForm from '../components/news/CreateNewsForm';
import NewsInterface from '../interfaces/NewsInterface';

const News = () => {
  const { auth } =  useAuth();

  const { 
    news, 
    isNewsDataLoading,
    isNewsDataError,
    newsDataError,
    addNewsMutation
  } = useNewsListData();

  const [isAddingNews, setIsAddingNews] = useState(false);

  if(isNewsDataError){
    throw newsDataError;
  }

  return (
    <div className='news-container'>
      <h2>Latest News</h2>
      {auth?.role === "ADMIN" &&
        <button onClick={() => setIsAddingNews(!isAddingNews)} className='confirm-button'>Create News</button>
      }
      {isAddingNews &&
        <CreateNewsForm 
          setIsAddingNews={setIsAddingNews}
          addNewsMutation={addNewsMutation}
        />
      }
      <div className='news-list'>
        {isNewsDataLoading ? <Loading/> :
        news.length > 0 ? 
        news?.map((news : NewsInterface, index : number) => (
          <NewsCard key={index} news={news}/>
        ))
        :
        <div>No news.</div>
        }
      </div>
    </div>
  )
}

export default News
