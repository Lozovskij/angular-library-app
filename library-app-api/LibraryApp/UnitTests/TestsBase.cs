using AutoFixture;

namespace UnitTests;

public abstract class TestsBase
{
    protected Fixture _fixture;

    public TestsBase()
	{
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}
