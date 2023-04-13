using System;
using VillaApi.Models.Dto;

namespace VillaApi.Data
{
	public static class VillaStore
	{
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO(1,"Pool View", 100, 4),
            new VillaDTO(2, "Beach View", 300, 3)
        };
    }
}

