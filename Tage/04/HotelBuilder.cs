using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulF.Tage._04
{
    internal static class HotelBuilder
    {
        static Random random = new Random();
        internal static Hotel BuildHotel() {
            Hotel hotel = new Hotel("HotelName");
            for (int i = 0; i < 2; i++)
                AddFloor(hotel);
            return hotel;
        }
        internal static void AddFloor(Hotel hotel) {
            Floor floor = new Floor(hotel);
            hotel.AddFloor(floor);
            for (int i = 0; i < 5; i++)
                AddRoom(hotel, floor);
        }
        internal static void AddRoom(Hotel hotel, Floor floor) {
            RoomType roomType = (RoomType)random.Next(1,4);
            Room room = new Room(floor, roomType, new BedType[0]);
            int bedCount = new Random().Next(1, 5);
            for (int i = 0;i < bedCount;i++)
                AddBed(hotel, floor, room);

        }
        internal static void AddBed(Hotel hotel, Floor floor, Room room) {
            BedType bedType = (BedType)random.Next(1,4);
            Bed bed  = new Bed(room, bedType);
            hotel.beds.Add(bed.id, bed);
        }
    }
}
