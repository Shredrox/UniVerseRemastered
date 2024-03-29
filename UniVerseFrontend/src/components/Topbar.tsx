import { useEffect, useState } from 'react'
import SearchIcon from '../assets/icons/icon-search-outline.svg'
import { useQuery } from '@tanstack/react-query';
import { useNavigate } from 'react-router-dom';
import ErrorFallback from './fallback/ErrorFallback';
import Loading from './fallback/Loading';
import { getUsersByFilter } from '../services/userService';
import User from '../interfaces/user/User';

const Topbar = () => {
  const navigate = useNavigate();

  const [search, setSearch] = useState('');

  const {data: searchUsers, isError, error, refetch} = useQuery({ 
    queryKey: ["searchUsers", search],
    queryFn: () => getUsersByFilter(search),
    enabled: false,
  });

  useEffect(() => {
    const queryDelay = setTimeout(() => {
      if(search !== ''){
        refetch();
      }
    }, 1000)

    return () => clearTimeout(queryDelay)
  }, [search])

  return (
    <div className='topbar'>
      <div className="search-container">
        <div className='search-bar'>
          <img src={SearchIcon}/>
          <input value={search} onChange={(e) => setSearch(e.target.value)} className='search-bar-input' type="text" placeholder="Search.."/>
        </div>
        {search !== '' &&
        <div className="search-results">
          {searchUsers?.length > 0 ?
          searchUsers?.map((user : User, index : number) =>
            <div onClick={() => {navigate(`/profile/${user?.username}`); setSearch('');}} 
            className='search-result' key={index}>
              <div className='chat-profile-picture'></div>
              {user?.username}
            </div>
          )
          : searchUsers?.length === 0 ? "User Not Found" 
          : isError ? <ErrorFallback error={error.message}/>
          : <Loading/>
          }
        </div>
        }
      </div>
    </div>
  )
}

export default Topbar
