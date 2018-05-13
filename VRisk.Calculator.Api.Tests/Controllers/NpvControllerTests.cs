using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using VRisk.Calculator.Api.Controllers;
using VRisk.Calculator.Api.Models.Dto;
using VRisk.Calculator.Api.Services;

namespace VRisk.Calculator.Api.Tests.Controllers
{
    [TestFixture(Category = "Unit")]
    public class NpvControllerTests : ApiTestBase<NpvController>
    {
        //[Test]
        //public void CalculateNpv_WhenCalculatorThrowsInputValidationException_ShouldReturnBadRequestWithMessage()
        //{
        //    var request = Fixture.Create<CalculateNpvRequest>();
        //    var exception = Fixture.Create<InputValidationException>();
        //    var npvCalculator = FreezeMock<INpvCalculator>();
        //    npvCalculator.Setup(m => m.CalculateNpv(It.IsAny<CalculateNpvRequest>()))
        //        .Throws(exception);

        //    var controller = CreateTarget();
        //    var result = controller.CalculateNpv(request);

        //    result.Should().NotBeNull();
        //    result.Should().BeOfType<BadRequestObjectResult>();
        //    var errorMessage = (result as BadRequestObjectResult).Value.ToString();
        //    errorMessage.Should().Be(exception.Message);
        //}

        [Test]
        public void CalculateNpv_WhenCalculatorThrowsException_ShouldReturnBadRequestWithGenericMessage()
        {
            var request = Fixture.Create<CalculateNpvRequest>();
            var npvCalculator = FreezeMock<INpvCalculator>();
            npvCalculator.Setup(m => m.CalculateNpv(It.IsAny<CalculateNpvRequest>()))
                .Throws<Exception>();

            var controller = CreateTarget();
            var result = controller.CalculateNpv(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            var errorMessage = (result as BadRequestObjectResult).Value.ToString();
            errorMessage.Should().Be("An error has occurred! Please validate your inputs.");
        }

        [Test]
        public void CalculateNpv_WhenCalculatorReturnsValue_ShouldReturnResponse()
        {
            var request = Fixture.Create<CalculateNpvRequest>();
            var response = Fixture.Create<CalculateNpvResponse>();

            var npvCalculator = FreezeMock<INpvCalculator>();
            npvCalculator.Setup(m => m.CalculateNpv(It.IsAny<CalculateNpvRequest>()))
                .Returns(response);

            var controller = CreateTarget();
            var result = controller.CalculateNpv(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.Value.Should().BeOfType<CalculateNpvResponse>();
            var actualResponse = okObjectResult.Value as CalculateNpvResponse;
            actualResponse.Should().Equal(response);
        }
    }
}
