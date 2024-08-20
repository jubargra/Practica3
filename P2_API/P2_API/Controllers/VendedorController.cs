using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using P2_API.Entities;

namespace P2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController(IConfiguration iConfiguration) : ControllerBase
    {
        [HttpPost]
        [Route("RegistrarVendedor")]
        public async Task<IActionResult> RegistrarVendedor(Vendedor ent)
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection((iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value)))
            {
                
                var result = await context.ExecuteAsync("RegistrarVendedor", 
                    new { ent.Cedula, ent.Nombre, ent.Correo }, 
                    commandType: System.Data.CommandType.StoredProcedure);

                if (result > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "Ok";
                    resp.Contenido = true;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "RESTRICCIÓN: La cédula del vendedor no se puede repetir";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }
    }
}
