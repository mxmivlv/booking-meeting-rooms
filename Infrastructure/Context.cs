using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Context : DbContext
{
    #region Свойства

    public DbSet<MeetingRoom> MeetingRooms { get; set; }

    public DbSet<BookingMeetingRoom> BookingMeetingRooms { get; set; }

    #endregion
    
    #region Конструктор
    
    public Context() {}
    
    #endregion
    
    #region Методы

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;User Id=Admin;Password=Admin;Port=5432;Database=Project_6");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Repository).Assembly);
        SeedDataBase(modelBuilder);
    }

    /// <summary>
    /// Заполнение бд начальными данными
    /// </summary>
    private void SeedDataBase(ModelBuilder modelBuilder)
    {
        var meetingRoom1 = new MeetingRoom("Переговорная комната 1.", "Описание переговорной комнаты.");
        var meetingRoom2 = new MeetingRoom("Переговорная комната 2.", "Описание переговорной комнаты.");
        var meetingRoom3 = new MeetingRoom("Переговорная комната 3.", "Описание переговорной комнаты.");
        var meetingRoom4 = new MeetingRoom("Переговорная комната 4.", "Описание переговорной комнаты.");
        var meetingRoom5 = new MeetingRoom("Переговорная комната 5.", "Описание переговорной комнаты.");

        modelBuilder.Entity<MeetingRoom>().HasData(meetingRoom1, meetingRoom2, meetingRoom3, meetingRoom4, meetingRoom5);
    }
    
    #endregion
}