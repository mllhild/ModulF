using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulF.Tage._04
{
    public class Bed
    {
        public string id { get; set; }
        public string name { get; set; }
        public int capacity { get; set; }
        public BedType type { get; set; } = BedType.Single;
        //public Room room { get; set; }
        public List<string> occupants { get; set; } = new List<string>();
        public Bed(Room _room, BedType bedType)
        {
            id = Guid.NewGuid().ToString("N");
            //room = _room;
            _room.beds.Add(this.id, this);
            name = "Bed " + _room.beds.Count.ToString();
            type = bedType;
            switch (type)
            {
                case BedType.Single: capacity = 1; break;
                case BedType.Double: capacity = 2; break;
                case BedType.Bunk: capacity = 3; break;
                default: type = BedType.Single; capacity = 1; break;
            }
        }
        internal bool IsOccupied()
        {
            switch (type)
            {
                case BedType.Single:
                    if (occupants.Count > 0) return true;
                    break;
                case BedType.Double:
                    if (occupants.Count > 0) return true;
                    break;
                case BedType.Bunk:
                    if (occupants.Count >= capacity) return true;
                    break;
            }
            return false;
        }
        internal int FreeCapacity()
        {
            int freeCapacity = 0;
            switch (type)
            {
                case BedType.Single:
                    freeCapacity = capacity - occupants.Count;
                    break;
                case BedType.Double:
                    if (occupants.Count > 0) return 0;
                    break;
                case BedType.Bunk:
                    freeCapacity = capacity - occupants.Count;
                    break;
            }
            return freeCapacity;
        }
        internal void AddGuest(Guest guest)
        {
            occupants.Add(guest.id);
            guest.bedId = id;
        }
        internal void RemoveGuest(string guestID) { occupants.Remove(guestID); }
    }
    public enum BedType
    {
        Single = 1,
        Double = 2,
        Bunk = 3
    }
}
