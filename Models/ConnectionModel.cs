using Fleck;

namespace WebSocketsServicio.Api.Models{


    public class ConnectionModel{
        public string Id { get; set; }
        public IWebSocketConnection Connection {get; private set;}

        public ConnectionModel(string id, IWebSocketConnection connection){
            Id = id;
            Connection = connection;
        }


    }
}