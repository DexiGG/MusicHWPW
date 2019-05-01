using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicApp.DataAccess
{
    public class SongsTableDataService
    {
        #region Показать все группы
        public static void ShowTeams()
        {
            int choice = 0;
            bool check = false;
            while (!check)
            {
                Console.Clear();
                Console.Write("1) Сортировать по возрастанию года основания\n" +
                                "2) Сортировать по убыванию года основания\n" +
                                "Выбор: ");
                check = int.TryParse(Console.ReadLine(), out choice);
                if (choice > 2 || choice < 1)
                    check = false;
            }
            Console.Clear();
            try
            {
                using (var context = new SongsContext())
                {
                    if (choice == 1)
                    {
                        var teams = context.Teams
                            .OrderBy(t => t.FoundationYear).ToList();

                        for (int i = 0; i < teams.Count; i++)
                        {
                            teams[i].ShowData();
                        }
                    }
                    else
                    {
                        var teams = context.Teams
                            .OrderByDescending(t => t.FoundationYear).ToList();

                        for (int i = 0; i < teams.Count; i++)
                        {
                            teams[i].ShowData();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Console.Write("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
            }
        }
        #endregion

        #region Показать все песни
        public static void ShowSongs()
        {
            int choice = 0;
            bool check = false;
            while (!check)
            {
                Console.Clear();
                Console.Write("1) Сортировать по возрастанию рейтинга\n" +
                                "2) Сортировать по убыванию рейтинга\n" +
                                "Выбор: ");
                check = int.TryParse(Console.ReadLine(), out choice);
                if (choice > 2 || choice < 1)
                    check = false;
            }
            Console.Clear();
            try
            {
                using (var context = new SongsContext())
                {
                    if (choice == 1)
                    {
                        var songs = context.Songs
                            .OrderBy(s => s.Rating).ToList();

                        for (int i = 0; i < songs.Count; i++)
                        {
                            songs[i].ShowData(GetTeamName(context, songs[i].TeamId));
                        }
                    }
                    else
                    {
                        var songs = context.Songs
                            .OrderByDescending(s => s.Rating).ToList();

                        for (int i = 0; i < songs.Count; i++)
                        {
                            songs[i].ShowData(GetTeamName(context, songs[i].TeamId));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Console.Write("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
            }
        }
        #endregion

        #region Добавление группы
        public static void InsertTeam()
        {
            using (var context = new SongsContext())
            {
                Team team = new Team();

                Console.Write("Введите даннные\n" +
                            "Название: ");
                team.Name = Console.ReadLine();
                
                int year = 0; bool check = false;
                while (!check)
                {
                    Console.Write("Год основания: ");
                    check = int.TryParse(Console.ReadLine(), out year);
                    if (year > DateTime.Now.Year)
                        check = false;
                }
                team.FoundationYear = year;

                Console.Write("Страна: ");
                team.Country = Console.ReadLine();

                try
                {
                    context.Teams.Add(team);
                    context.SaveChanges();
                    Console.WriteLine("Запись добавлена!");
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                }
                Console.Write("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
            }
        }
        #endregion

        #region Добавление песни
        public static void InsertSong()
        {
            using (var context = new SongsContext())
            {
                Song song = new Song();

                Console.Write("Введите даннные\n" +
                            "Название: ");
                song.Name = Console.ReadLine();

                Console.Write("Жанр: ");
                song.Genre = Console.ReadLine();

                double duration = 0; bool check = false;
                while (!check)
                {
                    Console.Write("Продолжительность (мин): ");
                    check = double.TryParse(Console.ReadLine(), out duration);
                }
                song.Duration = duration;

                double rating = 0; check = false;
                while (!check)
                {
                    Console.Write("Рейтинг: ");
                    check = double.TryParse(Console.ReadLine(), out rating);
                    if (rating > 5)
                        check = false;
                }
                song.Rating = rating;

                Console.Write("Текст песни:");
                song.Lyrics = Console.ReadLine();

                string team = "";
                int teamId;
                Console.Write("Группа: ");
                team = Console.ReadLine();
                teamId = GetTeamId(context, team);
                if (teamId == 0)
                {
                    Console.Write("Такой группы в базе не существует, сперва добавьте о нем информацию. " +
                        "Нажмите Enter чтобы продолжить...");
                    Console.ReadLine();
                    return;
                }
                song.TeamId = teamId;

                try
                {
                    context.Songs.Add(song);
                    context.SaveChanges();
                    Console.WriteLine("Запись добавлена!");
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                }
                Console.Write("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
            }
        }
        
        private static int GetTeamId(SongsContext context, string group)
        {
            int id = context.Teams
            .Where(t => t.Name == group)
            .Select(t => t.Id)
            .FirstOrDefault();
            return id;
        }
        #endregion

        #region Обновление данных группы
        public static void UpdateTeam(string name)
        {
            try
            {
                using (var context = new SongsContext())
                {
                    Team team = context.Teams
                        .Where(t => t.Name == name)
                        .FirstOrDefault();

                    if (team != null)
                    {
                        team.ShowData();

                        Console.Write("\nВведите новые даннные\n" +
                            "Название: ");
                        team.Name = Console.ReadLine();
                        
                        int year = 0; bool check = false;
                        while (!check)
                        {
                            Console.Write("Год основания: ");
                            check = int.TryParse(Console.ReadLine(), out year);
                            if (year > DateTime.Now.Year)
                                check = false;
                        }
                        team.FoundationYear = year;

                        Console.Write("Страна: ");
                        team.Country = Console.ReadLine();

                        context.SaveChanges();
                        Console.WriteLine("Запись обновлена!");
                    }
                    else
                        Console.WriteLine("Информация о такой группе в базе отсутствует.");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Console.Write("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
            }
        }
        #endregion

        #region Обновление данных песни
        public static void UpdateSong(string name)
        {
            try
            {
                using (var context = new SongsContext())
                {
                    Song song= context.Songs
                        .Where(s => s.Name == name)
                        .FirstOrDefault();

                    if (song != null)
                    {
                        song.ShowData(GetTeamName(context, song.TeamId));

                        Console.Write("\nВведите новые даннные\n" +
                            "Название: ");
                        song.Name = Console.ReadLine();

                        Console.Write("Жанр: ");
                        song.Genre = Console.ReadLine();

                        double duration = 0; bool check = false;
                        while (!check)
                        {
                            Console.Write("Продолжительность (мин): ");
                            check = double.TryParse(Console.ReadLine(), out duration);
                        }
                        song.Duration = duration;

                        double rating = 0; check = false;
                        while (!check)
                        {
                            Console.Write("Рейтинг: ");
                            check = double.TryParse(Console.ReadLine(), out rating);
                            if (rating > 5)
                                check = false;
                        }
                        song.Rating = rating;

                        Console.Write("Текст песни:");
                        song.Lyrics = Console.ReadLine();

                        string team = "";
                        int teamId;
                        Console.Write("Группа: ");
                        team = Console.ReadLine();
                        teamId = GetTeamId(context, team);
                        if (teamId == 0)
                        {
                            Console.Write("Такой группы в базе не существует, сперва добавьте о нем информацию. " +
                                "Нажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                            return;
                        }
                        song.TeamId = teamId;

                        context.SaveChanges();
                        Console.WriteLine("Запись обновлена!");
                    }
                    else
                        Console.WriteLine("Информация о такой группе в базе отсутствует.");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Console.Write("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
            }
        }
        #endregion

        #region Удаление группы
        public static void DeleteTeam(string name)
        {
            try
            {
                using (var context = new SongsContext())
                {
                    Team team = context.Teams
                        .Where(t => t.Name == name)
                        .FirstOrDefault();

                    if (team != null)
                    {
                        context.Teams.Remove(team);
                        context.SaveChanges();
                        Console.WriteLine("Запись удалена!");
                    }
                    else
                        Console.WriteLine("Информация о такой группе в базе отсутствует.");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Console.Write("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
            }
        }
        #endregion

        #region Удаление песни
        public static void DeleteSong(string name)
        {
            try
            {
                using(var context = new SongsContext())
                {
                    Song song = context.Songs
                        .Where(s => s.Name == name)
                        .FirstOrDefault();

                    if(song != null)
                    {
                        context.Songs.Remove(song);
                        context.SaveChanges();
                        Console.WriteLine("Запись удалена!");
                    }
                    else
                        Console.WriteLine("Информация о такой песне в базе отсутствует.");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Console.Write("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
            }
        }
        #endregion

        #region Найти группу
        public static void FindTeam(string name)
        {
            try
            {
                using (var context = new SongsContext())
                {
                    Team team = context.Teams
                        .Where(t => t.Name == name)
                        .FirstOrDefault();
                    if (team != null)
                        team.ShowData();
                    else
                        Console.WriteLine("Информация о такой группе в базе отсутствует.");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Console.Write("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
            }
        }
        #endregion

        #region Найти песню
        public static void FindSong(string name)
        {
            try
            {
                using (var context = new SongsContext())
                {
                    Song song = context.Songs
                        .Where(s => s.Name == name)
                        .FirstOrDefault();
                    
                    if (song != null)
                        song.ShowData(GetTeamName(context, song.TeamId));
                    else
                        Console.WriteLine("Информация о такой песне в базе отсутствует.");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Console.Write("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
            }
        }

        private static string GetTeamName(SongsContext context, int teamId)
        {
            string name = context.Teams
            .Where(t => t.Id == teamId)
            .Select(t => t.Name)
            .FirstOrDefault();
            return name;
        }
        #endregion
    }
}
