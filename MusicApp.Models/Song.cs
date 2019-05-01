using System;

namespace MusicApp.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public double Duration { get; set; }
        public double Rating { get; set; }
        public string Lyrics { get; set; }
        public Team Team { get; set; }
        public int TeamId { get; set; }

        public void ShowData(string team)
        {
            Console.WriteLine("Название песни: {0}\n" +
                "Жанр: {1}\n" +
                "Продолжительность: {2}\n" +
                "Рейтинг: {3}\n" +
                "Текст: {4}\n" +
                "Группа: {5}\n", 
                Name, Genre, Duration, Rating, Lyrics, team);
        }
    }
}
/*Создать приложение которе позволяет хранить, получать, создавать (добавлять) музыкальные группы, их песни и описание 
 * этих песен (слова, время звучания, рейтинг). Дать пользователю консольное управление этим процессом, в том числе и 
 * поиском по группам, песням. Сортировку по рейтингу (обратную и прямую).*/
