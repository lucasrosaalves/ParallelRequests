namespace ParallelRequests
{
    public class RequestResult
    {
        public double TotalSeconds { get; set; }
        public int Requests { get; set; }
        public int SuccessfulRequests { get; set; }

        public override string ToString()
        {
            return $"{Requests} simultaneous requests - {TotalSeconds}s elapsed - with {SuccessfulRequests} successfull requests";
        }
    }
}
