using System.ComponentModel.DataAnnotations;

namespace RoomCharges.Data
{
    public class FolioCharge
    {
        public int ReservationID { get; set; }
        public int BusinessID { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        public bool Tip { get; set; }
        [Required]
        public string EmployeeID { get; set; }
    }
}
