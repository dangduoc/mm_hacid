
namespace Application.Wrappers
{
  

    public class PagedResponse<T> : Response
    {
        public T Data { get; set; }
        public int TotalCount { get; set; }

        public int DataCount { get; set; }

        public PagedResponse()
        {

        }

        public PagedResponse(bool status, string message = null, T data = default(T), int dataCount = 0, int totalCount = 0)
         : base(status, message)
        {
            Data = data;
            TotalCount = totalCount;
            DataCount = dataCount;
        }
    }

 
}
