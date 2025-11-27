using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulF.Tage._04
{
    public class Guest
    {
        public string id;
        public string name;
        public int age;

        public string bedId;
        public DateTime arrive;
        public DateTime leave;

        internal Guest(string name, int age, int duration)
        {
            id = Guid.NewGuid().ToString("N");
            this.name = name;
            this.age = age;
            arrive = ControllerMain.hotel.currentDate;
            leave = arrive.AddDays(duration);
        }
    }
}
