using GG.Portafolio.Api.Controllers;
using GG.Portafolio.Shared.TemplateWord;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.Api.Test.Controllers
{
    public class TemplateWordControllerTest
    {
        private readonly ILogger<TemplateWordController> _logger;
        private readonly TemplateWordController _controller;
        private readonly Mock<IWebHostEnvironment> _mockIWebHostEnvironment;

        public TemplateWordControllerTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<TemplateWordController>();

            _mockIWebHostEnvironment = new();

            _mockIWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

            _controller = new TemplateWordController(_logger, _mockIWebHostEnvironment.Object);
        }

        [Fact]
        public void TemplateWordController_NotNull()
        {
            Assert.NotNull(new TemplateWordController(_logger, _mockIWebHostEnvironment.Object));
        }

        [Fact]
        public void TemplateWordController_GetTemplate_IsType_TemplateResponse()
        {
            var result = _controller.GetTemplate();
            var actionResult = Assert.IsType<ActionResult<TemplateResponse>>(result);
            Assert.IsType<TemplateResponse>(actionResult.Value);
        }

        [Fact]
        public void TemplateWordController_GetTemplate_IsType_BadRequest()
        {
            _mockIWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Brak");
            var result = _controller.GetTemplate();
            _mockIWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            var actionResult = Assert.IsType<ActionResult<TemplateResponse>>(result);
            var statusResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.True(statusResult.StatusCode == 400);
        }

        [Fact]
        public void TemplateWordController_GenerateTemplate_IsType_TemplateResponse()
        {
            TemplateRequest request = new()
            {
                Name = "Guillermo Galvan Mendoza",
                ColumnName1 = "Name",
                ColumnName2 = "Edad",
                ColumnName3 = "Alias",
                PasswordChangeControl = "1234546789",
                DetailRows = new List<DetailRow>(),
            };

            request.DetailRows.Add(new DetailRow
            {
                DetailColumn1 = "Silvia Mendoza Herrera",
                DetailColumn2 = "62",
                DetailColumn3 = "Mamá"
            });
            request.DetailRows.Add(new DetailRow
            {
                DetailColumn1 = "Guillermo Galván Pilotzi",
                DetailColumn2 = "62",
                DetailColumn3 = "Papá"
            });

            var result = _controller.GenerateTemplate(request);
            var actionResult = Assert.IsType<ActionResult<TemplateResponse>>(result);
            Assert.IsType<TemplateResponse>(actionResult.Value);
        }

        [Fact]
        public void TemplateWordController_GenerateTemplate_IsType_ObjectResult()
        {
            TemplateRequest request = new()
            {
                Name = "Guillermo Galvan Mendoza",
                ColumnName1 = "Name",
                ColumnName2 = "Edad",
                ColumnName3 = "Alias",
                PasswordChangeControl = "1234546789",
                DetailRows = new List<DetailRow>(),
            };

            request.DetailRows.Add(new DetailRow
            {
                DetailColumn1 = "Silvia Mendoza Herrera",
                DetailColumn2 = "62",
                DetailColumn3 = "Mamá"
            });
            request.DetailRows.Add(new DetailRow
            {
                DetailColumn1 = "Guillermo Galván Pilotzi",
                DetailColumn2 = "62",
                DetailColumn3 = "Papá"
            });

            _controller.ModelState.AddModelError(nameof(TemplateRequest.Name), "El campo es requerido");
            var result = _controller.GenerateTemplate(request);
            _controller.ModelState.Clear();
            var actionResult = Assert.IsType<ActionResult<TemplateResponse>>(result);
            var statusResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.NotEmpty(((ValidationProblemDetails)statusResult.Value).Errors);
        }

        [Fact]
        public void TemplateWordController_GenerateTemplate_IsType_BadRequest()
        {
            TemplateRequest request = new()
            {
                Name = "Guillermo Galvan Mendoza",
                ColumnName1 = "Name",
                ColumnName2 = "Edad",
                ColumnName3 = "Alias",
                PasswordChangeControl = "1234546789",
                DetailRows = null,
            };

            var result = _controller.GenerateTemplate(request);            
            var actionResult = Assert.IsType<ActionResult<TemplateResponse>>(result);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }
    }
}
