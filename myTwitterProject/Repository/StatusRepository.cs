using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using myTwitterProject.Models;
using Nest;

namespace myTwitterProject.Repositories
{
    public class StatusRepository : IStatusRepository
    {
			public static Uri node = new Uri("https://site:d25c25d83f9db495b4a6e394573bf8c5@kili-eu-west-1.searchly.com");
        public static ConnectionSettings settings = new ConnectionSettings(node, defaultIndex: "statusindex");
        public static ElasticClient client = new ElasticClient(settings);

        public IEnumerable<status> GetStatus(int NoOfStatus)
        {
            var response = client.Search<status>(s => s
             .Size(NoOfStatus)
             .SortDescending(j => j.StatusId)
             .Query(k => k.MatchAll()));

            return response.Documents;
        }


        public IEnumerable<status> SearchStatus(string searchTerm)
        {
            var response = client.Search<status>(s => s
               .From(0)
               .Size(20)
               .Query(k => k.QueryString(l => l.Query(searchTerm))));

            return response.Documents;
        }


        public IEnumerable<Like> GetLikes(int StatusId)
        {
            var response = client.Search<Like>(s => s
             .Query(k => k.QueryString(c => c.Query(StatusId.ToString()))));

            return response.Documents;
        }


        public IEnumerable<Comment> GetComment(int StatusId)
        {
            var response = client.Search<Comment>(s => s
             .Query(k => k.QueryString(c => c.Query(StatusId.ToString()))));

            return response.Documents;
        }


        public Task<IIndexResponse> PostComment(Comment comment)
        {
            return client.IndexAsync(comment, i => i.Refresh(true));

            
        }

		
		public void Postlike(int id, status status)
		{
			client.UpdateAsync<status, object>(u => u.Id(id)
			 .Doc(new { like = status.like })
			 .Refresh());



			var Like = new Like();
			Like.Name = status.Name;
			Like.StatusId = id;
			if (status.date == "del")
			{
				client.DeleteAsync<Like>(id.ToString() + status.Name);
			}
			else
			{
				client.IndexAsync(Like, i => i.Id(id.ToString() + Like.Name));
			}


		}


        public Task<IUpdateResponse> Putstatus(int id, status status)
        {


            return client.UpdateAsync<status, object>(u => u.Id(id)
             .Doc(new { statuses = status.statuses })
             .Refresh());

        }

     
        public Task<IIndexResponse> Poststatus(status status)
        {
            return client.IndexAsync(status, i => i.Id(status.StatusId)
               .Refresh(true));

        }


        public void Deletestatus(int id)
        {
            client.DeleteAsync<status>(id);

        }


		public void DeleteAccount(status status)
		{
			client.DeleteByQuery<status>(q => q.AllIndices()
				.Query(qr => qr.Term(f => f.Name, status.Name)));
			client.DeleteByQuery<Comment>(q => q.AllIndices()
				.Query(qr => qr.Term(f => f.Name, status.Name)));
		}


		public IEnumerable<ActiveUser> GetActiveUser()
		{
			var AggContainer = new AggregationContainer
			{
				Terms = new TermsAggregator
				{
					Field = "name",
					Size = 20
				}
			};
			var SearchRequest = new SearchRequest
			{
				Size=0,
				AllTypes=true,
				Aggregations = new Dictionary<string, IAggregationContainer>
			{
				{"Active_aggs",AggContainer}
			}

			};

			var response = client.Search<ActiveUser>(SearchRequest);

			var data = (from val in response.Aggs.Terms("Active_aggs").Items
						let Name = val.Key
						let Count = val.DocCount
						select new ActiveUser()
						{
							Name = Name,
							Count = Count
						});


			return data;
		}
    }
}