using RoomCharges.Models;
using System.Collections.Generic;
using System.Linq;

namespace RoomCharges.Data
{
    public class BusinessConfiguration
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Site Site { get; set; }
        public List<TransactionCode> TransactionCodes { get; set; }
        public List<User> Users { get; set; }

        public User ChargeUser(string pin)
        {
            return Users.Where(u => u.AuthPIN == pin).FirstOrDefault();
        }
        public bool ChargeAuthorized(string pin)
        {
            return ChargeUser(pin) != null;
        }
        public int TransactionCodeID(TransactionType type)
        {
            var transactionCode = TransactionCodes.Where(tc => tc.Type == type).FirstOrDefault();
            return transactionCode.ID;
        }
        public bool AllowTip => TransactionCodes.Where(tc => tc.Type == TransactionType.Tip).Any();
    }

    public class User
    {
        public int UserId { get; set; }
        public string AuthPIN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShortName => $"{FirstName[0]}{LastName}";
    }

    public class TransactionCode
    {
        public TransactionType Type { get; set; }
        public int ID { get; set; }
    }

    public enum TransactionType
    {
        Tip, Retail
    }
}
