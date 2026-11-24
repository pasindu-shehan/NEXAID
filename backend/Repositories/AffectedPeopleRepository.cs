using backend.Dto;
using backend.Models;

namespace backend.Repositories
{
    public class AffectedPeopleRepository: IAffextedPeopleRepository
    {
        private readonly ApplicationDbContext _context;
        public AffectedPeopleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<AffectedPeople> GetAllAffectedPeople()
        {
            return _context.AffectedPeople.ToList();
        }

        public string AddAffectedPeople(AffectedPeopleDto affectedPeopleDto)
        {
            var affectedPeople = new AffectedPeople
            {
                Name = affectedPeopleDto.Name,
                Age = affectedPeopleDto.Age,
                Gender = affectedPeopleDto.Gender,
                ContactNumber = affectedPeopleDto.ContactNumber,
                Email = affectedPeopleDto.Email,
                Address = affectedPeopleDto.Address,
                Country = affectedPeopleDto.Country,
                NeedsDescription = affectedPeopleDto.NeedsDescription
            };
            _context.AffectedPeople.Add(affectedPeople);
            _context.SaveChanges();
            return "Student Added Succefully";
        }

        public string UpdateAffectedPeople(int id, AffectedPeopleDto affectedPeopleDto)
        {
            var affectedPeople = _context.AffectedPeople.FirstOrDefault(ap => ap.Id == id);
            if (affectedPeople == null)
            {
                return "Affected Person not found";
            }
            affectedPeople.Name = affectedPeopleDto.Name;
            affectedPeople.Age = affectedPeopleDto.Age;
            affectedPeople.Gender = affectedPeopleDto.Gender;
            affectedPeople.ContactNumber = affectedPeopleDto.ContactNumber;
            affectedPeople.Email = affectedPeopleDto.Email;
            affectedPeople.Address = affectedPeopleDto.Address;
            affectedPeople.Country = affectedPeopleDto.Country;
            affectedPeople.NeedsDescription = affectedPeopleDto.NeedsDescription;
            _context.AffectedPeople.Update(affectedPeople);
            _context.SaveChanges(); 
            return "Affected Person Updated Successfully";
        }

        public string DeleteAffectedPeople(int id)
        {
            var affectedPeople = _context.AffectedPeople.FirstOrDefault(ap => ap.Id == id);
            if (affectedPeople == null)
            {
                return "Affected Person not found";
            }
            _context.AffectedPeople.Remove(affectedPeople);
            _context.SaveChanges();
            return "Affected Person Deleted Successfully";
        }
    }
}
