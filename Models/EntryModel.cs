namespace WebSocketsServicio.Api.Models
{

    public class EntryModel
    {

        public required int Id {get; set;}
        public required string Color {get;set;}


        public EntryModel(int id, string color)
        {
            Id = id;
            Color = color;
        }
    }

}