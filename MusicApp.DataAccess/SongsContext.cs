namespace MusicApp.DataAccess
{
    using MusicApp.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class SongsContext : DbContext
    {
        // Контекст настроен для использования строки подключения "SongsContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "MusicApp.DataAccess.SongsContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "SongsContext" 
        // в файле конфигурации приложения.
        public SongsContext()
            : base("SongsContext")
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Song> Songs { get; set; }
        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}