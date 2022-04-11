using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Usuarios.API.Models;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Helpers;
using Usuarios.API.Services;
using Pizzas.API.Controllers;


namespace Usuarios.API.Services
{
    public class BD2{
        public static List<Usuarios> GetAll(){
            List<Usuarios> ListaUsuarios;
            string sp= "SELECT * FROM Usuarios";
            using(SqlConnection BD=basededatos.GetConnection()){
                ListaUsuarios = BD.Query<Usuarios>(sp).ToList();
            }
            return ListaUsuarios;
        }


        public static Usuarios ConsultaUsuario(int Id){
            Usuarios MiUsuario = null;
            string sp = "SELECT * FROM Usuarios WHERE Id=@pId";
            using(SqlConnection BD=basededatos.GetConnection()){
                MiUsuario = BD.QueryFirstOrDefault<Usuarios>(sp, new{ pId = Id});
            }
            return MiUsuario;
        }

        public static Usuarios AgregarUsuario(Usuarios MiUsuario){
            string sp = "INSERT INTO Usuarios (Nombre,LibreGluten,Importe,Descripcion) Values(@pNombre, @pLibreGluten, @pImporte, @pDescripcion)";
            int temp=0;
            using(SqlConnection BD=basededatos.GetConnection()){
                temp = BD.Execute(sp, new{ pNombre=MiUsuario.Nombre,pLibreGluten=MiUsuario.LibreGluten,pImporte=MiUsuario.Importe,pDescripcion=MiUsuario.Descripcion});
            }
            return new Usuarios();
        }

        public static Usuarios Update(int Id, Usuarios MiUsuario){
            Usuarios UsuarioLocal;
            string sp = "UPDATE Usuarios SET  Nombre=@pNombre, LibreGluten=@pLibreGluten, Importe=@pImporte, Descripcion=@pDescripcion WHERE id=@pid";
            string sp2 = "SELECT * FROM Usuarios WHERE Id=@pId";
            int temp=0;

            using(SqlConnection BD=basededatos.GetConnection()){
                UsuarioLocal = BD.QueryFirstOrDefault<Usuarios>(sp2, new{ pId = Id});
            }
            if(UsuarioLocal==null){
                return UsuarioLocal;
            }else{
                    using(SqlConnection BD=basededatos.GetConnection()){
                        temp = BD.Execute(sp, new{pId = Id, pNombre=MiUsuario.Nombre,pLibreGluten=MiUsuario.LibreGluten,pImporte=MiUsuario.Importe,pDescripcion=MiUsuario.Descripcion});
                    }
                return new Usuarios();
                }
        }

        public static Usuarios Delete(int Id){
            Usuarios MiUsuario=null;
            string sp = "DELETE FROM Usuarios WHERE Id=@pid";
            string sp2 = "SELECT * FROM Usuarios WHERE Id=@pId";
            int temp=0;

            using(SqlConnection BD=basededatos.GetConnection()){
                MiUsuario = BD.QueryFirstOrDefault<Usuarios>(sp2, new{ pId = Id});
            }
            if(MiUsuario==null){
                return MiUsuario;
            }else{
                using(SqlConnection BD=basededatos.GetConnection()){
                temp = BD.Execute(sp, new{pId = Id});
            }
                return new Usuarios();
            }
        }
    }
}