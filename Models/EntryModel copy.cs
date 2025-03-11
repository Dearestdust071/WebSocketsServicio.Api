namespace WebSocketsServicio.Api.Models
{

    public class EntryModel
    {

        public required int Id {get; set;}
        public required string Color {get;set;}
        public required string User {get;set;}


        public EntryModel(int id, string color, string user)
        {
            Id = id;
            Color = color;
            User = user;
        }
    }

}