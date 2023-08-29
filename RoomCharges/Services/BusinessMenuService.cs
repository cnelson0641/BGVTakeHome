using System;

namespace RoomCharges.Services
{
    public class BusinessMenuService
    {
        public int SelectedBusinessID { get; set; }
        public event EventHandler<EventArgs> OnChanged;
        public void NotifyChanged()
        {
            OnChanged?.Invoke(this, null);
        }
    }
}
