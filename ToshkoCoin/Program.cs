namespace ToshiCoin
{
    using Blockchain;   
    using System;
    using Newtonsoft.Json;
    using System.Diagnostics;

    public class Program
    {
        public static void Main()
        {
            var coin = new Blockchain();
            
            coin.CreateTransaction(new Transaction("Pesho", "Tosho", 20));
            coin.CreateTransaction(new Transaction("Pesho", "Tosho", 20));
            coin.ProcessPendingTransactions("TOsho");
            
            coin.CreateTransaction(new Transaction("Pesho", "Tosho", 20));
            coin.CreateTransaction(new Transaction("Pesho", "Tosho", 20));
            coin.ProcessPendingTransactions("TOsho");
            
            Console.WriteLine(coin.GetBalance("Tosho"));
            Console.WriteLine(coin.GetBalance("TOsho"));
        }
    }
}