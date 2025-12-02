using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModulF.Tage
{
    internal class BattleShip
    {
        // map size 10 x 10

        // ships
        // 4 size 2
        // 4 size 3
        // 2 size 4
        // 1 size 5

        // if hit, player shoots again

        // sunk ships are announced

        int mapHeight = 10;
        int mapWidth = 15;

        Player player1 = new Player();
        Player player2 = new Player();


        internal void Start()
        {
            player1.name = "P1";
            player2.name = "P2";
            player1.PositionShips(mapHeight, mapWidth);
            player2.PositionShips(mapHeight, mapWidth);
            //Console.ReadKey();
            Thread.Sleep(500);
            TakeTurns();
            Console.ReadKey(intercept: true);
        }

        void TakeTurns()
        {
            bool bothPlayersHaveShips = true;
            bool playerTurn = true;
            int turnCounter = 0;
            while(bothPlayersHaveShips)
            {
                
                if (playerTurn)
                {
                    player1.TakeTurn(player2);
                    bothPlayersHaveShips = player2.UpdateShips();
                }
                else
                {
                    player2.TakeTurn(player1);
                    bothPlayersHaveShips = player1.UpdateShips();
                }
                
                Draw();
                Console.WriteLine(bothPlayersHaveShips.ToString());
                turnCounter++;
                if (playerTurn)
                    Console.WriteLine($"End Of Turn {turnCounter} for {player1.name}");
                else
                    Console.WriteLine($"End Of Turn {turnCounter} for {player2.name}");
                playerTurn = !playerTurn;
                Thread.Sleep(100);
            }
            Console.WriteLine("Game Over");
        }

        void Draw()
        {
            Console.Clear();

            StringBuilder sbheader = new StringBuilder();
            sbheader.Append("    ");
            for (int j = 0; j < mapWidth; j++)
            {
                sbheader.Append((char)(j + 65));
                sbheader.Append(" ");
            }
            sbheader.Append("        ");
            for (int j = 0; j < mapWidth; j++)
            {
                sbheader.Append((char)(j + 65));
                sbheader.Append(" ");
            }
            Console.WriteLine(sbheader.ToString());

            for (int i = 0; i < mapHeight; i++)
            {
                StringBuilder sb = new StringBuilder();
                if(i < 10)
                    sb.Append(i.ToString() + "  ");
                else
                    sb.Append(i.ToString() + " ");

                for (int j = 0; j < mapWidth; j++)
                {
                    if (player1.map[i, j] == 1)
                        sb.Append(" 0");
                    else if (player1.map[i, j] == 2)
                        sb.Append(" X");
                    else
                        sb.Append(" -");
                }

                sb.Append("      ");
                sb.Append(i.ToString() + " ");

                for (int j = 0; j < mapWidth; j++)
                {
                    if (player2.map[i, j] == 1)
                        sb.Append(" 0");
                    else if (player2.map[i, j] == 2)
                        sb.Append(" X");
                    else
                        sb.Append(" -");
                }

                Console.WriteLine(sb.ToString());
            }

            player1.PrintShipList();
            player2.PrintShipList();

            player1.PrintShipList2();
            player2.PrintShipList2();



        }

    }

    


    internal class Player
    {
        internal string name;
        int mapHeight;
        int mapWidth;
        internal int[,] map;
        internal float[,] probability;
        List<Ship> ships = new List<Ship>();

        internal void PositionShips(int _mapHeight, int _mapWidth)
        {
            mapHeight = _mapHeight;
            mapWidth = _mapWidth;
            // set up map
            map = new int[mapHeight, mapWidth];
            probability = new float[mapHeight, mapWidth];
            for (int y = 0; y < mapHeight; y++)
                for (int x = 0; x < mapWidth; x++)
                    probability[y, x] = 10;

            int[] shipSizeCount = { 4, 4, 2, 1 };
            int seed = 0;
            foreach (char c in name.ToCharArray())
                seed += c;
            Random  random = new Random(Seed:seed);
            HashSet<string> occupiedPositions = new HashSet<string>();
            for (int i = 0; i < shipSizeCount.Length; i++)
            {
                int attempts = 0;
                for (int j = 0; j < shipSizeCount[i] && attempts < 1000 ; j++)
                {
                    int shipSize = i + 2;
                    int x = random.Next(mapWidth);
                    int y = random.Next(mapHeight);
                    int direction = random.Next(0,8);
                    Ship ship = null;
                    switch (direction)
                    {
                        case 0: ship = new Ship(shipSize, x, y, x + 1, y + 1); break; // NE
                        case 1: ship = new Ship(shipSize, x, y, x + 0, y + 1); break; // N
                        case 2: ship = new Ship(shipSize, x, y, x + 1, y + 0); break; // E

                        case 3: ship = new Ship(shipSize, x, y, x - 1, y - 1); break; // SW
                        case 4: ship = new Ship(shipSize, x, y, x - 0, y - 1); break; // S
                        case 5: ship = new Ship(shipSize, x, y, x - 1, y - 0); break; // W

                        case 6: ship = new Ship(shipSize, x, y, x - 1, y + 1); break; // NW
                        case 7: ship = new Ship(shipSize, x, y, x + 1, y - 1); break; // SE
                        default: Console.WriteLine("error direction"); break;
                    }

                    bool positionsAlreadyOccupied = false;
                    foreach(ShipPart part in ship.parts)
                        if (occupiedPositions.Contains((part.x.ToString() + "," + part.y.ToString())))
                        { 
                            positionsAlreadyOccupied = true;
                            break;
                        }

                    bool insideOfMap = true;
                    foreach (ShipPart part in ship.parts)
                        if(part.x >= mapHeight  || part.y >= mapWidth || part.x < 0 || part.y < 0)
                            insideOfMap = false;

                    if (positionsAlreadyOccupied || insideOfMap == false)
                    {
                        j--;
                    }
                    else
                    {
                        ships.Add(ship);
                        foreach (ShipPart part in ship.parts)
                            occupiedPositions.Add((part.x.ToString() + "," + part.y.ToString()));
                        Console.Write($"\n ShipL:{ship.length} Parts:");
                        foreach (ShipPart part in ship.parts)
                            Console.Write($" x:{part.x} y:{part.y}");
                    }

                }
            }
        }

        internal void TakeTurn(Player enemy)
        {
            bool turnUntilMiss = true;
            while (turnUntilMiss)
            {
                DecideTarget(out int x, out int y);
                turnUntilMiss = Shoot(x, y, enemy);
                bool areShipsRemaining = enemy.UpdateShips();
                if (areShipsRemaining == false)
                    break;
                break;
            }
        }
        void DecideTarget(out int x, out int y)
        {
            x = 0; y = 0;
            float maxValue = float.MinValue;
            for(int i = 0;i<probability.GetLength(0);i++)
                for(int j = 0;j<probability.GetLength(1);j++)
                    if (probability[i,j] > maxValue)
                    {
                        maxValue = probability[i,j];
                        x = i; y = j;
                    }
        }
        bool Shoot(int x, int y, Player enemy)
        {
            bool wasHit = WasAShipPartHit(x, y, enemy.ships);
            if (wasHit)
            {
                map[x, y] = 2;
                probability[x,y] = 0;
                for(int x1 = -1; x1 <= 1; x1++)
                    for (int y1 = -1; y1 <= 1; y1++)
                        if(x+x1 >= 0 && y+y1 >= 0 && x + x1 < mapHeight && y + y1 < mapWidth)
                            probability[x + x1, y + y1] = (probability[x + x1, y + y1]) * 2;
            }
            else
            {
                map[x, y] = 1;
                probability[x, y] = 0;
                for (int x1 = -1; x1 <= 1; x1++)
                    for (int y1 = -1; y1 <= 1; y1++)
                        if ((x + x1) >= 0 && (y + y1) >= 0 && (x + x1) < mapHeight && (y + y1) < mapWidth)
                            probability[x + x1, y + y1] = (probability[x + x1, y + y1]) / 2;
            }
            return wasHit;
        }
        bool WasAShipPartHit(int x, int y, List<Ship> targets)
        {
            foreach (Ship target in targets)
                if(target.IsShipHit(x, y))
                    return true;
            return false;
        }
        internal bool UpdateShips()
        {
            bool areShipsRemaining = false;
            int shipCount  = 0;
            foreach(Ship ship in ships)
            {
                if (ship.isSunk == false && ship.remainingParts == 0)
                { 
                    ship.isSunk = true;
                }
                if(ship.isSunk == false)
                    shipCount++;
            }
            if(shipCount > 0)
                areShipsRemaining = true;
            return areShipsRemaining;
        }
        internal void PrintShipList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(name);
            sb.Append(": ");
            foreach (Ship ship in ships)
            {
                if(ship.isSunk)
                    continue;
                sb.Append(ship.length.ToString());
                sb.Append("-");
                sb.Append(ship.remainingParts.ToString());
                sb.Append(", ");
            }
            Console.WriteLine(sb.ToString());
        }
        internal void PrintShipList2()
        {
            foreach(Ship ship in ships)
            {
                if(ship.isSunk)
                    continue;
                Console.Write($"\n ShipL:{ship.length} Parts:");
                foreach (ShipPart part in ship.parts)
                    Console.Write($" x:{part.x} y:{part.y}");
            }
            Console.WriteLine();
        }
    }

    internal class  Ship
    {
        internal bool isSunk = false;
        internal int length;
        internal int remainingParts;
        internal List<ShipPart> parts;
        internal Ship(int _length, int posX1, int posY1, int posX2, int posY2)
        {
            length = _length;
            remainingParts = _length;
            parts = new List<ShipPart>();
            int directionX = posX2 - posX1;
            int directionY = posY2 - posY1;
            for (int i = 0; i < length; i++)
                parts.Add(new ShipPart(posX1 + i*directionX, posY1 + i*directionY));
        }
        internal bool IsShipHit(int x, int y)
        {
            foreach (ShipPart part in parts)
                if(part.x == x && part.y == y)
                {  
                    part.destroyed = true;
                    remainingParts--;
                    return true; 
                }
            return false;
        }
    }

    internal class ShipPart
    {
        internal bool destroyed = false;
        internal int x;
        internal int y;
        internal ShipPart(int _x, int _y) { x = _x; y = _y; }
    }

}
