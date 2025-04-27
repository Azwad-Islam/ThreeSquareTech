using FormSubmissionApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // ✅ Required for ToListAsync

[Route("api/[controller]")]
[ApiController]
public class FormDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public FormDataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostData([FromBody] FormData data)
    {
        _context.FormDatas.Add(data);
        await _context.SaveChangesAsync();
        return Ok(data);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormData>>> GetData()
    {
        return await _context.FormDatas.ToListAsync();
    }
}
