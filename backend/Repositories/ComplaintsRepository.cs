using backend.Dto;
using backend.Models;

namespace backend.Repositories
{
    public class ComplaintsRepository : IComplaintsRepository
    {
        private readonly ApplicationDbContext _context;
        public ComplaintsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Complaints> GetAllComplaints()
        {
            return _context.Complaints.ToList();
        }

        public Complaints AddComplaint(CreateComplaintDto complaint)
        {
            var newComplaint = new Complaints
            {
                Id = complaint.Id,
                Title = complaint.Title,
                Description = complaint.Description,
                Category = complaint.Category,
                Location = complaint.Location
            };
            _context.Complaints.Add(newComplaint);
            _context.SaveChanges();
            return newComplaint;
        }

        public string UpdateComplaint(int id, CreateComplaintDto complaint)
        {
            var existingComplaint = _context.Complaints.FirstOrDefault(c => c.Id == id);
            if (existingComplaint == null)
            {
                return "Complaint not found";
            }
            existingComplaint.Title = complaint.Title;
            existingComplaint.Description = complaint.Description;
            existingComplaint.Category = complaint.Category;
            existingComplaint.Location = complaint.Location;
            _context.Complaints.Update(existingComplaint);
            _context.SaveChanges();
            return "Complaint Updated Successfully";
        }

        public string DeleteComplaint(int id)
        {
            var complaint = _context.Complaints.FirstOrDefault(c => c.Id == id);
            if (complaint == null)
            {
                return "Complaint not found";
            }
            _context.Complaints.Remove(complaint);
            _context.SaveChanges();
            return "Complaint Deleted Successfully";
        }

        public async Task SaveEmbeddingAsync(
    int complaintId,
    float[] embedding)
        {
            var complaintEmbedding = new ComplaintEmbedding
            {
                ComplaintId = complaintId,

                EmbeddingJson = System.Text.Json.JsonSerializer.Serialize(embedding),

                ModelName = "all-MiniLM-L6-v2",

                CreatedAt = DateTime.UtcNow
            };

            await _context.ComplaintEmbeddings.AddAsync(complaintEmbedding);

            await _context.SaveChangesAsync();
        }

        //public string UpdateComplaint(int id, CreateComplaintDto complaint)
        //{
        //    var existingComplaint = _context.Complaints.FirstOrDefault(c => c.Id == id);
        //    if (existingComplaint == null)
        //    {
        //        return "Complaint not found";
        //    }
        //    existingComplaint.Title = complaint.Title;
        //    existingComplaint.Description = complaint.Description;
        //    existingComplaint.Category = complaint.Category;
        //    existingComplaint.Location = complaint.Location;
        //    _context.Complaints.Update(existingComplaint);
        //    _context.SaveChanges();
        //    return "Complaint Updated Successfully";
        //}

        //public string DeleteComplaint(int id)
        //{
        //    var complaint = _context.Complaints.FirstOrDefault(c => c.Id == id);
        //    if (complaint == null)
        //    {
        //        return "Complaint not found";
        //    }
        //    _context.Complaints.Remove(complaint);
        //    _context.SaveChanges();
        //    return "Complaint Deleted Successfully";
        //    }
        //    complaint.Id = affectedPeopleDto.Name;
        //    affectedPeople.Age = affectedPeopleDto.Age;
        //    affectedPeople.Gender = affectedPeopleDto.Gender;
        //    affectedPeople.ContactNumber = affectedPeopleDto.ContactNumber;
        //    affectedPeople.Email = affectedPeopleDto.Email;
        //    affectedPeople.Address = affectedPeopleDto.Address;
        //    affectedPeople.Country = affectedPeopleDto.Country;
        //    affectedPeople.NeedsDescription = affectedPeopleDto.NeedsDescription;
        //    _context.AffectedPeople.Update(affectedPeople);
        //    _context.SaveChanges();
        //    return "Affected Person Updated Successfully";
        //}

        //public string DeleteAffectedPeople(int id)
        //{
        //    var affectedPeople = _context.AffectedPeople.FirstOrDefault(ap => ap.Id == id);
        //    if (affectedPeople == null)
        //    {
        //        return "Affected Person not found";
        //    }
        //    _context.AffectedPeople.Remove(affectedPeople);
        //    _context.SaveChanges();
        //    return "Affected Person Deleted Successfully";
        //}

    }
}
