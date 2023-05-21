using EventUp.Application.Services;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Xunit;

namespace Tests.EventTypeTests;

public class CreateEventType: TestComandBase
{
    [Fact]
    public async Task CreateEventType_Success()
    {
        //Arrage
        var service = new EventTypeService(Context);
        var eventTypeName = "Str";
        var eventTypeCode = "str";
        //Act
        var res = await service.AddEventType(new(){Name = eventTypeName, Code = eventTypeCode});
        //Assert
        Assert.NotNull(await Context.EventTypes.SingleOrDefaultAsync(e=>e.Id == res.Id && e.Name== eventTypeName && e.Code == eventTypeCode));
    }
}