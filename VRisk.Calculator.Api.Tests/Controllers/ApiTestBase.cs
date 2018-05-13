namespace VRisk.Calculator.Api.Tests.Controllers
{
    public abstract class ApiTestBase<TApiType> : TestBase<TApiType>
    {
        public override void SetUp()
        {
            base.SetUp();
            Fixture = Fixture.Customize(new ApiTestCustomization());
        }
    }
}
