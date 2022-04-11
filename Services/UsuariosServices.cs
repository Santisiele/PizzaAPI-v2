using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Helpers;
using Pizzas.API.Controllers;


namespace Pizzas.API.Services
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
            string sp = "INSERT INTO Usuarios (Nombre,Apellido,UserName,Passwordd,Token) Values(@pNombre,@pApellido,@pUserName,@pPasswordd,@pToken)";
            int temp=0;
            using(SqlConnection BD=basededatos.GetConnection()){
                temp = BD.Execute(sp, new{ pNombre=MiUsuario.Nombre,pApellido=MiUsuario.Apellido,pUserName=MiUsuario.UserName,pPasswordd=MiUsuario.Passwordd, pToken=MiUsuario.Token});
            }
            return new Usuarios();
        }

        public static Usuarios Update(int Id, Usuarios MiUsuario){
            Usuarios UsuarioLocal;
            string sp = "UPDATE Usuarios SET  Nombre=@pNombre,Apellido=@pApellido,UserName=@pUserName,Passwordd=@pPasswordd,Token=@pToken WHERE id=@pid";
            string sp2 = "SELECT * FROM Usuarios WHERE Id=@pId";
            int temp=0;

            using(SqlConnection BD=basededatos.GetConnection()){
                UsuarioLocal = BD.QueryFirstOrDefault<Usuarios>(sp2, new{ pId = Id});
            }
            if(UsuarioLocal==null){
                return UsuarioLocal;
            }else{
                    using(SqlConnection BD=basededatos.GetConnection()){
                        temp = BD.Execute(sp, new{pId = Id, pNombre=MiUsuario.Nombre,pApellido=MiUsuario.Apellido,pUserName=MiUsuario.UserName,pPasswordd=MiUsuario.Passwordd, pToken=MiUsuario.Token});
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

        public static Usuarios GetByUserNamePassword(string UserName, string Passwordd){
            Usuarios MiUsuario = null;
            string sp = "SELECT * FROM Usuarios WHERE UserName=@pUserName, Passwordd=@pPassword";
            using(SqlConnection BD=basededatos.GetConnection()){
                MiUsuario = BD.QueryFirstOrDefault<Usuarios>(sp, new{ pUserName = UserName, pPasswordd = Passwordd});
            }
            return MiUsuario;
        }
}
}