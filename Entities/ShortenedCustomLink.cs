namespace APIShortLink.Entities
{
    public class ShortenedCustomLink
    {
        //construtor
        public ShortenedCustomLink(string title, string destinationLink)
        {
            var code = title.Split(" ")[0];
            Title = title;
            DestinationLink = destinationLink;
            ShortnedLink = $"localhost:3000/{code}";
            Code = code;
            CreatedAt = DateTime.Now.ToShortDateString();
        }

        public string Title { get; set; }
        public int Id { get; set; }
        public string ShortnedLink { get; set; }
        public string DestinationLink { get; set; }
        public string Code { get; set; }
        public string CreatedAt { get; set; }

        //método que manipula o Update
        public void Update(string title, string destinationLink)
        {
            Title = title;
            DestinationLink = destinationLink;
        }
    }
}