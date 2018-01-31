using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using myTwitterProject.Models;
using Nest;
using myTwitterProject.Repositories;



namespace myTwitterProject.Controllers
{
	public class StatusController : ApiController
	{


		public static StatusRepository Repository = new StatusRepository();
		private StatusContext db = new StatusContext();



		public IEnumerable<status> Getstatus(int NoOfStatus)
		{

			return Repository.GetStatus(NoOfStatus);

		}


		[HttpGet]
		public IEnumerable<status> SearchStatus(string searchTerm)
		{


			return Repository.SearchStatus(searchTerm);
		}
		[HttpPost]
		public async Task<IHttpActionResult> PostComment(Comment comment)
		{
			if (!ModelState.IsValid)
			{
				 return BadRequest(ModelState);
			}

			
			await Repository.PostComment(comment);
			return Ok("{}");
			

		}
		[HttpGet]
		public IEnumerable<Comment> GetComment(int StatusId)
		{

			return Repository.GetComment(StatusId);

		}


		[HttpGet]
		public IEnumerable<ActiveUser> GetActiveUser()
		{

			return Repository.GetActiveUser();

		}

		
		[HttpGet]
		public IEnumerable<Like> GetLikes(int StatusId)
		{

			return Repository.GetLikes(StatusId);

		}

		[ResponseType(typeof(status))]
		public IHttpActionResult Postlike(int id, status status)
		{


			//NEST 6.x
			//await client.UpdateAsync<status, dynamic>(new DocumentPath<status>(id), u => u.Index("statusindex").Doc(updateDoc));

			Repository.Postlike(id, status);
			return Ok();

			//var likeup = new status() { StatusId = id, like = status.like };
			//db.status.Attach(likeup);
			//db.Entry(likeup).Property(x => x.like).IsModified = true;

			//try
			//{
			//	await db.SaveChangesAsync();
			//}
			//catch (DbUpdateConcurrencyException)
			//{
			//	if (!statusExists(id))
			//	{
			//		return NotFound();
			//	}
			//	else
			//	{
			//		throw;
			//	}
			//}

		}



		public async Task<IHttpActionResult> Putstatus(int id, status status)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != status.StatusId)
			{
				return BadRequest();
			}

			await Repository.Putstatus(id, status);
			return Ok("{}");

			//db.Entry(status).State = EntityState.Modified;

			//try
			//{
			// await db.SaveChangesAsync();
			//}
			//catch (DbUpdateConcurrencyException)
			//{
			// if (!statusExists(id))
			// {
			//  return NotFound();
			// }
			// else
			// {
			//  throw;
			// }
			//}


		}


		[HttpPost]
		public async Task<IHttpActionResult> Poststatus(status status)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//db.status.Add(status);
			//await db.SaveChangesAsync();
			await Repository.Poststatus(status);
			return Ok("{}");
		}


		[ResponseType(typeof(status))]
		public IHttpActionResult Deletestatus(int id)
		{
			//status status = await db.status.FindAsync(id);
			//if (status == null)
			//{
			// return NotFound();
			//}

			//db.status.Remove(status);
			//await db.SaveChangesAsync();

			Repository.Deletestatus(id);
			return Ok();
		}


		[HttpPost]
		public IHttpActionResult DeleteAcc(status status)
		{

			if (status.Name == null)
			{
				return BadRequest();
			}


			Repository.DeleteAccount(status);


			return Ok();
		}


		#region helpers
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		
	}
}
		#endregion