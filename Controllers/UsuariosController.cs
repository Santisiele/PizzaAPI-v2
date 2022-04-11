using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Services;
using Pizzas.API.Controllers;

namespace Pizzas.API.Controller{
    [Route("login")]
    public class UsuariosController : ControllerBase{
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Usuarios> ListaUsuarios;
            ListaUsuarios= BD2.GetAll();
            return Ok(ListaUsuarios);
        }

        [HttpGet ("{Id}")]
        public IActionResult GetById(int Id)
        {
            if(Id<=0){
                return BadRequest();
            }else{
                Usuarios MiUsuario;
                MiUsuario=BD2.ConsultaUsuario(Id);
                if(MiUsuario==null){
                    return NotFound();
                }else{
                    return Ok(MiUsuario);
                }
            }
        }

        [HttpPost]
        public IActionResult Create(Usuarios MiUsuario)
        {
            BD2.AgregarUsuario(MiUsuario);
            return Created("/API/Usuario", new{Id=MiUsuario.Id});
        }

        [HttpPut ("{id}")]
        public IActionResult Update(int Id, Usuarios MiUsuario)
        {
            if(MiUsuario.Id!=Id){
                return BadRequest();
            }else{
                if(BD2.Update(Id,MiUsuario)==null){
                    return NotFound();
                }else{
                    return Ok();
                }
            }
        }

        [HttpDelete ("{id}")]
        public IActionResult Delete(int Id)
        {
            if(Id<=0){
                return BadRequest();
            }else{
                if(BD2.Delete(Id)==null){
                    return NotFound();
                }else{
                    return Ok();
                }
            }
        }
    }
}
