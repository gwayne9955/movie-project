using System.Collections.Generic;

namespace movie_project.API.Resources
{
	public class QueryResultResource<T>
	{
		public List<T> Items { get; set; } = new List<T>();
		public int TotalItems { get; set; } = 0;
	}
}
