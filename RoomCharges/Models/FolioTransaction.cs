using System;

namespace RoomCharges.Models
{
    public class FolioTransaction
    {
        public string TransactionDate { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public int FolioId { get; set; }
    }
}

