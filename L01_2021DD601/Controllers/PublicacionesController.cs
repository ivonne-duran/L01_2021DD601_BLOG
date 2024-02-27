using L01_2021DD601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021DD601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        private readonly PublicacionesContext _publicacionesContext;

        public PublicacionesController(PublicacionesContext publicacionesContext)
        {
            _publicacionesContext = publicacionesContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Publicaciones> listadoPublicaciones = (from e in _publicacionesContext.publicaciones
                                                        select e
                                                        ).ToList();

            if(listadoPublicaciones.Count == 0) { 
                return NotFound();
            }

            return Ok(listadoPublicaciones);
        }

        [HttpGet]
        [Route("FindByUsuario/{usuarioId}")]
        public IActionResult Get(int usuarioId) {
            List<Publicaciones> listadoPublicaciones = (from e in _publicacionesContext.publicaciones
                                                        where e.usuarioId == usuarioId
                                                        select e
                                                        ).ToList();

            if (listadoPublicaciones.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPublicaciones);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] Publicaciones publicaciones) {
            try
            {
                _publicacionesContext.publicaciones.Add(publicaciones);
                _publicacionesContext.SaveChanges();
                return Ok(publicaciones);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult actualizarPublicacion(int id, [FromBody] Publicaciones publicacionModificar)
        {
            Publicaciones? publicacionActual = (from e in _publicacionesContext.publicaciones
                                       where e.publicacionId == id
                                       select e
                ).FirstOrDefault();

            if (publicacionActual == null)
            {
                return NotFound();
            }

            publicacionActual.titulo = publicacionModificar.titulo;
            publicacionActual.descripcion = publicacionModificar.descripcion;

            _publicacionesContext.Entry(publicacionActual).State = EntityState.Modified;
            _publicacionesContext.SaveChanges();

            return Ok(publicacionModificar);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult eliminarPublicacion(int id)
        {
            Publicaciones? publicacion = (from e in _publicacionesContext.publicaciones
                                 where e.publicacionId == id
                                 select e
                ).FirstOrDefault();

            if (publicacion == null)
            {
                return NotFound();
            }

            _publicacionesContext.publicaciones.Attach(publicacion);
            _publicacionesContext.publicaciones.Remove(publicacion);
            _publicacionesContext.SaveChanges();

            return Ok(publicacion);
        }

    }
}
