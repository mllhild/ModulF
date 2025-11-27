using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModulF.Tage._04
{
    public class Room
    {
        public string id { get; set; }
        public string name { get; set; }
        //public Floor floor { get; set; }
        public RoomType roomType { get; set; }
        public Dictionary<string, Bed> beds { get; set; } = new Dictionary<string, Bed>();
        internal Room(Floor _floor, RoomType _roomType, BedType[] bedTypes) {
            id = Guid.NewGuid().ToString("N");
            //floor = _floor;
            _floor.AddRoom(this);
            roomType = _roomType;
            foreach (BedType bedType in bedTypes)
                AddBed(bedType);
            name = "Room " + _floor.rooms.Count.ToString();
        }
        internal void AddBed(BedType bedType) => new Bed(this, bedType);
        internal int FreeCapacity()
        {
            int freeCapacity = 0;
            foreach (var bed in beds.Values)
                freeCapacity += bed.FreeCapacity();
            return freeCapacity;
        }
        internal int TotalCapacity()
        {
            int capacity = 0;
            foreach (var bed in beds.Values)
                capacity += bed.capacity;
            return capacity;
        }
        
    }

    public enum RoomType
    {
        economy = 1,
        buissness = 2,
        executive = 3
    }
}
