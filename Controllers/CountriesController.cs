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
public class CountriesController : ControllerBase
{
  private readonly HotelListingDbContext _context;
  private readonly IMapper _mapper;

  public CountriesController(HotelListingDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  // GET: api/Countries
  [HttpGet]
  public async Task<ActionResult<IEnumerable<CountrySummaryDTO>>> GetCountries()
  {
    if (_context.Countries == null)
    {
      return NotFound();
    }
    var countries = await _context.Countries.ToListAsync();
    var records = _mapper.Map<List<CountrySummaryDTO>>(countries);
    return Ok(records);
  }

  // GET: api/Countries/5
  [HttpGet("{id}")]
  public async Task<ActionResult<CountryDTO>> GetCountry(int id)
  {
    if (_context.Countries == null)
    {
      return NotFound();
    }
    var country = await _context.Countries.Include(q => q.Hotels).FirstOrDefaultAsync(q => q.Id == id);

    if (country == null)
    {
      return NotFound();
    }

    var result = _mapper.Map<CountryDTO>(country);

    return Ok(result);
  }

  // PUT: api/Countries/5
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPut("{id}")]
  public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateCountryDTO)
  {
    if (_context.Countries == null)
    {
      return NotFound();
    }
    var country = await _context.Countries.FindAsync(id);
    if (country == null)
    {
      return NotFound();
    }
    _mapper.Map<UpdateCountryDTO, Country>(updateCountryDTO, country);

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      throw;
    }

    return NoContent();
  }

  // POST: api/Countries
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPost]
  public async Task<ActionResult<CreateCountryDTO>> PostCountry(CreateCountryDTO createCountryDTO)
  {
    if (_context.Countries == null)
    {
      return Problem("Entity set 'HotelListingDbContext.Countries'  is null.");
    }
    var country = _mapper.Map<Country>(createCountryDTO);

    _context.Countries.Add(country);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetCountry", new { id = country.Id }, country);
  }

  // DELETE: api/Countries/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteCountry(int id)
  {
      if (_context.Countries == null)
      {
          return NotFound();
      }
      var country = await _context.Countries.FindAsync(id);
      if (country == null)
      {
          return NotFound();
      }

      _context.Countries.Remove(country);
      await _context.SaveChangesAsync();

      return NoContent();
  }

  private bool CountryExists(int id)
  {
      return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
  }
}

