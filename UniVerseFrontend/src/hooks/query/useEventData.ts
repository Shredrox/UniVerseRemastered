import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { attendEvent, deleteEvent, getIsAttending, removeAttending } from "../../services/eventService";

const useEventData = (eventId : number, user : string) =>{
  const queryClient = useQueryClient();

  const {data: isAttending, 
    isLoading: isAttendingLoading, 
    isError: isAttendingError, 
    error: attendingError
  } = useQuery({ 
    queryKey: ["eventIsAttending", eventId, user],
    queryFn: () => getIsAttending(eventId, user),
  });

  const {mutateAsync: attendEventMutation} = useMutation({
    mutationFn: attendEvent,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["eventIsAttending", eventId, user]
      });
    },
  });

  const {mutateAsync: removeAttendingMutation} = useMutation({
    mutationFn: removeAttending,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["eventIsAttending", eventId, user]
      });
    },
  });

  const {mutateAsync: deleteEventMutation} = useMutation({
    mutationFn: deleteEvent,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["events"]
      });
    },
  });

  return {
    isAttending, 
    isAttendingLoading, 
    isAttendingError, 
    attendingError,
    attendEventMutation,
    removeAttendingMutation,
    deleteEventMutation
  }
}

export default useEventData
