using L01_2021DD601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021DD601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly ComentariosContext _comentariosContext;

        public ComentariosController(ComentariosContext comentariosContext)
        {
            _comentariosContext = comentariosContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Comentarios> listadoComentarios = (from e in _comentariosContext.comentarios
                                              select e
                                              ).ToList();

            if (listadoComentarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoComentarios);
        }

        [HttpGet]
        [Route("GetByPublicacion/{id}")]
        public IActionResult Get(int id)
        {
            List<Comentarios> comentarios = (from e in _comentariosContext.comentarios
                                             where e.publicacionId == id
                                             select e
                                             ).ToList();

            if(comentarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(comentarios);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult guardarUsuario([FromBody] Comentarios comentarios)
        {
            try
            {
                _comentariosContext.comentarios.Add(comentarios);
                _comentariosContext.SaveChanges();
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult actualizarUsuario(int id, [FromBody] Comentarios comentarioModificar)
        {
            Comentarios? comentarioActual = (from e in _comentariosContext.comentarios
                                       where e.comentarioId == id
                                       select e
                ).FirstOrDefault();

            if (comentarioActual == null)
            {
                return NotFound();
            }

            comentarioActual.comentario = comentarioModificar.comentario;
            

            _comentariosContext.Entry(comentarioActual).State = EntityState.Modified;
            _comentariosContext.SaveChanges();

            return Ok(comentarioModificar);
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult eliminarComentario(int id)
        {
            Comentarios? usuario = (from e in _comentariosContext.comentarios
                                 where e.comentarioId == id
                                 select e
                ).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }

            _comentariosContext.comentarios.Attach(usuario);
            _comentariosContext.comentarios.Remove(usuario);
            _comentariosContext.SaveChanges();

            return Ok(usuario);
        }

    }
}
