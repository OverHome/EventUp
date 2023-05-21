using EventUp.Application.Services;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Xunit;

namespace Tests.StationsTests;

public class CreateStations : TestComandBase
{
    [Fact]
    public async Task CreateStations_Success()
    {
        //Arrage
        var service = new StationService(Context);
        var placeName = "Московский дом книги на Новом Арбате";
        var placeAddress = "улица Новый Арбат, 8";
        var geoLong = 55.753109;
        var geoLat = 37.595348;

        //Act
        var res = await service.AddStations(new()
            { PlaceName = placeName, PlaceAddress = placeAddress, GeoLong = geoLong, GeoLat = geoLat });

        //Assert
        Assert.NotNull(await Context.Stations.SingleOrDefaultAsync(e =>
            e.Id == res.Id && e.PlaceName == placeName && e.PlaceAddress == placeAddress && e.GeoLong == geoLong &&
            e.GeoLat == geoLat));
    }
}