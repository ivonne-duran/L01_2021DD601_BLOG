using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021DD601.Models;

namespace L01_2021DD601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosContext _usuariosContext;

        public UsuariosController(UsuariosContext usuariosContext)
        {
            _usuariosContext = usuariosContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Usuarios> listadoUsuarios = (from e in _usuariosContext.usuarios
                                              select e
                                              ).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }

        [HttpGet]
        [Route("FindByName/{filtro}")]
        public IActionResult obtenerPorNombre(string filtro) {
            List<Usuarios> usuarios = (from e in _usuariosContext.usuarios
                                  where e.nombre.Contains(filtro)
                                  select e).ToList();
            if(usuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        [HttpGet]
        [Route("FindByApellido/{filtro}")]
        public IActionResult obtenerPorApellido(string filtro)
        {
            List<Usuarios> usuarios = (from e in _usuariosContext.usuarios
                                  where e.apellido.Contains(filtro)
                                  select e).ToList();
            if (usuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        [HttpGet]
        [Route("FindByRol/{filtro}")]
        public IActionResult obtenerPorRol(int filtro)
        {
            List<Usuarios> usuarios = (from e in _usuariosContext.usuarios
                                  where e.rolId == filtro
                                  select e).ToList();

            if (usuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult guardarUsuario([FromBody] Usuarios usuarios)
        {
            try
            {
                _usuariosContext.usuarios.Add(usuarios);
                _usuariosContext.SaveChanges();
                return Ok(usuarios);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult actualizarUsuario(int id, [FromBody] Usuarios usuarioModificar)
        {
            Usuarios? usuarioActual = ( from e in _usuariosContext.usuarios
                                  where e.usuarioID == id
                                  select e
                ).FirstOrDefault();

            if(usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.nombreUsuario = usuarioModificar.nombreUsuario;
            usuarioActual.nombre = usuarioModificar.nombre;
            usuarioActual.rolId = usuarioModificar.rolId;

            _usuariosContext.Entry(usuarioActual).State = EntityState.Modified;
            _usuariosContext.SaveChanges();

            return Ok(usuarioModificar);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult eliminarUsuario(int id)
        {
            Usuarios? usuario = ( from e in _usuariosContext.usuarios
                                  where e.usuarioID == id
                                  select e
                ).FirstOrDefault();

            if(usuario == null)
            {
                return NotFound();
            }

            _usuariosContext.usuarios.Attach(usuario);
            _usuariosContext.usuarios.Remove(usuario);
            _usuariosContext.SaveChanges();

            return Ok(usuario);
        }

        
    }
}
