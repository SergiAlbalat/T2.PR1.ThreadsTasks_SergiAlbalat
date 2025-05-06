using System;

namespace T2.PR1.ThreadsTasks_SergiAlbalat
{
    public class Program
    {
        public static DateTime Start = DateTime.Now;
        public static Random random = new Random();
        public static readonly object Consola = new object();
        public static readonly object Pal1 = new object();
        public static readonly object Pal2 = new object();
        public static readonly object Pal3 = new object();
        public static readonly object Pal4 = new object();
        public static readonly object Pal5 = new object();
        public static void Main()
        {
            Thread comensal1 = new Thread(() => Comensal(1));
            Thread comensal2 = new Thread(() => Comensal(2));
            Thread comensal3 = new Thread(() => Comensal(3));
            Thread comensal4 = new Thread(() => Comensal(4));
            Thread comensal5 = new Thread(() => Comensal(5));
            try
            {
                comensal1.Start();
                comensal2.Start();
                comensal3.Start();
                comensal4.Start();
                comensal5.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                comensal1.Interrupt();
                comensal2.Interrupt();
                comensal3.Interrupt();
                comensal4.Interrupt();
                comensal5.Interrupt();
            }
        }

        public static void Comensal(int num)
        {
            DateTime inicioHambre = DateTime.Now;
            while ((DateTime.Now - Start).TotalSeconds < 30)
            {
                Escribir(num, ConsoleColor.Blue, "Pensando");
                Thread.Sleep(random.Next(500, 2001));
                try
                {
                    switch (num)
                    {
                        case 1:
                            Comer(num, Pal1, Pal2, ref inicioHambre);
                            break;
                        case 2:
                            Comer(num, Pal2, Pal3, ref inicioHambre);
                            break;
                        case 3:
                            Comer(num, Pal3, Pal4, ref inicioHambre);
                            break;
                        case 4:
                            Comer(num, Pal4, Pal5, ref inicioHambre);
                            break;
                        case 5:
                            Comer(num, Pal5, Pal1, ref inicioHambre);
                            break;
                    }
                }
                catch
                {
                    throw new Exception($"Comensal {num} ha muerto de hambre");
                }
                
            }
        }

        public static ConsoleColor GetColor(int num)
        {
            switch (num)
            {
                case 1:
                    return ConsoleColor.Magenta;
                case 2:
                    return ConsoleColor.Cyan;
                case 3:
                    return ConsoleColor.Gray;
                case 4:
                    return ConsoleColor.Black;
                case 5:
                    return ConsoleColor.DarkGreen;
                default:
                    return ConsoleColor.White;
            }
        }

        public static void Comer(int num, object pal1, object pal2, ref DateTime inicioHambre)
        {
            Escribir(num, ConsoleColor.Yellow, "Cogiendo primer palillo");
            lock (pal1)
            {
                Escribir(num, ConsoleColor.DarkYellow, "Cogiendo segundo palillo");
                lock (pal2)
                {
                    if ((DateTime.Now - inicioHambre).TotalSeconds > 15)
                    {
                        throw new Exception();
                    }
                    Escribir(num, ConsoleColor.Green, "Comiendo");
                    Thread.Sleep(random.Next(500, 1001));
                    inicioHambre = DateTime.Now;
                    Escribir(num, ConsoleColor.DarkBlue, "Deja el primer palillo");
                }
                Escribir(num, ConsoleColor.DarkCyan, "Deja el segundo palillo");
            }
        }

        public static void Escribir(int num, ConsoleColor color, string mensaje)
        {
            lock (Consola)
            {
                Console.ForegroundColor = GetColor(num);
                Console.BackgroundColor = color;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}]{num}: {mensaje}");
                Console.ResetColor();
            }
        }
    }
}