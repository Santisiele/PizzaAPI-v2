using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Usuarios.API.Models;
using Usuarios.API.Utils;
using System.Data.SqlClient;
using Dapper;

namespace Pizzas.API.Services
{
    public class BD2{
        public static List<Usuarios> GetAll(){
            List<Pizza> ListaPizzas;
            string sp= "sp_GetAll";
            using(SqlConnection BD=BD.GetConnection()){
                ListaPizzas = db.Query<Pizza>(sp,commandType: commandType.StoredProcedures).ToList();
            }
            return ListaPizzas;
        }

        public static Usuarios GetByUserNamePassword(string UserName, string Passwordd){
            Usuarios MiUsuario = null;
            string sp = "sp_GetByUserNamePassword";
            using(SqlConnection BD=BD.GetConnection()){
                MiUsuario = db.QueryFirstOrDefault<Usuarios>(sp, new{ pUserName = UserName, pPassword=Passwordd}, commandType: commandType.StoredProcedures);
            }
            return MiUsuario;
        }

        public static Pizza AgregarPizza(Pizza MiPizza){
            string sp = "sp_GetByUserNamePassword";
            int temp=0;
            using(SqlConnection BD=BD.GetConnection()){
                temp = db.Execute(sp, new{ pNombre=MiPizza.Nombre,pLibreGluten=MiPizza.LibreGluten,pImporte=MiPizza.Importe,pDescripcion=MiPizza.Descripcion}, commandType: commandType.StoredProcedures);
            }
            return new Pizza();
        }

        public static Pizza Update(int Id, Pizza MiPizza){
            Pizza PizzaLocal;
            string sp = "sp_Update";
            string sp2 = "sp_GetById";
            int temp=0;

            using(SqlConnection BD=BD.GetConnection()){
                PizzaLocal = db.QueryFirstOrDefault<Pizza>(sp2, new{ pId = Id}, commandType: commandType.StoredProcedures);
            }
            if(PizzaLocal==null){
                return PizzaLocal;
            }else{
                    using(SqlConnection BD=BD.GetConnection()){
                        temp = db.Execute(sp, new{pId = Id, pNombre=MiPizza.Nombre,pLibreGluten=MiPizza.LibreGluten,pImporte=MiPizza.Importe,pDescripcion=MiPizza.Descripcion}, commandType: commandType.StoredProcedures);
                    }
                return new Pizza();
                }
        }

        public static Pizza Delete(int Id){
            Pizza MiPizza=null;
            string sp = "sp_DELETE";
            string sp2 = "sp_GetById";
            int temp=0;

            using(SqlConnection BD=BD.GetConnection()){
                MiPizza = db.QueryFirstOrDefault<Pizza>(sp2, new{ pId = Id}, commandType: commandType.StoredProcedures);
            }
            if(MiPizza==null){
                return MiPizza;
            }else{
                using(SqlConnection BD=BD.GetConnection()){
                temp = db.Execute(sp, new{pId = Id}, commandType: commandType.StoredProcedures);
            }
                return new Pizza();
            }
        }
    }
}