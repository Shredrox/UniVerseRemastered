import useAuth from '../../hooks/auth/useAuth'
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Loading from '../fallback/Loading'
import ErrorFallback from '../fallback/ErrorFallback'
import useProfilePicture from "../../hooks/query/useProfilePicture";
import { FaUserAstronaut } from "react-icons/fa";
import { useSocket } from "../../hooks/useSocket";
import Comment from "../../interfaces/Comment";
import useCommentData from "../../hooks/query/useCommentData";

interface CommentCardProps{
  comment: Comment;
  isReply: boolean;
}

const CommentCard = ({ comment, isReply } : CommentCardProps) => {
  const [commentText, setCommentText] = useState('');
  const [replyOn, setReplyOn] = useState(false);
  const { sendNotification } = useSocket();

  const navigate = useNavigate();

  const { auth } = useAuth();

  const [isError, setIsError] = useState(false);
  const [error, setError] = useState('');

  const { 
    replies,
    isRepliesLoading,
    isRepliesError,
    repliesError,
    addReplyMutation 
  } = useCommentData(comment.id);

  const { profilePicture } = useProfilePicture("commentUserProfilePicture", comment.author);

  useEffect(() => {
    setIsError(false);
    setError('');
  }, [commentText])

  const handleReply = () =>{
    if(commentText === ''){
      setIsError(true);
      setError('Comment cannot be empty');
      return;
    }

    addReplyMutation({commentId: comment.id, username: auth?.username, content: commentText});
    sendNotification(
      { 
        message: `${auth?.username} replied to your comment: "${commentText}"`, 
        type: "Reply", 
        source: "Feed", 
        recipientName: comment.author
      }
    ); 
    setCommentText('')
    setReplyOn(false);
  }

  const handleCancel = () =>{
    setReplyOn(!replyOn);
    setError('');
    setIsError(false);
  }

  return (
    <div className={isReply ? "reply-container" : "comment-container"}>
      {isReply && <div className="vertical-line"/>}
      <div className={isReply ? "reply" : "comment"}>
        <div className={isReply ? "" : "comment-content"}>
          <div className='comment-author'>
            <div className='comment-profile-picture'>
              {profilePicture?.size > 0 ? 
              <img className='author-profile-picture' src={URL.createObjectURL(profilePicture)} alt="ProfilePicture" /> 
              :
              <FaUserAstronaut/>}
            </div>
            <span onClick={() => navigate(`/profile/${comment.author}`)}>{comment.author}</span>
          </div>
          <p className="comment-text">{comment.content}</p>
          <div className="reply-error">
            {isError && <ErrorFallback error={error}/>}
          </div>
          {replyOn ?
          <div className="comment-input"> 
            <button className="comment-cancel-button" onClick={handleCancel}>X</button>
            <textarea
              className={`comment-textarea ${isError ? 'input-error' : ''}`}
              value={commentText} 
              onChange={(e) => setCommentText(e.target.value)} 
              placeholder='...'
            />
            <button 
              className="comment-button" 
              onClick={handleReply}>
                Reply
            </button>
          </div>
          :
          <button onClick={() => setReplyOn(!replyOn)} className="comment-button">Reply</button>
          }
        </div>
        {isRepliesError && <ErrorFallback error={repliesError.message}/>}
        {isRepliesLoading ? 
        <Loading/> :
        replies?.length > 0 &&
        <div className="replies-list">
        {replies?.map((reply, index) => 
         <CommentCard key={index} comment={reply} isReply={true}/>
        )}
        </div>
        }
      </div>
    </div>
  )
}

export default CommentCard
