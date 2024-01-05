namespace dotnet_endPoints.Dtos.HttpRequest
{
    public class HttpRequestResponse<T>
    {
        public T Content { get; set; }

        public bool Success => string.IsNullOrWhiteSpace(Message);

        public string Message { get; set; }
    }
}
