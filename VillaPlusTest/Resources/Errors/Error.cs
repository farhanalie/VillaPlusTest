namespace EcbForex.API.Resources.Errors
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ErrorDetail[] Details { get; set; }
    }
}