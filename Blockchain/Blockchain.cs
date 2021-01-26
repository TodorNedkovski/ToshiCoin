using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace Blockchain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Blockchain
    {
        private IList<Block> chain;

        private const int reward = 1;

        public Blockchain()
        {
            this.InitializeChain();
            this.AddGenesisBlock();
        }
        
        public Dictionary<string, int> Balances = new Dictionary<string, int>();
        
        public List<Transaction> PendingTransactions = new List<Transaction>();

        public List<Block> Chain => (List<Block>)chain;

        public int Difficulty { get; set; }

        public void InitializeChain()
        {
            chain = new List<Block>();
            this.Difficulty = 2;
        }

        public void ProcessPendingTransactions(string minerAddress)  
        {
            Block block = new Block(GetLatestBlock().Hash, PendingTransactions);  
            AddBlock(block); 
            
            AddBalances(PendingTransactions);
  
            PendingTransactions = new List<Transaction>();  
            CreateTransaction(new Transaction(null, minerAddress, reward));  
        }  

        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }

        public Block CreateGenesisBlock()
        {
            return new Block(null, new List<Transaction>());
        }

        public void AddGenesisBlock()
        {
            chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return chain.Last();
        }

        public bool IsValid()
        {
            for (int i = 1; i < this.chain.Count; i++)
            {
                Block currentBlock = this.chain[i];
                Block previousBlock = this.chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Hash = block.CalculateHash();
            block.Mine(this.Difficulty);
            chain.Add(block);
        }

        private void AddBalances(List<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                string fromAddress = transaction.FromAddress;
                string toAddress = transaction.ToAddress;
                int amount = transaction.Amount;
                
                if (!string.IsNullOrEmpty(fromAddress) && !this.Balances.ContainsKey(fromAddress))
                {
                    this.Balances.Add(fromAddress, 0);
                }
                
                if (!this.Balances.ContainsKey(toAddress))
                {
                    this.Balances.Add(toAddress, 0);
                }

                if (this.Balances[fromAddress] - amount < 0) continue;

                if (!string.IsNullOrEmpty(fromAddress))
                {
                    this.Balances[fromAddress] -= amount;
                }
                
                this.Balances[toAddress] += amount;
            }
        }

        public int GetBalance(string address)
        {
            return this.Balances[address];
        }
    }
}