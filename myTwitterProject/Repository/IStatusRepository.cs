using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using myTwitterProject.Models;
using Nest;


namespace myTwitterProject.Repositories
{
    public interface IStatusRepository
    {

        IEnumerable<status> GetStatus(int NoOfStatus);
        IEnumerable<status> SearchStatus(string searchTerm);
        IEnumerable<Like> GetLikes(int StatusId);
        IEnumerable<Comment> GetComment(int StatusId);
        Task<IIndexResponse> PostComment(Comment comment);
        void Postlike(int id, status status);
        Task<IUpdateResponse> Putstatus(int id, status status);
        Task<IIndexResponse> Poststatus(status status);
        void Deletestatus(int id);
		void DeleteAccount(status status);
		IEnumerable<ActiveUser> GetActiveUser();
      


    }
}