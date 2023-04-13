using System;
using System.ComponentModel.DataAnnotations;

namespace VillaApi.Models.Dto
{
	public class VillaDTO
	{
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public VillaDTO(int id, string name, int occupancy, int sqft)
        {
            Name = name;
            Id = id;
            Occupancy = occupancy;
            Sqft = sqft;
        }
    }
}

