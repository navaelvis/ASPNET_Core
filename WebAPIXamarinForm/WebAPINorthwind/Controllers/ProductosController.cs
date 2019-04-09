using System.Collections.Generic;
using System.Linq;
using Contexto;
using Microsoft.AspNetCore.Mvc;
using Modelo;

namespace WebAPINorthwind.Controllers
{
    [Route("[controller]/[action]/")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public ProductosController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet("{pId}")]
        public List<Products> ObtenerProductosPorIdCategoria(int pId)
        {
            var _productos = from _producto in _context.Products
                             where _producto.CategoryId == pId
                             select _producto;

            return _productos.ToList();
        }
    }
}