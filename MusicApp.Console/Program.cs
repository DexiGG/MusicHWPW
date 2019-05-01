using MusicApp.DataAccess;
using System;

namespace MusicApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int choice = 0;
                bool check = false;
                while (!check)
                {
                    System.Console.Clear();
                    System.Console.Write("\tМузыкальная коллекция\n" +
                                        "1) Показать все группы\n" +
                                        "2) Показать все песни\n" +
                                        "3) Добавить группу\n" +
                                        "4) Добавить песню\n" +
                                        "5) Найти группу\n" +
                                        "6) Найти песню\n" +
                                        "7) Изменить группу\n" +
                                        "8) Изменить песню\n" +
                                        "9) Удалить группу\n" +
                                        "10) Удалить песню\n" +
                                        "11) Выход\n" +
                                        "Выберите действие: ");
                    check = int.TryParse(System.Console.ReadLine(), out choice);
                    if (choice < 1 || choice > 11)
                        check = false;
                    if (choice == 11)
                        Environment.Exit(0);
                }
                System.Console.Clear();

                string name = "";
                if (choice >= 5 && choice % 2 == 1)
                {
                    System.Console.Write("Введите название группы: ");
                    name = System.Console.ReadLine();
                }
                else if(choice >= 6 && choice % 2 == 0)
                {
                    System.Console.WriteLine("Введите название песни: ");
                    name = System.Console.ReadLine();
                }

                switch (choice)
                {
                    case 1: SongsTableDataService.ShowTeams(); break;
                    case 2: SongsTableDataService.ShowSongs(); break;
                    case 3: SongsTableDataService.InsertSong(); break;
                    case 4: SongsTableDataService.InsertSong(); break;
                    case 5: SongsTableDataService.FindTeam(name); break;
                    case 6: SongsTableDataService.FindSong(name); break;
                    case 7: SongsTableDataService.UpdateTeam(name); break;
                    case 8: SongsTableDataService.UpdateSong(name); break;
                    case 9: SongsTableDataService.DeleteTeam(name); break;
                    case 10: SongsTableDataService.DeleteSong(name); break;
                }
            }
        }
    }
}
