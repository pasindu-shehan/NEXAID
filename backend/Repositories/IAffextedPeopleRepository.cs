using backend.Dto;
using backend.Models;

namespace backend.Repositories
{
    public interface IAffextedPeopleRepository
    {
        List<AffectedPeople> GetAllAffectedPeople();
        string AddAffectedPeople(AffectedPeopleDto affectedPeople);

        string UpdateAffectedPeople(int id, AffectedPeopleDto affectedPeople);
        string DeleteAffectedPeople(int id);
    }
}
