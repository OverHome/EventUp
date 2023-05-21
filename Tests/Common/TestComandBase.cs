using EventUp.Infrastructure.Data;

namespace Tests.Common;

public class TestComandBase : IDisposable
{
    protected readonly AppDbContext Context;

    public TestComandBase()
    {
        Context = EventTypeContext.Create();
    }

    public void Dispose()
    {
        EventTypeContext.Destroy(Context);
        Context.Dispose();
    }
}