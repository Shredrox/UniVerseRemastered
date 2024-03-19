import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { addCommentReply, getCommentReplies } from "../../services/postService";
import useAuth from '../../hooks/auth/useAuth'
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Loading from '../../components/fallback/Loading'
import ErrorFallback from '../../components/fallback/ErrorFallback'
import useProfilePicture from "../../hooks/query/useProfilePicture";
import { FaUserAstronaut } from "react-icons/fa";

const Comment = ({comment, isReply}) => {
  const [commentText, setCommentText] = useState('');
  const [replyOn, setReplyOn] = useState(false);
  const queryClient = useQueryClient();

  const navigate = useNavigate();

  const { auth } = useAuth();

  const [isError, setIsError] = useState(false);
  const [error, setError] = useState('');

  const {data: replies, isLoading, isError: isQueryError, error: queryError} = useQuery({ 
    queryKey: ["commentReplies", comment.id],
    queryFn: () => getCommentReplies(comment.id),
  });

  const {mutateAsync: addReplyMutation} = useMutation({
    mutationFn: addCommentReply,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey:["commentReplies", comment.id]
      });
    },
  });

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
        {isQueryError && <ErrorFallback error={queryError.message}/>}
        {isLoading ? <Loading/> :
        replies?.length > 0 &&
        <div className="replies-list">
        {replies?.map((reply, index) => 
         <Comment key={index} comment={reply} isReply={true}/>
        )}
        </div>
        }
      </div>
    </div>
  )
}

export default Comment
