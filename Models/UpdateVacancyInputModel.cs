namespace DevJobs.API.Models
{
    public record UpdateVacancyInputModel(
        string Title,
        string Description,
        bool IsRemote,
        string SalaryRange)
    {
    }
}