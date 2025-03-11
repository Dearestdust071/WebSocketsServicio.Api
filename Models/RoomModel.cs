using WebSocketsServicio.Api.Models;
namespace WebSocketsServicio.Api.Models{

public class RoomModel{

    public string RoomName {get;set;}
    public string Password {get;set;}

    public List<ConnectionModel> users {get;set;} 
    // = List<ConnectionModel>();
    public RoomModel(string roomName, string password){
        RoomName = roomName;
        Password = password;
    }
}

}