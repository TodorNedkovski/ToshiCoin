using System;
using System.Collections.Generic;
using System.Transactions;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;
using Blockchain;
using ToshiCoin;
using Transaction = Blockchain.Transaction;

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
        
        protected override void OnMessage(MessageEventArgs e)  
        {  
            if (e.Data == "Hi Server")  
            {  
                Console.WriteLine(e.Data);  
                Send("Hi Client");  
            }  
            else  
            {  
                Blockchain.Blockchain newChain = JsonConvert.DeserializeObject<Blockchain.Blockchain>(e.Data);  
  
                if (newChain.IsValid() && newChain.Chain.Count > Program.ToshiCoin.Chain.Count)  
                {  
                    List<Transaction> newTransactions = new List<Transaction>();  
                    newTransactions.AddRange(newChain.PendingTransactions);  
                    newTransactions.AddRange(Program.ToshiCoin.PendingTransactions);  
  
                    newChain.PendingTransactions = newTransactions;  
                    Program.ToshiCoin = newChain;  
                }  
  
                if (!chainSynched)  
                {  
                    Send(JsonConvert.SerializeObject(Program.ToshiCoin));  
                    chainSynched = true;  
                }  
            }  
        }
    }
}