using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulF.Tage._04
{
    public class Hotel
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime currentDate { get; set; }
        public Dictionary<string, Floor> floors { get; set; } = new Dictionary<string, Floor>();
        public Dictionary<string, Bed> beds { get; set; } = new Dictionary<string, Bed>();
        public Dictionary<string, Guest> guests { get; set; } = new Dictionary<string, Guest>();

        public Hotel(string _name)
        {
            id = Guid.NewGuid().ToString("N");
            name = _name;
            currentDate = DateTime.Now;
        }
        public void AddFloor(Floor floor) => floors.Add(floor.id, floor);

        
    }
}
