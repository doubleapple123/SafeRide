using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using SafeRide.src.Controllers;
using SafeRide.src.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Models;
using SRUnitTests.Mocks;

namespace SRUnitTests
{
    public class ReportHazardControllerTestEmpty
    {
        private ReportHazardController _controller;
        private IReportHazardService _service;

        public ReportHazardControllerTestEmpty()
        {
            _service = new MockReportHazardServiceEmpty();
            _controller = new ReportHazardController(_service);
        }

        [Fact]
        public void GetHazards_ReturnsNotFound()
        {
            //act
            var notFoundResult = _controller.GetHazards();

            //assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
    }
}
