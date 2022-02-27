using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LibApp.Interfaces
{
    public interface IMembershipTypeRepository
    {
        IEnumerable<MembershipType> GetMembershipTypes();
        MembershipType GetMembershipTypeById(int id);
        void AddMembershipType(MembershipType membershipType);
        void UpdateMembershipType(MembershipType membershipType);
        void DeleteMembershipType(int id);

        void Save();
    }
}
