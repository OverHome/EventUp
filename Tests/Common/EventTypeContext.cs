using EventUp.Domain.Models;
using EventUp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Tests.Common;

public class EventTypeContext
{
    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite("DataSource=:memory:").Options;
        var builder = new ConfigurationManager();
        builder.AddInMemoryCollection(new[]
        {
            new KeyValuePair<string, string>("AdminUser:Name", "Admin"),
            new KeyValuePair<string, string>("AdminUser:Password", "qwerty")
        }!);
        
        var context = new AppDbContext(options, builder);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        context.EventTypes.AddRange(
            new []
            {
                new EventType()
                {
                    Name = "String",
                    Code = "string"
                },
                new EventType()
                {
                    Name = "String1",
                    Code = "string1"
                },
                new EventType()
                {
                    Name = "String2",
                    Code = "string2"
                }
                
            }
            );
        
        context.Stations.AddRange(
            new []
            {
                new Station()
                {
                    PlaceName = "Государственная публичная историческая библиотека России",
                    PlaceAddress = "Улица Вильгельма Пика, дом 4, строение 2",
                    GeoLong = 55.835968,
                    GeoLat = 37.635242
                },
                new Station()
                {
                    PlaceName = "Объединение культурных центров Северо-Западного административного округа",
                    PlaceAddress = "Живописная улица, дом 30, корпус 2",
                    GeoLong = 55.792053 ,
                    GeoLat = 37.455705

                },
                new Station()
                {
                    PlaceName = "Объединение культурных центров Северного административного округа",
                    PlaceAddress = "Большая Академическая улица, дом 35",
                    GeoLong = 55.831357,
                    GeoLat = 37.530939 
                }
                
            }
            );

        context.SaveChangesAsync();
        context.Events.AddRange(
            new []
            {
                new Event()
                {
                    Title = "«3+1» в культурном центре «Феникс»",
                    About = "По сюжету трое поросят решили сделать домики на зиму. Один построил домик из соломы, второй — из веток, а третий — из камня. Однажды пришел голодный волк и разрушил домики из соломы и веток. Только каменный устоял. Там и спаслись трое братьев-поросят.",
                    StationId = 2,
                    Station = context.Stations.First(e=> e.Id==2),
                    EventTypeIds = new List<int> { 1, 3},
                    EventType = context.EventTypes.Where(e=> (new int[]{1, 3}).Contains(e.Id)).ToList(),
                    StartDuring = DateTime.Parse("2023-05-27T14:00:00"),
                    EndDuring = DateTime.Parse("2023-05-27T14:40:00")
                },
                new Event()
                {
                    Title= "«От ассистента до владельца бизнеса» в «Московском доме книги»",
                    About = "«Московский дом книги» на Новом Арбате приглашает на презентацию книги Николая Казанского «От ассистента до владельца бизнеса».",
                    StationId = 1,
                    Station = context.Stations.First(e=> e.Id==1),
                    EventTypeIds = new List<int> {1},
                    EventType = context.EventTypes.Where(e=> (new int[]{1}).Contains(e.Id)).ToList(),
                    StartDuring = DateTime.Parse("2023-05-24T19:00:00"),
                    EndDuring = DateTime.Parse("2023-05-24T23:00:00")
                }
            
            });
        context.SaveChangesAsync();
        return context;
    }


    public static void Destroy(AppDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}