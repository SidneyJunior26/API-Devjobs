using DevJobs.API.Entities;

namespace DevJobs.API.Persistence.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly DevJobsContext _context;
        public JobApplicationRepository(DevJobsContext context)
        {
            _context = context;
        }
        public JobApplication GetById(int id)
        {
            return _context.JobApplications
                .SingleOrDefault(ja => 
                    ja.Id == id
                    );
        }
        public void Add(JobApplication jobApplication)
        {
            _context.JobApplications.Add(jobApplication);
            _context.SaveChanges();
        }

        public void Delete(JobApplication jobApplication)
        {
            _context.JobApplications.Remove(jobApplication);
            _context.SaveChanges();
        }
    }
}