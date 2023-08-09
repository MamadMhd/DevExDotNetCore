using Newtonsoft.Json;

namespace DevExtremeAspNetCoreApp1.Models
{
    public class Book
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Include, PropertyName = "Id")]
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Title")]
        public string Title { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Genre")]
        public string Genre { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "PublishDate")]
        public DateTime PublishDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Price")]
        public long Price { get; set; }
    }
}
