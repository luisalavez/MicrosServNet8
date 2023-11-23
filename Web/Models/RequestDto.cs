using static Web.Utility.SD;

namespace Web.Models
{
    public class RequestDto
    {
        public ApyType ApiType { get; set; } = ApyType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
