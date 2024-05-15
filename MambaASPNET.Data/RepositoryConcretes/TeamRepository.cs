using MambaASPNET.Core.Models;
using MambaASPNET.Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MambaASPNET.Data.RepositoryConcretes
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
