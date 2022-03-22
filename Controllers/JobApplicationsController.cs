namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobApplicationsController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Cadastrar aplicação de vaga.
        /// </summary>
        /// <remarks>
        /// {
        ///  "applicantName": "Sidney Junior",
        ///  "applicantEmail": "sidneyemail@gmail.com",
        ///  "idVacancy": 1
        /// }
        /// </remarks>
        /// <param name="id">Id da vaga que vai aplicar.</param>
        /// <param name="model">Dados do usuário.</param>
        /// <returns>Retorna o Objeto recém-criado.</returns>
        /// <response code="201">Aplicação cadastrada com sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            var application = new JobApplication(
                model.ApplicantName,
                model.ApplicantEmail,
                id
            );

            _repository.AddApplication(application);

            return NoContent();
        }
    }
}