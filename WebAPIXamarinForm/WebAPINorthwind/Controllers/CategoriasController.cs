using System.Linq;
using Contexto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPINorthwind.Controllers
{
    [Route("[controller]/[action]/")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CategoriasController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet("{pId}")]
        public string ObtenerNombreDeCategoria(int pId)
        {
            return _context.Categories.First(cat => cat.CategoryId == pId).CategoryName;
        }
    }
}