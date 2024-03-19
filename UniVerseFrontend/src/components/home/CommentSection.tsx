import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { addPostComment, getPostComments } from "../../services/postService";
import Comment from "./Comment";
import { useEffect, useState } from "react";
import useAuth from '../../hooks/auth/useAuth'
import Loading from '../../components/fallback/Loading'
import ErrorFallback from '../../components/fallback/ErrorFallback'
import PostInterface from "../../interfaces/post/PostInterface";

interface CommentSectionProps{
  post: PostInterface;
}

const CommentSection = ({post} : CommentSectionProps) => {
  const [commentText, setCommentText] = useState('');

  const { auth } = useAuth();

  const [isError, setIsError] = useState(false);
  const [error, setError] = useState('');

  const queryClient = useQueryClient();

  const {data: comments, isLoading, isError: isQueryError, error: queryError} = useQuery({ 
    queryKey: ["postComments", post.id],
    queryFn: () => getPostComments(post.id),
  });

  const {mutateAsync: addCommentMutation} = useMutation({
    mutationFn: addPostComment,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey:["postComments", post.id]
      });
    },
  });

  useEffect(() => {
    setIsError(false);
    setError('');
  }, [commentText])

  const handleAddComment = () =>{
    if(commentText === ''){
      setIsError(true);
      setError('Comment cannot be empty');
      return;
    }

    addCommentMutation({postId: post.id, username: auth?.username, content: commentText});
    setCommentText('');
  }

  if(isLoading){
    return <Loading/>
  }

  if(isQueryError){
    return <ErrorFallback error={queryError.message}/>
  }

  return (
    <div className="comment-section">
      {isError && <ErrorFallback error={error}/>}
      <div className="comment-input">
        <textarea 
          className={`comment-textarea ${isError ? 'input-error' : ''}`}
          value={commentText} 
          onChange={(e) => setCommentText(e.target.value)} 
          placeholder='Comment...'
        />
        <button 
          className="comment-button"
          onClick={handleAddComment}>
            Comment
        </button>
      </div>
      <div className="comments-list">
      {comments?.length > 0 ? 
      comments?.map((comment, index) =>
        <Comment key={index} comment={comment} isReply={false}/>
      )
      :
        <div>No Comments.</div>
      }
      </div>
    </div>
  )
}

export default CommentSection
