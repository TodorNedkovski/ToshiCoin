namespace ToshiCoin
{
    using Blockchain;   
    using System;
    using Newtonsoft.Json;
    using System.Diagnostics;
    using Core;

    public static class Program
    {
        public static void Main(string[] args)
        {
            new Core().Start(args);
        }
    }
}