namespace Blockchain
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using Newtonsoft.Json;

    public class Block
    {
        public int Index { get; set; }

        public DateTime TimeStamp { get; set; }

        public string PreviousHash { get; set; }

        public string Hash { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public int Nonce { get; set; }

        public Block(string hash, List<Transaction> transactions)
        {
            this.Index = 0;
            this.TimeStamp = DateTime.UtcNow;
            this.PreviousHash = this.PreviousHash;
            this.Hash = hash;
            this.Transactions = transactions;
            this.Nonce = 0;
        }

        public string CalculateHash()
        {
            var sha256 = SHA256.Create();

            var transactions = JsonConvert.SerializeObject(this.Transactions);

            byte[] bytes = Encoding.ASCII.GetBytes($"{this.TimeStamp} - {transactions} - {this.Index} - {this.Nonce}");
            byte[] outputBytes = sha256.ComputeHash(bytes);

            return Encoding.ASCII.GetString(outputBytes);
        }

        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }
}