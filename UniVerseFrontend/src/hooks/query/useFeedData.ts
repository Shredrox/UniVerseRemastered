import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { addPost, getAllPosts, getFriendsPosts } from "../../services/postService";

const useFeedData = (user : string, userRole : string) => {
  const queryClient = useQueryClient();

  const fetchAndSortPosts = async () => {
    try {
      const data = userRole === "ADMIN" ? await getAllPosts() : await getFriendsPosts(user) ;
      const sortedPosts = data.slice().sort((a : any, b : any) => {
        const [adayA, amonthA, ayearA, atimeA] = a.timestamp.split(/[-\s:]/);
        const [adayB, amonthB, ayearB, atimeB] = b.timestamp.split(/[-\s:]/);
        
        const dateA = new Date(ayearA, amonthA - 1, adayA, atimeA[0], atimeA[1]);
        const dateB = new Date(ayearB, amonthB - 1, adayB, atimeB[0], atimeB[1]);
        
        return dateB.getTime() - dateA.getTime();
      });
      return sortedPosts;
    } catch (error) {
      throw new Error(error);
    }
  }

  const {data: posts, 
    isLoading: isPostsLoading, 
    isError: isPostsError, 
    error: postsError
  } = useQuery({ 
    queryKey: ["posts", user],
    queryFn: fetchAndSortPosts
  });

  const {mutateAsync: addPostMutation} = useMutation({
    mutationFn: addPost,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["posts"]
      });
    },
  });

  return {
    posts, 
    isPostsLoading, 
    isPostsError, 
    postsError,
    addPostMutation
  }
}

export default useFeedData
