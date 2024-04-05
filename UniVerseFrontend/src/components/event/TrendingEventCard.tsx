import useAuth from "../../hooks/auth/useAuth";
import useEventData from "../../hooks/query/useEventData";
import GroupEvent from "../../interfaces/GroupEvent";

interface TrendingEventCardProps{
  event: GroupEvent;
}

const TrendingEventCard = ({ event } : TrendingEventCardProps) => {
  const { auth } = useAuth();

  const { 
    isAttending, 
    attendEventMutation,
    removeAttendingMutation
  } = useEventData(event.id, auth?.username);

  const handleClick = () =>{
    if(isAttending){
      removeAttendingMutation({eventId: event.id, username: auth?.username});
      return;
    }

    attendEventMutation({eventId: event.id, username: auth?.username});
  }

  return (
    <div className='trending-event' >
      <img src="https://picsum.photos/200/100" alt="EventImage" />
      <h3>{event.title}</h3>
      <button 
        onClick={handleClick} 
        className="confirm-button">
          Attend
      </button>
    </div>
  )
}

export default TrendingEventCard
