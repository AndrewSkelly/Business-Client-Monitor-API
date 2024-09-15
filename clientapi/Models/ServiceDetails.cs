namespace clientapi.Models
{
    public class ServiceDetails
    {
        public int servicehistoryid { get; set; }
        public int clientid { get; set; }
        public int staffid { get; set; }
        public string? servicetype { get; set; }
        public DateTime servicedate { get; set; }
    }
}
