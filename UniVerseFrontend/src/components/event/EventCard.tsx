import useAuth from "../../hooks/auth/useAuth";
import useEventData from "../../hooks/query/useEventData";
import GroupEvent from "../../interfaces/GroupEvent";

interface EventCardProps{
  event: GroupEvent;
}

const EventCard = ({ event } : EventCardProps) => {
  const { auth } = useAuth();

  const { 
    isAttending, 
    attendEventMutation,
    removeAttendingMutation,
    deleteEventMutation
  } = useEventData(event.id, auth?.username);

  const handleClick = () =>{
    if(isAttending){
      removeAttendingMutation({eventId: event.id, username: auth?.username});
      return;
    }

    attendEventMutation({eventId: event.id, username: auth?.username});
  }

  const handleDelete = async () =>{
    await deleteEventMutation(event.id);
  }

  return (
    <div className='event'>
      <img src="https://picsum.photos/250/200" alt="EventImage" />
      <div className="event-info">
        <h3>{event.title}</h3>
        <h4>{event.date}</h4>
        <span>{event.description}</span>
        <button 
          onClick={handleClick} 
          className="confirm-button">
          {isAttending ? "Attending" : "Attend"}
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

export default EventCard
