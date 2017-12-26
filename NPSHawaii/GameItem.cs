namespace NPSHawaii
{
    public sealed class GameItem
    {
        public string TitleID { get; set; }
        public string Region { get; set; }
        public string TitleName { get; set; }
        public string PKGLink { get; set; }
        public string RAP { get; set; }
        public string ContentID { get; set; }
        public string LastModified { get; set; }
        public string FileSize { get; set; }
        public string SHA256 { get; set; }
        public string DBSource { get; set; }
        public string Platform { get; set; }

        public GameItem(string ID, string RegionCode, string Title, string Url)
        {
            TitleID = ID;
            Region = RegionCode;
            TitleName = Title;
            PKGLink = Url;
        }
    }
}
