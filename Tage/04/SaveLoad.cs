using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ModulF.Tage._04
{
    internal static class SaveLoad
    {
        static string path = AppContext.BaseDirectory;//@"../../SaveFiles";
        static string filename = "HotelName.json";
        internal static bool Save(Hotel hotel)
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                WriteIndented = true
            };

            string saveData = JsonSerializer.Serialize(hotel, options);
            
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            if (saveData == null)
                return false;
            
            string savePath = Path.Combine(path, filename);
            File.WriteAllText(savePath, saveData);
            Console.WriteLine(savePath);

            return true;
        }
        internal static bool Load(out Hotel hotel)
        {
            if (!Directory.Exists(path))
            {
                hotel = new Hotel("error");
                hotel = HotelBuilder.BuildHotel();
                Save(hotel);
                return false;
            }
            
            string savePath = Path.Combine(path, filename);
            if(!File.Exists(savePath))
            {
                hotel = new Hotel("error");
                hotel = HotelBuilder.BuildHotel();
                Save(hotel);
                return false;
            }

            Console.WriteLine(savePath);
            string saveData = File.ReadAllText(savePath);

            if (File.ReadAllLines(savePath).Length < 3)
            {
                hotel = new Hotel("error");
                hotel = HotelBuilder.BuildHotel();
                Save(hotel);
                return false;
            }

            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                WriteIndented = true
            };
            try
            {
                hotel = JsonSerializer.Deserialize<Hotel>(saveData, options);
            }
            catch (Exception ex)
            {
                hotel = HotelBuilder.BuildHotel();
                Save(hotel);
            }
            return true;
        }
    }
}
