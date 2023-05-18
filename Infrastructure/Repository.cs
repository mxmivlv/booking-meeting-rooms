using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Infrastructure;

namespace Infrastructure;

public class Repository : DbContext, IRepository
{
    #region Свойства

    public DbSet<MeetingRoom> MeetingRooms { get; set; }

    public DbSet<BookingMeetingRoom> BookingMeetingRooms { get; set; }

    #endregion

    #region Конструктор

    public Repository() { }

    #endregion

    #region Базовые методы

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

    #region Интерфейсные методы

    /// <summary>
    /// Бронирование комнаты
    /// </summary>
    /// <param name="id">Id комнаты</param>
    /// <param name="dateMeeting">Дата бронирования</param>
    /// <param name="startTimeMeeting">Время начала брони</param>
    /// <param name="endTimeMeeting">Время конца брони</param>
    /// <returns>Комнату с данными</returns>
    public async Task<BookingMeetingRoom> BookingMeetingRoomAsync(Guid id, DateOnly dateMeeting, TimeOnly startTimeMeeting, TimeOnly endTimeMeeting)
    {
        var meetingRoom = await MeetingRooms
                              .Include(e => e.BookingMeetingRooms)
                              .FirstOrDefaultAsync(e => e.Id == id)
                          ?? throw new Exception("Данной комнаты нет.");
        
        var bookingMeetingRoom = meetingRoom.BookingRoom(dateMeeting, startTimeMeeting, endTimeMeeting);
        BookingMeetingRooms.Add(bookingMeetingRoom);
        await SaveChangesAsync();

        return bookingMeetingRoom;
    }

    /// <summary>
    /// Получение расписание комнаты
    /// </summary>
    /// <param name="id">Id комнаты</param>
    /// <returns>Расписание комнаты</returns>
    public async Task<MeetingRoom> GetScheduleAsync(Guid id)
    {
        var meetingRoom = await MeetingRooms
                              .Include(e => e.BookingMeetingRooms
                                  .OrderBy(e => e.DateMeeting)
                                  .ThenBy(e => e.StartTimeMeeting))
                              .FirstOrDefaultAsync(e => e.Id == id)
                          ?? throw new Exception("Данной комнаты нет.");

        return meetingRoom;
    }

    /// <summary>
    /// Разбронирование комнаты
    /// </summary>
    public async Task UnbookingMeetingRoomAsync()
    {
        // Получить текущую дату
        var currentDateOnly = DateOnly.FromDateTime(DateTime.Now);
        // Получить текущее время
        var currentTimeOnly = TimeOnly.FromDateTime(DateTime.Now);

        // Получить коллекцию для разбронирования
        var bookingMeetingRooms = MeetingRooms
            .SelectMany(e => e.BookingMeetingRooms)
            .Where(e => (e.DateMeeting < currentDateOnly) 
                        || (e.DateMeeting == currentDateOnly && e.EndTimeMeeting < currentTimeOnly));

        BookingMeetingRooms.RemoveRange(bookingMeetingRooms);
        await SaveChangesAsync();
    }

    #endregion
}