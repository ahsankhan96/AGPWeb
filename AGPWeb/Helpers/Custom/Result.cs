namespace AGPWeb.Helpers.Custom
{
    public struct RequestData<T> where T : class
    {

        public List<T> data { get; set; }
    }

    public struct Result<T> where T : class
    {
        public ResultStatus status { get; set; }
        public string message { get; set; }
        public IEnumerable<T> data { get; set; }
    }
    public struct Result
    {
        public ResultStatus status { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
    }
    public enum ResultStatus
    {

        Unauthorized = 0,
        Success = 1,
        Error = 2,
        NotFound = 3,
        Warning = 4,
        InProcess = 5
    }
}
