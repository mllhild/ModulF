using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModulF.Tage
{
    internal class FlappyBird
    {
        List<Pipe> pipes = new List<Pipe>();
        PipeSpawner pipeSpawner = new PipeSpawner();
        Bird bird = new Bird();
        bool endGame = false;
        int rows = 25;
        int cols = 100;
        char[,] map;
        int counter = 0;
        
        internal void Start()
        {
            map = new char[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    map[i, j] = '.';

            while (!endGame)
            {
                Step();
                Draw();
                Thread.Sleep(200);
            }
            Console.WriteLine(" \n\n\n");
            Console.WriteLine("_______________________________________________________");
            Console.WriteLine($"Score: {counter}");
            Console.WriteLine("_______________________________________________________");
            Console.WriteLine("\n\n\n");
            Thread.Sleep(500);
        }
        void Step()
        {
            if (counter % 20 == 0)
                pipeSpawner.CreateNewPipe(pipes, rows, cols);

            // delete old bird position
            map[bird.pos.y,bird.pos.x ] = '.';
            map[bird.pos.y, bird.pos.x-1] = '.';

            bird.ApplyGravity();
            if (bird.pos.y < 0 || bird.pos.y >= rows) { endGame = true; return; }

            //advance pipes
            foreach (Pipe pipe in pipes)
            {
                if (!pipe.active)
                    continue;

                for (int i = pipe.lowerLimit; i <= pipe.upperLimit; i++)
                    for (int j = pipe.leftLimit; j <= pipe.rightLimit && j < cols; j++)
                        map[i, j] = '.';

                pipe.AdvancePipe();

                if (pipe.rightLimit< 0)
                    { pipe.active = false; continue; }

                for (int i = pipe.lowerLimit; i <= pipe.upperLimit; i++)
                    for (int j = pipe.leftLimit; j <= pipe.rightLimit && j < cols; j++)
                        map[i, j] = '#';
            }

            // check collision
            foreach (Pipe pipe in pipes)
                if (pipe.IsInPipe(bird.pos))
                    endGame = true;

            // clean up pipes
            for(int i=pipes.Count-1; i>=0; i--)
                if (pipes[i].rightLimit < 0)
                    pipes.RemoveAt(i);



                    // score
                    map[0, 0] = 's';
            map[0, 1] = 'c';
            map[0, 2] = 'o';
            map[0, 3] = 'r';
            map[0, 4] = 'e';
            map[0, 5] = ' ';
            char[] chars = counter.ToString().ToCharArray();
            for(int i = 0;  i < chars.Length; i++)
                map[0, i+6] = chars[i];

            // Input
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    bird.FlapWIngs();
                }
            }
            //if (Console.ReadKey().Key == ConsoleKey.UpArrow)
                
            if (bird.pos.y < 0 || bird.pos.y >= rows) { endGame = true; return; }

            map[bird.pos.y, bird.pos.x] = 'b';
            map[bird.pos.y, bird.pos.x-1] = '>';



            counter++;
        }
        void Draw()
        {
            Console.Clear();
            for(int i = 0; i < rows; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(map[i, j]);
                }
                Console.WriteLine(sb.ToString());
            }
        }
    }
    internal class Bird
    {
        internal Vector2 pos = new Vector2(10,10);
        internal void ApplyGravity()
        {
            pos.y++;
        }
        internal void FlapWIngs()
        {
            pos.y -= 2;
        }
    }
    internal class Pipe
    {
        internal int upperLimit;
        internal int lowerLimit;
        internal int leftLimit;
        internal int rightLimit;

        internal bool active = true;
        internal bool IsInPipe(Vector2 pos)
        {
            if(pos.y <= upperLimit && pos.y >= lowerLimit)
                if (pos.x <= rightLimit && pos.x >= leftLimit)
                    return true;
            return false;
        }
        internal void AdvancePipe()
        {
            rightLimit--;
            leftLimit--;
            if (leftLimit < 0 && leftLimit < rightLimit)
                leftLimit++;
        }

    }
    internal class PipeSpawner
    {
        internal void CreateNewPipe(List<Pipe> pipes, int rows, int cols)
        {

            //upper pipe
            Pipe pipe = new Pipe();
            pipe.upperLimit = new Random().Next(rows/2);
            pipe.lowerLimit = 0;
            pipe.leftLimit = cols;
            pipe.rightLimit = pipe.leftLimit + new Random().Next(5);
            pipes.Add(pipe);

            Pipe pipe2 = new Pipe();
            pipe2.leftLimit = pipe.leftLimit;
            pipe2.rightLimit = pipe.rightLimit;
            pipe2.upperLimit = rows-1;
            pipe2.lowerLimit = pipe.upperLimit + (pipe.rightLimit - pipe.leftLimit) + new Random().Next(3) + 6;
            if(pipe2.lowerLimit > pipe2.upperLimit)
                pipe2.lowerLimit = pipe2.upperLimit;
            pipes.Add(pipe2);
        }
    }
    internal class Vector2
    {
        internal int x, y;
        internal Vector2(int _x, int _y) { x = _x; y = _y; }
    }
}
