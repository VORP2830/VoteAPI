namespace VoteAPI.Domain.Exceptions
{
    public class VoteAPIException : Exception
    {    
        public VoteAPIException(string message) : base(message) { }
        public static void When(bool hasError, string error)
        {
            if (hasError)
            {
                throw new VoteAPIException(error);
            }
        }   
    }
}