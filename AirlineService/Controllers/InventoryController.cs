using AirlineService.Interfaces;
using AirlineService.Models;
using Common;
using CommonDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Controllers
{
    [Route("api/airline/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        IAirlineRepository _context;
        FlightBookingDBContext _dbContext;
        public InventoryController(IAirlineRepository context, FlightBookingDBContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddFlightDetails(TblFlightMaster[] inventoryDetails)
        {
            try
            {
                int isFlightAddedSuccessfully = _context.AddFlightDetails(inventoryDetails);

                if (isFlightAddedSuccessfully > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Flight details could not be added");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Response = "Error", ResponseMessage = ex.Message });
            }
        }

        [HttpGet("getairlines")]
        public IActionResult GetAirlinesDetails()
        {
            try
            {
                IEnumerable<TblFlightMaster> details = _dbContext.TblFlightMasters.ToList();
               
                if (details != null)
                {

                   

                   
                    return Ok(details);
                }

                return NotFound("No records found with the entered PNR number. Please enter the correct PNR number.");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Response = "Error",
                    ResponseMessage = ex.Message
                });
            }
        }
    }
}
