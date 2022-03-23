using DevJobs.API.Entities;

namespace DevJobs.API.Persistence.Repositories
{
    public interface IJobApplicationRepository
    {
        JobApplication GetById(int id);
        void Add(JobApplication jobApplication);
        void Delete(JobApplication jobApplication);
    }
}