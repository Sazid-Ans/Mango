namespace MangoWeb.Models 
{
    //public class ResponseDto
    //{
    //    public object Result { get; set; }
    //    public bool isSuccess { get; set; } = true;
    //    public string? message { get; set; }
    //}

    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool isSuccess { get; set; } = true;
        public string? message { get; set; }
        public string? title { get; set; }
        public int? status { get; set; }
        public List<string>? ErrorMessages { get; set; }
    }
}
