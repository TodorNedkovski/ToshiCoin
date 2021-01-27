using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace P2PServer
{
    public class P2PServer : WebSocketBehavior
    {
        bool chainSynched = false;  
        WebSocketServer wss = null;  
  
        public void Start(int port)  
        {
            wss = new WebSocketServer($"ws://127.0.0.1:{port}");  
            wss.AddWebSocketService<P2PServer>("/Blockchain");  
            wss.Start();  
            Console.WriteLine($"Started server at ws://127.0.0.1:{port}");  
        }
        
        
    }
}