using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulF.Tage._04
{
    public class Floor
    {
        public string id { get; set; }
        public string name { get; set; }
        //internal Hotel hotel { get; set; }
        public Dictionary<string, Room> rooms { get; set; } = new Dictionary<string, Room>();
        
        internal Floor(Hotel _hotel)
        {
            id = Guid.NewGuid().ToString("N");
        //    hotel = _hotel;
            name = "Floor " + _hotel.floors.Count.ToString();
        }
        internal void AddRoom(Room room) => rooms.Add(room.id, room);

    }
}
