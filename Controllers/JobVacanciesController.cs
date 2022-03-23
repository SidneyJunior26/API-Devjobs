namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    [Route("api/[controller]")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        // Get api/JobVacancies
        /// <summary>
        /// Consulta de todas as vagas.
        /// </summary>
        /// <returns>Retorna os dados de todas as vagas.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobVacancies = _repository.GetAll();

            return Ok(jobVacancies);
        }

        // Get api/JobVacancies/Id
        /// <summary>
        /// Consulta uma vaga específica pelo Id.
        /// </summary>
        /// <param name="id">Código(Id) da vaga.</param>
        /// <returns>Retorna os dados da vaga.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            return Ok(jobVacancy);
        }

        // Post api/JobVacancies
        /// <summary>
        /// Cadastrar vaga de emprego.
        /// </summary>
        /// <remarks>
        /// {
        ///  "title": "Desenvolvedor .NET Jr",
        ///  "description": "Vaga para sustenção back end de aplicações .NET Core.",
        ///  "company": "Nome da Empresa",
        ///  "isRemote": true,
        ///  "salaryRange": "3000 - 5000"
        /// }
        /// </remarks>
        /// <param name="model">Dados da Vaga.</param>
        /// <returns>Retorna o Objeto recém-criado.</returns>
        /// <response code="201">Vaga cadastrada com sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            Log.Information("POST Job Vacancy chamado");

            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            if (jobVacancy.Title.Length > 30)
                return BadRequest("Título da vaga precisa ter menos de 30 caracteres");

            _repository.Add(jobVacancy);

            return CreatedAtAction(
                "GetById",
                new { id = jobVacancy.Id }, jobVacancy);
        }

        // Put api/JobVacancies/id
        /// <summary>
        /// Atualizar vaga de emprego.
        /// </summary>
        /// <remarks>
        /// {
        ///  "title": "Desenvolvedor .NET Jr",
        ///  "description": "Vaga para sustenção back end de aplicações .NET Core.",
        ///  "isRemote": true,
        ///  "salaryRange": "3000 - 5000"
        /// }
        /// </remarks>
        /// <param name="id">Código(Id) da vaga.</param>
        /// <param name="model">Dados da Vaga</param>
        /// <response code="200">Vaga atualizada com sucesso.</response>
        /// <response code="204">Dados não localizados.</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateVacancyInputModel model)
        {
            Log.Information("PUT Job Vacancy chamado");

            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description, model.IsRemote, model.SalaryRange);
            _repository.Update(jobVacancy);

            return NoContent();
        }

        /// <summary>
        /// Deletar vaga de emprego
        /// </summary>
        /// <param name="id">Id da vaga.</param>
        /// <response code="202">Vaga deletada com sucesso.</response>
        /// <response code="204">Dados não localizados.</response>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Log.Information("Delete Job Vacancy chamado");

            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            _repository.Delete(jobVacancy);

            return Ok(jobVacancy);
        }
    }
}