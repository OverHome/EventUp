using EventUp.Application.Services;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Xunit;

namespace Tests.EventsTests;

public class DeleteEvent: TestComandBase
{
    [Fact]
    public async Task DeleteEvent_Success()
    {
        //Arrage
        var service = new EventService(Context, new StationService(Context), new EventTypeService(Context));
        var eventId = 2;
        //Act
        await service.DeleteEventById(eventId);
        //Assert
        Assert.Null(await Context.Events.SingleOrDefaultAsync(e=>e.Id == eventId));
    }
    
    
}