using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Services;
using NHibernate.Envers;
using ClosedXML.Excel;
using System.IO;
using WebApplication1.DataComponents;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly PersonaService _personaService;
        //private readonly EnversRevisionListener _enversRevisionListener;
        //private readonly IAuditReader _auditReader;

        public PersonaController(PersonaService personaService)
        {
            _personaService = personaService;
            //_enversRevisionListener = enversRevisionListener;
            //_auditReader = auditReader;
        }

        [HttpGet]
        [Route("persona/get")]
        public IActionResult Get()
        {
            return Ok(_personaService.GetPersonas());
        }

        [HttpGet]
        [Route("persona/filter")]
        public IActionResult Get([FromQuery] FilterAndSearchPerson filterAndSearchPerson)
        {
            return Ok(_personaService.GetFilterdAndSearchPerson(filterAndSearchPerson)); 
        }

        [HttpGet]
        [Route("persona/get_exel")]
        public IActionResult GetExel()
        {
            using (var workbook = new XLWorkbook())
            {
                var personas = _personaService.GetPersonas();

                var worksheet = workbook.Worksheets.Add("Personas");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Nombre";
                worksheet.Cell(currentRow, 3).Value = "Apellido";

                foreach (var persona in personas)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = persona.Id;
                    worksheet.Cell(currentRow, 2).Value = persona.Nombre;
                    worksheet.Cell(currentRow, 3).Value = persona.Apellido;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Personas.xlsx");
                }
            }

        }

        [HttpPost]
        [Route("persona/Post")]
        public IActionResult Post([FromBody] Persona persona)
        {
            var result = _personaService.AddPersona(persona);
            //var currentRevision = _auditReader.GetCurrentRevision(true);
            //_enversRevisionListener.NewRevision(currentRevision);
            return Ok(result);
        }

        [HttpPut]
        [Route("persona/Put")]
        public IActionResult Update([FromBody] PersonaDTO personaDTO)
        {
            Persona persona = new Persona
            {
                Nombre = personaDTO.Nombre,
                Apellido = personaDTO.Apellido,    
                Id = personaDTO.Id,
            };
            _personaService.UpdatePersona(persona);
            return Ok();
        }

        [HttpDelete]
        [Route("persona/Delete")]
        public IActionResult Delete([FromBody] PersonaDTO personaDTO)
        {
            Persona persona = new Persona
            {
                Nombre = personaDTO.Nombre,
                Apellido = personaDTO.Apellido,
                Id = personaDTO.Id,
            };
            _personaService.DeletePersona(persona);
            return Ok();
        }
    }
}
