using EventUp.Application.Services;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Xunit;

namespace Tests.EventTypeTests;

public class DeleteEventType: TestComandBase
{
    [Fact]
    public async Task DeleteEventType_Success()
    {
        //Arrage
        var service = new EventTypeService(Context);
        var eventTypeId = 2;
        //Act
        await service.DeleteEventTypeById(eventTypeId);
        //Assert
        Assert.Null(await Context.EventTypes.SingleOrDefaultAsync(e=>e.Id == eventTypeId));
    }
    
    
}