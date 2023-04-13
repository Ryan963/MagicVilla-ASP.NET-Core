using System;
namespace VillaApi.Models
{
	public class Villa
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreatedDate { get; set; }
		public Villa(int id, string name)
		{
			this.Id = id;
			this.Name = name;
		}
	}
}

