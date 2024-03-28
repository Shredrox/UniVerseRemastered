import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { addNews, getNews } from "../../services/newsService";

const useNewsListData = () =>{
  const queryClient = useQueryClient();

  const {data: newsData, 
    isLoading: isNewsDataLoading, 
    isError: isNewsDataError, 
    error: newsDataError
  } = useQuery({ 
    queryKey: ["news"],
    queryFn: () => getNews(),
  });

  const news = newsData?.sort((a : any, b : any) => {
    if (a.pinned !== b.pinned) {
      return a.pinned ? -1 : 1;
    } else {
      const [aday, amonth, ayearAndTime] = a.date.split('-');
      const [ayear, atime] = ayearAndTime.split(' ');
      const [ahour, aminute] = atime.split(':');
      const dateA = new Date(ayear, amonth - 1, aday, ahour, aminute);

      const [bday, bmonth, byearAndTime] = b.date.split('-');
      const [byear, btime] = byearAndTime.split(' ');
      const [bhour, bminute] = btime.split(':');
      const dateB = new Date(byear, bmonth - 1, bday, bhour, bminute);

      return dateB.getTime() - dateA.getTime();
    }
  });

  const {mutateAsync: addNewsMutation} = useMutation({
    mutationFn: addNews,
    onSuccess: () =>{
      queryClient.invalidateQueries({
        queryKey: ["news"]
      });
    },
  });

  return {
    news, 
    isNewsDataLoading, 
    isNewsDataError, 
    newsDataError,
    addNewsMutation
  }
}

export default useNewsListData
