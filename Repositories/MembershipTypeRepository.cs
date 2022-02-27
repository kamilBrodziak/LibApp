using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Repositories
{
    public class MembershipTypeRepository : IMembershipTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public MembershipTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddMembershipType(MembershipType membershipType) => _context.MembershipTypes.Add(membershipType);

        public void DeleteMembershipType(int id) => _context.MembershipTypes.Remove(GetMembershipTypeById(id));

        public MembershipType GetMembershipTypeById(int id) => _context.MembershipTypes.SingleOrDefault(b => b.Id == id);

        public IEnumerable<MembershipType> GetMembershipTypes() => _context.MembershipTypes.ToList();

        public void Save() => _context.SaveChanges();

        public void UpdateMembershipType(MembershipType membershipType) => _context.MembershipTypes.Update(membershipType);
    }
}
