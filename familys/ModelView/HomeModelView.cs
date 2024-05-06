namespace familys.ModelView
{
    public class HomeModelView
    {
    }
    public class addHome
    {
        public int AccId { get; set; }
        public string Avatar { get; set; }
        public string Title { get; set; }
        public int Like { get; set; }
        public int Share { get; set; }
    }
    public class AccId
    {
        public int accId { get; set; }
    }
    public class likePost
    {
        public int accId { get; set; }
        public int homeId { get; set; }
    }
    public class addComment
    {
        public int accId { get; set; }
        public int homeId { get; set; }
        public string comment { get; set; }
    }
}
