using MambaASPNET.Business.Exceptions;
using MambaASPNET.Business.Services.Abstracts;
using MambaASPNET.Core.Models;
using MambaASPNET.Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MambaASPNET.Business.Services.Concretes
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeamService(ITeamRepository teamRepository, IWebHostEnvironment webHostEnvironment)
        {
            _teamRepository = teamRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public void CreateTeam(Team team)
        {
            if (team.ImageFile == null) throw new NullReferenceException("Null reference");
            if (team.ImageFile.Length > 2097152)
                throw new FileSizeException("ImageFile", "File size error!");
            if (team == null)
                throw new NullReferenceException("Null team");
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(team.ImageFile.FileName);
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Teams\" + fileName;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                team.ImageFile.CopyTo(stream);
            }
            team.ImageUrl = fileName;
            _teamRepository.Add(team);
            _teamRepository.Commit();
        }

        public void DeleteTeam(int id)
        {
            var existService = _teamRepository.Get(x => x.Id == id);
            if (existService == null) throw new NullReferenceException("Null service");


            string path = _webHostEnvironment + @"\Upload\Services" + existService.ImageFile.FileName;
            //if (!File.Exists(path)) throw new Exceptions.FileNotFoundException("ImageFile", "File not found!");
            File.Delete(path);

            _teamRepository.Delete(existService);
            _teamRepository.Commit();
        }

        public List<Team> GetAllTeams(Func<Team, bool>? func = null)
        {
            return _teamRepository.GetAll(func);
        }

        public Team GetTeam(Func<Team, bool>? func = null)
        {
            return _teamRepository.Get(func);
        }

        public void UpdateTeam(int id, Team team)
        {
            throw new NotImplementedException();
        }
    }
}
