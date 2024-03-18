import { useQuery } from "@tanstack/react-query";
import { getNewsImage } from "../../services/newsService";

const useNewsImage = (newsId : number) =>{
  const {data: newsImage
	} = useQuery({
    queryKey: ["newsImage", newsId],
    queryFn: () => getNewsImage(newsId),
  });

  return {
    newsImage
  }
}

export default useNewsImage
