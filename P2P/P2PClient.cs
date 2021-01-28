using System;
using System.Collections.Generic;
using Transaction = Blockchain.Transaction;
using Newtonsoft.Json;
using WebSocketSharp;

namespace P2PServer
{
    public class P2PClient
    {
        IDictionary<string, WebSocket> wsDict = new Dictionary<string, WebSocket>();
        
        public void Connect(string url)
        {
            if (!wsDict.ContainsKey(url))  
            {  
                WebSocket ws = new WebSocket(url);  
                ws.OnMessage += (sender, e) =>   
                {  
                    if (e.Data == "Hi Client")  
                    {
                        Console.WriteLine(e.Data);  
                    }
                    else  
                    {  
                        Blockchain.Blockchain newChain = JsonConvert.DeserializeObject<Blockchain.Blockchain>(e.Data);  
                        if (newChain.IsValid() && newChain.Chain.Count > global::Core.Core.ToshiCoin.Chain.Count)  
                        {  
                            List<Transaction> newTransactions = new List<Transaction>();  
                            newTransactions.AddRange(newChain.PendingTransactions);  
                            newTransactions.AddRange(global::Core.Core.ToshiCoin.PendingTransactions);  
  
                            newChain.PendingTransactions = newTransactions;  
                            global::Core.Core.ToshiCoin = newChain;  
                        }  
                    }  
                };  
                ws.Connect();  
                ws.Send("Hi Server");  
                ws.Send(JsonConvert.SerializeObject(global::Core.Core.ToshiCoin));  
                wsDict.Add(url, ws);  
            }  
        }
        
        public void Send(string url, string data)  
        {  
            foreach (var item in wsDict)  
            {  
                if (item.Key == url)  
                {  
                    item.Value.Send(data);  
                }  
            }  
        }  
    }
}