namespace ToshiCoin
{
    using Blockchain;   
    using System;
    using Newtonsoft.Json;
    using System.Diagnostics;
    using Core;

    public static class Program
    {
        public static Blockchain ToshiCoin = new Blockchain();
        
        public static void Main()
        {
            new Core().Start();
        }
    }
}