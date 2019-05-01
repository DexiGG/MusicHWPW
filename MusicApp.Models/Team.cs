using System;

namespace MusicApp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FoundationYear { get; set; }
        public string Country { get; set; }

        public void ShowData()
        {
            Console.WriteLine("Название группы: {0}\n" +
                "Год основания: {1}\n" +
                "Страна: {2}\n", Name, FoundationYear, Country);
        }
    }
}
