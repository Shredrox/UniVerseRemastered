import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { addPostComment, getPostComments } from "../../services/postService";

const useCommentsData = (postId : number) =>{
  const queryClient = useQueryClient();

  const {data: comments, 
    isLoading : isCommentsLoading, 
    isError: isCommentsError, 
    error: commentsError
  } = useQuery({ 
    queryKey: ["postComments", postId],
    queryFn: () => getPostComments(postId),
  });

  const {mutateAsync: addCommentMutation} = useMutation({
    mutationFn: addPostComment,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["postComments", postId]
      });
    },
  });

  return {
    comments,
    isCommentsLoading,
    isCommentsError,
    commentsError,
    addCommentMutation
  }
}

export default useCommentsData;