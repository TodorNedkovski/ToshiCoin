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
            //Blockchain toshiCoin = new Blockchain();
            //toshiCoin.AddBlock(new Block(null, "{sender:Gosho,receiver:Tosho,amount:10}"));
            //toshiCoin.AddBlock(new Block(null, "{sender:Valeri,receiver:Tosho,amount:5}"));
            //toshiCoin.AddBlock(new Block(null, "{sender:Pesho,receiver:Tosho,amount:5}"));

            //Console.WriteLine(toshiCoin.IsValid());

            //toshiCoin.GetLatestBlock().Data = "{sender:Pesho,receiver:Tosho,amount:100000}";

            //Console.WriteLine(toshiCoin.IsValid());

            //toshiCoin.Chain[2].PreviousHash = toshiCoin.Chain[1].Hash;
            //toshiCoin.Chain[2].Hash = toshiCoin.Chain[2].CalculateHash();
            //toshiCoin.Chain[3].PreviousHash = toshiCoin.Chain[2].Hash;
            //toshiCoin.Chain[3].Hash = toshiCoin.Chain[3].CalculateHash();

            //Console.WriteLine(toshiCoin.IsValid());

            //Console.WriteLine(JsonConvert.SerializeObject(toshiCoin, Formatting.Indented));

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            Blockchain toshiCoin = new Blockchain();
            toshiCoin.PendingTransactions.Add(new Transaction("Gosho", "Tosho", 10));
            toshiCoin.AddBlock(new Block(null, "{sender:Gosho,receiver:Tosho,amount:10}"));
            toshiCoin.AddBlock(new Block(null, "{sender:Valeri,receiver:Tosho,amount:5}"));
            toshiCoin.AddBlock(new Block(null, "{sender:Pesho,receiver:Tosho,amount:5}"));

            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
