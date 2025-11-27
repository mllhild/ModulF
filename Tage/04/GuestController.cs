using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModulF.Tage._04
{
    internal static class GuestController
    {

        internal static void ReceiveGuest(Hotel hotel)
        {
            Console.Clear();
            Console.WriteLine("Welcome Guest!");
            Console.WriteLine("\nPress a key to proceed....");
            Console.ReadKey();
            Guest guest = CreateGuest();

            

            ConsoleKey key;

            int selectedBedType = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Dear Guest, do you desire a spezific BED type?");
                foreach (var bedType in Enum.GetValues(typeof(BedType)))
                    Console.WriteLine($"{(int)bedType} - {bedType.ToString()}");
                Console.WriteLine($"0 - irrelevant");
                Console.WriteLine("Press the desired number for a type");

                var pressed = Console.ReadKey(intercept: true).Key;
                if (pressed >= ConsoleKey.D0 && pressed <= (ConsoleKey.D0 + Enum.GetValues(typeof(BedType)).Length))
                {
                    selectedBedType = (int)(pressed - ConsoleKey.D0);
                    break;
                }
                Console.WriteLine(selectedBedType.ToString());
                Console.WriteLine("Invalid input. Try again!");
                Thread.Sleep(1000);
            }

            int selectedRoomType = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Dear Guest, do you desire a spezific ROOM type?");
                foreach (var roomType in Enum.GetValues(typeof(RoomType)))
                    Console.WriteLine($"{(int)roomType} - {roomType.ToString()}");
                Console.WriteLine($"0 - irrelevant");
                Console.WriteLine("Press the desired number for a type");

                var pressed = Console.ReadKey(intercept: true).Key;
                if (pressed >= ConsoleKey.D0 && pressed <= (ConsoleKey.D0 + Enum.GetValues(typeof(RoomType)).Length))
                {
                    selectedRoomType = (int)(pressed - ConsoleKey.D0);
                    break;
                }
                Console.WriteLine(selectedRoomType.ToString());
                Console.WriteLine("Invalid input. Try again!");
                Thread.Sleep(1000);
            }
            Console.Clear();
            Console.WriteLine($"{selectedBedType} {selectedRoomType}");
            if (selectedBedType == 0 && selectedRoomType == 0) CheckIn(hotel, guest); 
            if (selectedBedType != 0 && selectedRoomType == 0) CheckIn(hotel, guest, (BedType)selectedBedType);
            if (selectedBedType == 0 && selectedRoomType != 0) CheckIn(hotel, guest, (RoomType)selectedRoomType);
            if (selectedBedType != 0 && selectedRoomType != 0) CheckIn(hotel, guest, (RoomType)selectedRoomType, (BedType)selectedBedType);

        }

        internal static Guest CreateGuest()
        {
            string name = "";
            do
            {
                Console.Clear();
                Console.WriteLine("Write guest name: ");
                name = Console.ReadLine();
            } while (name.Length<1);

            string ageOfGuest = "";
            int age;
            do
            {
                Console.Clear();
                Console.WriteLine("Age of Guests: ");
                ageOfGuest = Console.ReadLine();
            } while (!int.TryParse(ageOfGuest, out age));

            string durationOfStay = "";
            int duration;
            do
            {
                Console.Clear();
                Console.WriteLine("Duration of Stay: ");
                durationOfStay = Console.ReadLine();
            } while (!int.TryParse(durationOfStay, out duration));

            Guest guest = new Guest(name, age, duration);
            return guest;
        }



        internal static void CheckIn(Hotel hotel,Guest guest) { 
            bool foundEmptyBed = false;
            foreach (var floor in hotel.floors.Values)
            {
                if (foundEmptyBed) break;
                foreach (var room in floor.rooms.Values)
                {
                    if (foundEmptyBed) break;
                    foreach (var bed in room.beds.Values)
                    {
                        if (foundEmptyBed) break;
                        if (!bed.IsOccupied())
                        {
                            foundEmptyBed = true;
                            bed.AddGuest(guest);
                        }
                    }
                }
            }
            if (foundEmptyBed)
                hotel.guests.Add(guest.id, guest);
            else
                NoEmptyPlace(guest);
        }
        internal static void CheckIn(Hotel hotel, Guest guest, BedType bedType) {
            bool foundEmptyBed = false;
            foreach (var floor in hotel.floors.Values)
            {
                if (foundEmptyBed) break;
                foreach (var room in floor.rooms.Values)
                {
                    if (foundEmptyBed) break;
                    foreach (var bed in room.beds.Values)
                    {
                        if (foundEmptyBed) break;
                        if (!bed.IsOccupied() && bed.type == bedType)
                        {
                            foundEmptyBed = true;
                            bed.AddGuest(guest);
                        }
                    }
                }
            }
            if (foundEmptyBed)
                hotel.guests.Add(guest.id, guest);
            else
                NoEmptyPlace(guest);
        }
        internal static void CheckIn(Hotel hotel, Guest guest, RoomType roomType) {
            bool foundEmptyBed = false;
            foreach (var floor in hotel.floors.Values)
            {
                if (foundEmptyBed) break;
                foreach (var room in floor.rooms.Values)
                {
                    if (foundEmptyBed) break;
                    if (room.roomType != roomType) continue;
                    foreach (var bed in room.beds.Values)
                    {
                        if (foundEmptyBed) break;
                        if (!bed.IsOccupied())
                        {
                            foundEmptyBed = true;
                            bed.AddGuest(guest);
                        }
                    }
                }
            }
            if (foundEmptyBed)
                hotel.guests.Add(guest.id, guest);
            else
                NoEmptyPlace(guest);
        }
        internal static void CheckIn(Hotel hotel, Guest guest, RoomType roomType, BedType bedType) {
            bool foundEmptyBed = false;
            foreach (var floor in hotel.floors.Values)
            {
                if (foundEmptyBed) break;
                foreach (var room in floor.rooms.Values)
                {
                    if (foundEmptyBed) break;
                    if (room.roomType != roomType) continue;
                    foreach (var bed in room.beds.Values)
                    {
                        if (foundEmptyBed) break;
                        if (!bed.IsOccupied() && bed.type == bedType)
                        {
                            foundEmptyBed = true;
                            bed.AddGuest(guest);
                        }
                    }
                }
            }
            if (foundEmptyBed)
                hotel.guests.Add(guest.id, guest);
            else
                NoEmptyPlace(guest);
        }
        internal static void CheckOut(Hotel hotel, Guest guest) {
            hotel.beds[guest.bedId].RemoveGuest(guest.id);
            hotel.guests.Remove(guest.id);
        }
        internal static void NoEmptyPlace(Guest guest) {
            Console.WriteLine("Sorry, we dont have any Room that fits your requests.");
            Console.WriteLine("Press button to continue....");
            Console.ReadKey(intercept:true);
        }
    }
}

