using static MangoWeb.Utility.SD;

namespace MangoWeb.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; }
        public string URI { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }

    }
}
