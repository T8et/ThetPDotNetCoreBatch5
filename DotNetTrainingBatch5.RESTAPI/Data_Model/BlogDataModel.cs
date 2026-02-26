namespace DotNetTrainingBatch5.RESTAPI.Data_Model
{
    public class BlogDataModel
    {
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
        public int DeleteFlag { get; set; }
    }
}
