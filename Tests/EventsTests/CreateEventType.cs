using EventUp.Application.Services;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Xunit;

namespace Tests.EventsTests;

public class CreateEvent : TestComandBase
{
    [Fact]
    public async Task CreateEvent_Success()
    {
        //Arrage
        var service = new EventService(Context, new StationService(Context), new EventTypeService(Context));
        var title = "«Библионочь» в Государственной публичной исторической библиотеке России";
        var about =
            "В филиале в Страросадком переулке (дом 9) тематическая программа откроется пешеходной экскурсией в окрестностях библиотеки «Ивановская горка — место трех конфессий». Участники узнают об истории этого места и зданий. Начало в 17:00.";
        var stationId = 1;
        var eventTypeIds = new List<int>() { 1, 2};
        var startDuring = DateTime.Parse("2023-05-27T17:00:00");
        var endDuring = DateTime.Parse("2023-05-27T20:00:00");

        //Act
        var res = await service.AddEvent(new()
        {
            Title = title, About = about, StationId = stationId, EventTypeIds = eventTypeIds, StartDuring = startDuring,
            EndDuring = endDuring
        });

        //Assert
        Assert.NotNull(await Context.Events.SingleOrDefaultAsync(
            e => e.Id == res.Id&& e.Title == title && e.About == about && e.StationId == stationId &&
                 e.EventTypeIds == eventTypeIds && e.StartDuring == startDuring &&
                 e.EndDuring == endDuring && e.Station == Context.Stations.First(s => s.Id == e.StationId) &&
                 e.EventType.All(et => eventTypeIds.Contains(et.Id))));
    }
}