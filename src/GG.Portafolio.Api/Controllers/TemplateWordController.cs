using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GG.Portafolio.Shared.TemplateWord;
using GenerateWord;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using GG.Portafolio.Shared;

namespace GG.Portafolio.Api.Controllers
{
    [Authorize(Policy.PolicyBasic)]
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateWordController : ControllerBase
    {
        private const string TemplateFollder = "Templates";
        private readonly ILogger<TemplateWordController> _logger;
        private readonly IWebHostEnvironment _environment;

        public TemplateWordController(ILogger<TemplateWordController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        [HttpGet("downloadtemplate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TemplateResponse> GetTemplate()
        {
            try
            {
                string path = Path.Combine(_environment.ContentRootPath, $"{TemplateFollder}{Path.DirectorySeparatorChar}Template.docx");

                return new TemplateResponse { File = System.IO.File.ReadAllBytes(path) , Extension = ".docx" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                return Problem("Ocurrio un error.", statusCode: (int)HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("generatetemplate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TemplateResponse> GenerateTemplate(TemplateRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string path = Path.Combine(_environment.ContentRootPath, $"{TemplateFollder}{Path.DirectorySeparatorChar}Template.docx");

                    DataWord dataWord = new()
                    {
                        KeyValues = new Dictionary<string, string>(),
                        Password = request.PasswordChangeControl,
                        PathTemplate = path,
                        PathFileFinaly = Path.Combine(_environment.ContentRootPath, $"{TemplateFollder}{Path.DirectorySeparatorChar}"),
                        DynamicTable = new List<DynamicTable>(),
                        PathEmbeddedPackageParts = string.Empty
                    };

                    dataWord.KeyValues.Add("Nombre", request.Name);
                    dataWord.KeyValues.Add("<Fecha>", DateTime.Now.ToString("dd/MM/yyyy"));
                    dataWord.KeyValues.Add("Columna1", request.ColumnName1);
                    dataWord.KeyValues.Add("Columna2", request.ColumnName2);
                    dataWord.KeyValues.Add("Columna3", request.ColumnName3);

                    DynamicTable dynamicTable = new() { TableName = "DatosColumna", TypeIntegration = TypeIntegration.InRow };

                    if (request.DetailRows.Count == 0)
                    {
                        request.DetailRows.Add(new DetailRow
                        {
                            DetailColumn1 = string.Empty,
                            DetailColumn2 = string.Empty,
                            DetailColumn3 = string.Empty
                        });
                    }

                    request.DetailRows.ForEach(x =>
                    {
                        Row row = new();
                        row.Columns.Add(0, x.DetailColumn1);
                        row.Columns.Add(1, x.DetailColumn2);
                        row.Columns.Add(2, x.DetailColumn3);
                        dynamicTable.Rows.Add(row);
                    });
                    dataWord.DynamicTable.Add(dynamicTable);
                    new Generate().GenerateWord(dataWord);

                    byte[] file = null;
                    string typeFile = string.Empty;
                    FileInfo fileInfo = new(dataWord.PathFileFinaly);
                    file = System.IO.File.ReadAllBytes(dataWord.PathFileFinaly);
                    typeFile = fileInfo.Extension;
                    System.IO.File.Delete(dataWord.PathFileFinaly);
                    return new TemplateResponse
                    {
                        Extension = typeFile,
                        File =  file
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {request}", MethodBase.GetCurrentMethod().Name, request);
                return BadRequest();
            }

            return ValidationProblem(ModelState);
        }
    }
}
