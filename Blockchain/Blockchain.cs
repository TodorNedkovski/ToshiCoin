namespace Blockchain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Blockchain
    {
        private IList<Block> chain;

        public Blockchain()
        {
            this.InitializeChain();
            this.AddGenesisBlock();
        }


        public IList<Transaction> PendingTransactions = new List<Transaction>();

        public List<Block> Chain => (List<Block>)chain;

        public int Difficulty { get; set; }

        public void InitializeChain()
        {
            chain = new List<Block>();
            this.Difficulty = 2;
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
    }
}