import CommentCard from "./CommentCard";
import { useEffect, useState } from "react";
import useAuth from '../../hooks/auth/useAuth'
import Loading from '../../components/fallback/Loading'
import ErrorFallback from '../../components/fallback/ErrorFallback'
import PostInterface from "../../interfaces/post/PostInterface";
import { useSocket } from "../../hooks/useSocket";
import useCommentsData from "../../hooks/query/useCommentsData";

interface CommentSectionProps{
  post: PostInterface;
}

const CommentSection = ({ post } : CommentSectionProps) => {
  const [commentText, setCommentText] = useState('');
  const { sendNotification } = useSocket();

  const { auth } = useAuth();

  const [isError, setIsError] = useState(false);
  const [error, setError] = useState('');

  const { 
    comments,
    isCommentsLoading,
    isCommentsError,
    commentsError,
    addCommentMutation
  } = useCommentsData(post.id);

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
    sendNotification(
      { 
        message: `${auth?.username} commented on your post: "${commentText}"`, 
        type: "Comment", 
        source: "Feed", 
        recipientName: post.authorName
      }
    ); 
    setCommentText('');
  }

  if(isCommentsLoading){
    return <Loading/>
  }

  if(isCommentsError){
    return <ErrorFallback error={commentsError.message}/>
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
        <CommentCard key={index} comment={comment} isReply={false}/>
      )
      :
        <div>No Comments.</div>
      }
      </div>
    </div>
  )
}

export default CommentSection
