import Event from '../components/event/Event';
import { MdEventAvailable } from "react-icons/md";
import { IoTrendingUp } from "react-icons/io5";
import TrendingEvent from '../components/event/TrendingEvent';
import useAuth from "../hooks/auth/useAuth";
import Loading from '../components/fallback/Loading'
import useEventsData from '../hooks/query/useEventsData';

const Events = () => {
  const { auth } = useAuth();

  const { 
    eventsData, 
    isEventsLoading,
    isEventsError,
    eventsError,
  } = useEventsData(auth?.username);

  if(isEventsError){
    throw Error(eventsError);
  }

  if(isEventsLoading){
    return <Loading/>
  }

  return (
    <div className='events-container'>
      <div className="trending-events-container">
        <h2><IoTrendingUp className='event-icon'/>Trending Events</h2>
        <div className='trending-events-list'>
        {eventsData.trendingEvents?.map((event, index) =>
          <TrendingEvent key={index} event={event}/>
        )}
        </div>
      </div>
      <div className="events-list-container">
      <h2><MdEventAvailable className='event-icon'/>Events</h2>
        <div className='events-list'>
        {eventsData.events?.map((event, index) =>
          <Event key={index} event={event}/>
        )}
        </div>
      </div>
    </div>
  )
}

export default Events
