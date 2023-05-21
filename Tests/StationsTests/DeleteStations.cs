using EventUp.Application.Services;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Xunit;

namespace Tests.StationsTests;

public class DeleteStations : TestComandBase
{
    [Fact]
    public async Task DeleteStations_Success()
    {
        //Arrage
        var service = new StationService(Context);
        var eventTypeId = 2;
        //Act
        await service.DeleteStationsById(eventTypeId);
        //Assert
        Assert.Null(await Context.Stations.SingleOrDefaultAsync(e => e.Id == eventTypeId));
    }
}