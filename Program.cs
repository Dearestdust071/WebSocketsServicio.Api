using Fleck;
using Newtonsoft.Json;
using WebSocketsServicio.Api.Models;
namespace WebSocketsServicio.Api
{
    internal class Program
    {
        // private static readonly Dictionary<string, List<IWebSocketConnection>> rooms = new Dictionary<string, List<IWebSocketConnection>>();
        // private static readonly List<IWebSocketConnection> clients = new List<IWebSocketConnection>();
        
        private static readonly WebSocketServer server = new WebSocketServer("ws://192.168.101.7:9001");

        private static readonly List<IWebSocketConnection> users = new List<IWebSocketConnection>();


        // Identificador valor
        static void Main(string[] args)
        {
            server.Start(connection =>
            {
                connection.OnOpen = () =>
                {
                    users.Add(connection);
                    
                    Console.WriteLine($"Join: {connection.ConnectionInfo.Id}");
                };

                connection.OnClose = () =>
                {
                    users.Remove(connection);
                    Console.WriteLine($"Out: {connection.ConnectionInfo.Id}");
                };
                connection.OnMessage = (string mensaje_del_cliente) =>
                {

                  EntryModel entryModel = JsonConvert.DeserializeObject<EntryModel>(mensaje_del_cliente);
                    foreach (IWebSocketConnection user in users)
                    {
                        if (connection != user)
                        {
                            var message = new 
                            {
                                Id = entryModel.Id,
                                Color = entryModel.Color
                            };
                            string jsonMessage = JsonConvert.SerializeObject(message);
                            user.Send(jsonMessage);
                        }
                    }

                };
            });
            Console.ReadLine();

        }

        // private static void Handler(EntryModel entry, IWebSocketConnection connection)
        // {

        //     switch (entry.Action_Type)
        //     {
        //         case ActionType.join:
        //             Join(entry.Value.Name, connection);
        //             break;
        //         case ActionType.leave:
        //             Leave(entry.Value.Name, connection);
        //             break;
        //         case ActionType.message:
        //             Message(entry.Value, connection);
        //             break;
        //         case ActionType.createRoom:
        //             CreateRoom(entry.Value.Name);
        //             break;
        //         default:
        //             break;

        //     }
        // }

        // private static void CreateRoom(string nameRoom)
        // {
        //     if (!rooms.ContainsKey(nameRoom))
        //     {
        //         rooms[nameRoom] = new List<IWebSocketConnection>();
        //         Console.WriteLine($"Se creo una nueva sala con el nombre: {nameRoom}");
        //     }
        //     else
        //     {
        //         Console.WriteLine($"La sala {nameRoom} ya existe");
        //     }
        // }

        // private static void Join(string nameRoom, IWebSocketConnection connection)
        // {
        //     if (!rooms.ContainsKey(nameRoom))
        //     {
        //         Console.WriteLine($"La sala {nameRoom} no existe por favor creala antes de entrar a ella");
        //         return;
        //         // rooms[nameRoom] = new List<IWebSocketConnection>();
        //         // Console.WriteLine($"Se creo una nueva sala con el nombre: {nameRoom}");
        //     }
        //     if (!rooms[nameRoom].Contains(connection))
        //     {


        //         // EN este apartado se le manda mensaje a LOS DEMAS en la sala
        //         var message = new
        //         {
        //             Action_Type = "join",
        //             Value = new
        //             {
        //                 Name = nameRoom,
        //                 Msg = $"El usuario {connection.ConnectionInfo.Id} se ha conectado a la sala.",
        //             }
        //         };
        //         string jsonMessage = JsonConvert.SerializeObject(message);





        //         string listaUsuariosEnSala = "Los siguientes ususarios se encuentran en la sala: \n";
        //         foreach (var item in rooms[nameRoom])
        //         {   
        //             listaUsuariosEnSala = listaUsuariosEnSala  + item.ConnectionInfo.Id.ToString() + "\n";
        //         }


        //         var message2 = new
        //         {
        //             Action_Type = "join",
        //             Value = new
        //             {
        //                 Name = nameRoom,
        //                 Msg = $"{listaUsuariosEnSala}",
        //             }
        //         };
        //         string jsonMessage2 = JsonConvert.SerializeObject(message2);

                

        //         foreach (var user in rooms[nameRoom])
        //         {
        //             user.Send(jsonMessage);

        //         }
        //         connection.Send(jsonMessage2);


        //         // foreach (IWebSocketConnection cliente in rooms[nameRoom])
        //         // {
                    
        //         // }







        //         // rooms[nameRoom].Add(connection);
        //         rooms[nameRoom].Add(connection);
        //         SendRooms();
        //         Console.WriteLine($"El usuario: {connection.ConnectionInfo.Id} Se conecto a: {nameRoom}");
        //     }
        // }

        // private static void Leave(string nameRoom, IWebSocketConnection connection)
        // {

        //     if (!rooms.ContainsKey(nameRoom))
        //     {
        //         Console.WriteLine($"La sala {nameRoom} no existe por favor cree dicha sala");
        //     }

        //     if (!rooms[nameRoom].Contains(connection))
        //     {
        //         Console.WriteLine($"No te encuentras conectado a la sala {nameRoom} por favor conectate para poder hablar en esta sala");
        //     }

        //     if (rooms[nameRoom].Contains(connection))
        //     {
        //         rooms[nameRoom].Remove(connection);
        //         Console.WriteLine($"El usuario: {connection.ConnectionInfo.Id} salio de la sala: {nameRoom}");
        //     }

        // }

        // private static void Message(Value value, IWebSocketConnection connection)
        // {

        //     if (!rooms.ContainsKey(value.Name))
        //     {
        //         Console.WriteLine($"La sala {value.Name} no existe por favor cree dicha sala");
        //         return;
        //     }
        //     if (!rooms[value.Name].Contains(connection))
        //     {

        //         Console.WriteLine($"No te encuentras conectado a la sala {value.Name} por favor conectate para poder hablar en esta sala");
        //         return;
        //     }

        //     foreach (IWebSocketConnection cliente in rooms[value.Name])
        //     {
        //         if (cliente != connection)
        //         {


        //             var message = new
        //             {
        //                 Action_Type = "message",
        //                 Value = new
        //                 {
        //                     Name = value.Name,
        //                     Msg = value.Msg
        //                 }
        //             };

        //             string jsonMessage = JsonConvert.SerializeObject(message);
        //             cliente.Send(jsonMessage); // Enviar la cadena JSON



        //         }

        //     }
        // }

        // private static void SendRoomsOnOpen(IWebSocketConnection connection)
        // {
        //     var roomList = rooms.Select(room => new
        //     {
        //         RoomName = room.Key,
        //         User = room.Value.Count
        //     }).ToArray();
        //     var message = new
        //     {
        //         Action_Type = "rooms",
        //         Value = new
        //         {
        //             Name = "",
        //             Msg = "",
        //             Rooms = roomList

        //         }


        //     };
        //     string jsonMessage = JsonConvert.SerializeObject(message);
        //     connection.Send(jsonMessage);
        // }

        // private static void SendRooms()
        // {
        //     var roomList = rooms.Select(room => new
        //     {
        //         RoomName = room.Key,
        //         User = room.Value.Count
        //     }).ToArray();
        //     var message = new
        //     {
        //         Action_Type = "rooms",
        //         Value = new
        //         {
        //             Name = "",
        //             Msg = "",
        //             Rooms = roomList

        //         }


        //     };

        //     string jsonMessage = JsonConvert.SerializeObject(message);
        //     foreach (var user in users)
        //     {
        //         user.Send(jsonMessage);
        //     }
        //     // connection.Send(jsonMessage);
        // }









    }
}