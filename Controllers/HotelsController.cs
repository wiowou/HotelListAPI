using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListAPI.Data;
using HotelListAPI.DTO;
using AutoMapper;

namespace HotelListAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
  private readonly HotelListingDbContext _context;
  private readonly IMapper _mapper;

  public HotelsController(HotelListingDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  // GET: api/Hotels
  [HttpGet]
  public async Task<ActionResult<IEnumerable<HotelSummaryDTO>>> GetHotels()
  {
    if (_context.Hotels == null)
    {
      return NotFound();
    }
    var hotels = await _context.Hotels.ToListAsync();
    var results = _mapper.Map<List<HotelSummaryDTO>>(hotels);
    return Ok(results);
  }

  // GET: api/Hotels/5
  [HttpGet("{id}")]
  public async Task<ActionResult<HotelDTO>> GetHotel(int id)
  {
    if (_context.Hotels == null)
    {
      return NotFound();
    }
    //var hotel = await _context.Hotels.FindAsync(id);
    var hotel = await _context.Hotels.Include(q => q.Country).FirstOrDefaultAsync(q => q.Id == id);

    if (hotel == null)
    {
      return NotFound();
    }

    var response = _mapper.Map<HotelDTO>(hotel);

    return response;
  }

  // PUT: api/Hotels/5
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPatch("{id}")]
  public async Task<ActionResult<HotelSummaryDTO>> PutHotel(int id, UpdateHotelDTO updateHotelDTO)
  {
    if (_context.Hotels == null)
    {
      return NotFound();
    }
    var hotel = await _context.Hotels.FindAsync(id);
    if (hotel == null)
    {
      return NotFound();
    }
    _mapper.Map<UpdateHotelDTO, Hotel>(updateHotelDTO, hotel);

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      throw;
    }

    var response = _mapper.Map<HotelSummaryDTO>(hotel);
    return Ok(response);
  }

  // POST: api/Hotels
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPost]
  public async Task<ActionResult<HotelSummaryDTO>> PostHotel(CreateHotelDTO createHotelDTO)
  {
    if (_context.Hotels == null)
    {
        return Problem("Entity set 'HotelListingDbContext.Hotels'  is null.");
    }
    var hotel = _mapper.Map<Hotel>(createHotelDTO);
    _context.Hotels.Add(hotel);
    await _context.SaveChangesAsync();

    var response = _mapper.Map<HotelSummaryDTO>(hotel);
    
    return CreatedAtAction("GetHotel", new { id = hotel.Id }, response);
  }

  // DELETE: api/Hotels/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteHotel(int id)
  {
    if (_context.Hotels == null)
    {
      return NotFound();
    }
    var hotel = await _context.Hotels.FindAsync(id);
    if (hotel == null)
    {
      return NotFound();
    }

    _context.Hotels.Remove(hotel);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool HotelExists(int id)
  {
    return (_context.Hotels?.Any(e => e.Id == id)).GetValueOrDefault();
  }
}