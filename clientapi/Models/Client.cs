namespace clientapi.Models
{
    public class Client
    {
        public int clientid { get; set; }
        public string ?name { get; set; }
        public string ?email { get; set; }
        public string ?phone { get; set; }
        public string ?tags { get; set; }
        public string ?notes { get; set; }
    }
}
