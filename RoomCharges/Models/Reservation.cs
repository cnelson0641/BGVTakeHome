using RoomCharges.Models;

namespace RoomCharges.Data
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public string ReservationNumber { get; set; }
        public Site Site { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
        public string RoomNumber { get; set; }
        public string LastFirstName => $"{GuestLastName}, {GuestFirstName}";
    }
    public class SearchReservation
    {
        public Site DefaultResort { get; set; }
        public bool AllResorts { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoomNumber { get; set; }
    }
}
