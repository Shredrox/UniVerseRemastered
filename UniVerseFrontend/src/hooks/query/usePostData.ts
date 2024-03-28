import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { deletePost, getIsLiked, getPostCommentCount, getPostImage, getPostLikes, likePost, unlikePost } from "../../services/postService";

const usePostData = (postId : number, user : string) =>{
  const queryClient = useQueryClient();

  const {data: postLikes, 
    isLoading: likesLoading, 
    isError: isLikesError, 
    error: likesError
  } = useQuery({ 
    queryKey: ["postLikes", postId],
    queryFn: () => getPostLikes(postId),
  });
  
  const {data: postImage, 
    isLoading: postImageLoading, 
    isError: isPostImageError, 
    error: postImageError
  } = useQuery({ 
    queryKey: ["postImage", postId],
    queryFn: () => getPostImage(postId),
  });
  
  const {data: postCommentCount, 
    isLoading: commentCountLoading, 
    isError: isCommentsError, 
    error: commentsError
  } = useQuery({ 
    queryKey: ["postCommentCount", postId],
    queryFn: () => getPostCommentCount(postId),
  });
  
  const {data: isLiked, 
    isLoading: postLikedLoading, 
    isError: isLikedError, 
    error: likedError
  } = useQuery({ 
    queryKey: ["postLiked", postId, user],
    queryFn: () => getIsLiked(postId, user),
  });

  const {mutateAsync: likePostMutation} = useMutation({
    mutationFn: likePost,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["postLikes", postId]
      });
      queryClient.invalidateQueries({
        queryKey: ["postLiked", postId, user]
      });
    },
  });

  const {mutateAsync: unlikePostMutation} = useMutation({
    mutationFn: unlikePost,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["postLikes", postId]
      });
      queryClient.invalidateQueries({
        queryKey: ["postLiked", postId, user]
      });
    },
  });

  const {mutateAsync: deletePostMutation} = useMutation({
    mutationFn: deletePost,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["posts"]
      });
    },
  });

  const isPostError =  isLikesError || isPostImageError || isCommentsError ||  isLikedError;
  const postError = likesError || postImageError || commentsError || likedError;
  const isPostLoading = likesLoading || postImageLoading || commentCountLoading || postLikedLoading;

  return {
    postData: {postLikes, postImage, postCommentCount, isLiked}, 
    isPostLoading, 
    isPostError, 
    postError,
    likePostMutation,
    unlikePostMutation,
    deletePostMutation
  }
}

export default usePostData
