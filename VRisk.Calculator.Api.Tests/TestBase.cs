using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;

namespace VRisk.Calculator.Api.Tests
{
    public abstract class TestBase<TTarget>
    {
        protected IFixture Fixture { get; set; }

        [SetUp]
        public virtual void SetUp()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        protected TTarget CreateTarget()
        {
            return Fixture.Create<TTarget>();
        }

        protected Mock<TType> FreezeMock<TType>()
            where TType: class
        {
            return Fixture.Freeze<Mock<TType>>();
        }
    }
}
