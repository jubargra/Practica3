using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using P2_API.Entities;
using System.Data;

namespace P2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController(IConfiguration iConfiguration) : ControllerBase
    {
        [HttpPost]
        [Route("RegistrarVehiculo")]
        public async Task<IActionResult> RegistrarVehiculo(Vehiculo ent)
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection((iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value)))
            {

                var result = await context.ExecuteAsync("RegistrarVehiculo",
                    new { ent.Marca, ent.Modelo, ent.Color, ent.Precio, ent.NombreVendedor },
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
                    resp.Mensaje = "RESTRICCIÓN: El nombre del vendedor no existe en los registros o la marca del vehiculo ya existe";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        [HttpGet]
        [Route("ConsultarVehiculos")]
        public async Task<IActionResult> ConsultarVehiculos()
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryAsync<Vehiculo>("ConsultarVehiculos", new { }, commandType: CommandType.StoredProcedure);

                if (result.Count() > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = result;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No hay usuarios registrados en este momento";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

    }
}
