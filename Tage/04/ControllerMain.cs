using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModulF.Tage._04
{
    internal static class ControllerMain
    {
        internal static Hotel hotel;


        internal static void Menu()
        {
            StartHotel();
            string message = "";
            bool play = true;
            while (play)
            {
                Console.Clear();
                Console.WriteLine("Welcome to HotelManager");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("1 - Check In Guest");
                Console.WriteLine($"2 - Advance Day ({hotel.currentDate.ToString("yyyy MM dd")})");
                Console.WriteLine("");
                Console.WriteLine("4 - List Guests");
                Console.WriteLine("5 - List Stats");
                Console.WriteLine("6 - List Beds");
                Console.WriteLine("");
                Console.WriteLine("0 - Quit");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine(message);
                message = "";

                switch (Console.ReadKey(intercept: true).Key)
                {
                    case (ConsoleKey.D1):
                        GuestController.ReceiveGuest(hotel);
                        break;
                    case (ConsoleKey.D2):
                        message += AdvanceDay();
                        break;
                    case (ConsoleKey.D4):
                        ListGuests();
                        break;
                    case (ConsoleKey.D5):
                        break;
                    case (ConsoleKey.D6):
                        ListRooms();
                        break;
                    case (ConsoleKey.D0):
                        StopHotel();
                        play = false;
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        internal static void StartHotel() {
            if(!SaveLoad.Load(out hotel))
                hotel = HotelBuilder.BuildHotel();
            if(hotel.floors.Count == 0)
                hotel = HotelBuilder.BuildHotel();
        }
        internal static void StopHotel()
        {
            SaveLoad.Save(hotel);
        }

        internal static string AdvanceDay()
        {
            string message = "Checked Out: ";
            hotel.currentDate = hotel.currentDate.AddDays(1);
            List<Guest> guestsToCheckOut = new List<Guest>();
            foreach (var guest in hotel.guests.Values)
                if (hotel.currentDate > guest.leave)
                    guestsToCheckOut.Add(guest);
            if (guestsToCheckOut.Count == 0)
                return "";
            foreach (var guest in guestsToCheckOut)
            { 
                message += "\n" + guest.name; 
                GuestController.CheckOut(hotel, guest);
            }
            return message;
        }
        internal static void ListGuests()
        {
            Console.Clear();
            hotel.guests.Values.ToList().ForEach(guests => Console.WriteLine(guests.name.ToString()));
            Console.WriteLine("Press any Key to Return to Menu");
            Console.ReadKey(intercept: true);
        }
        static void ListRooms()
        {

            Console.Clear();
            Console.WriteLine($"Floor Count: {hotel.floors.Count}");
            foreach (var floor in hotel.floors.Values)
            {
                Console.WriteLine($"  Room Count: {floor.rooms.Count}");
                foreach (var room in floor.rooms.Values)
                {
                    Console.WriteLine($"    Bed Count: {room.beds.Count}");
                    foreach (var bed in room.beds.Values)
                    { 
                        Console.WriteLine($"    {floor.name} - {room.name} {room.roomType.ToString()} - {bed.name} {bed.type.ToString()}"); 
                    }
                }
            }
            Console.WriteLine("Press any Key to Return to Menu");
            Console.ReadKey(intercept: true);
        }
    }
}
