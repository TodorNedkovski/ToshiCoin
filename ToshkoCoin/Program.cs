namespace ToshiCoin
{
    using Blockchain;
    using System;
    using Newtonsoft.Json;

    public class Program
    {
        public static void Main()
        {
            Blockchain toshkoCoin = new Blockchain();
            toshkoCoin.AddBlock(new Block(null, "{sender:Gosho,receiver:Tosho,amount:10}"));
            toshkoCoin.AddBlock(new Block(null, "{sender:Valeri,receiver:Tosho,amount:5}"));
            toshkoCoin.AddBlock(new Block(null, "{sender:Pesho,receiver:Tosho,amount:5}"));

            Console.WriteLine(JsonConvert.SerializeObject(toshkoCoin, Formatting.Indented));
        }
    }
}
