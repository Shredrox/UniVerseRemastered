import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { addCommentReply, getCommentReplies } from "../../services/postService";

const useCommentData = (commentId : number) =>{
  const queryClient = useQueryClient();

  const {data: replies, 
    isLoading : isRepliesLoading, 
    isError: isRepliesError, 
    error: repliesError
  } = useQuery({ 
    queryKey: ["commentReplies", commentId],
    queryFn: () => getCommentReplies(commentId),
  });

  const {mutateAsync: addReplyMutation} = useMutation({
    mutationFn: addCommentReply,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["commentReplies", commentId]
      });
    },
  });

  return {
    replies, 
    isRepliesLoading, 
    isRepliesError, 
    repliesError,
    addReplyMutation
  }
}

export default useCommentData
