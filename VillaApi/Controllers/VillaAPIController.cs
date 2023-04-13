using System;
using System.Xml.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaApi.Data;
using VillaApi.Logging;
using VillaApi.Models.Dto;
namespace VillaApi.Controllers;
[Route("api/VillaAPI")]
[ApiController]
public class VillaAPIController: ControllerBase
{
    public readonly ILogging _logger;

    public VillaAPIController(ILogging logger)
    {
        _logger = logger;
    }
    // get ALl villas
	[HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
	{
        _logger.log("Getting all Villas", "error");
		return Ok(VillaStore.villaList);
	}

	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
		if (id == 0)
		{
			return BadRequest("there is not");
		}
		var villa = VillaStore.villaList.FirstOrDefault(i => i.Id == id);
		if (villa == null)
		{
			return NotFound();
		}
        return Ok(villa);
    }

	[HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villa)
	{
		if (villa == null)
		{
			return BadRequest(villa);
		}
		if (villa.Id > 0)
		{
			return StatusCode(StatusCodes.Status500InternalServerError);
		}
		villa.Id = VillaStore.villaList
		.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;


		VillaStore.villaList.Add(villa);
		return Ok(villa);
	}

	[HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteVilla(int id)
	{
        if (id == 0)
        {
            return BadRequest("there is not");
        }
        var villa = VillaStore.villaList.FirstOrDefault(i => i.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
		VillaStore.villaList.Remove(villa);
		return NoContent();
    }

    [HttpPatch("{id:int}", Name = "PatchVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult PatchVilla(int id, [FromBody] JsonPatchDocument<VillaDTO> villaPatch)
    {
        if (villaPatch == null)
        {
            return BadRequest();
        }

        var villa = VillaStore.villaList.FirstOrDefault(i => i.Id == id);

        if (villa == null)
        {
            return NotFound();
        }
       villaPatch.ApplyTo(villa, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }


}

