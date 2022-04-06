using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Controllers;


namespace Pizzas.API.Services
{
    public class BD{
        public static List<Pizza> GetAll(){
            List<Pizza> ListaPizzas;
            string sp= "sp_GetAll";
            using(SqlConnection BD=basededatos.GetConnection()){
                ListaPizzas = BD.Query<Pizza>(sp, commandType:commandType.StoredProcedure).ToList();
            }
            return ListaPizzas;
        }

        public static Pizza ConsultaPizza(int Id){
            Pizza MiPizza = null;
            string sp = "sp_GetById";
            using(SqlConnection BD=basededatos.GetConnection()){
                MiPizza = BD.QueryFirstOrDefault<Pizza>(sp, new{ pId = Id}, commandType: commandType.StoredProcedures);
            }
            return MiPizza;
        }

        public static Pizza AgregarPizza(Pizza MiPizza){
            string sp = "sp_AgregarPizzas";
            int temp=0;
            using(SqlConnection BD=basededatos.GetConnection()){
                temp = BD.Execute(sp, new{ pNombre=MiPizza.Nombre,pLibreGluten=MiPizza.LibreGluten,pImporte=MiPizza.Importe,pDescripcion=MiPizza.Descripcion}, commandType: commandType.StoredProcedures);
            }
            return new Pizza();
        }

        public static Pizza Update(int Id, Pizza MiPizza){
            Pizza PizzaLocal;
            string sp = "sp_Update";
            string sp2 = "sp_GetById";
            int temp=0;

            using(SqlConnection BD=basededatos.GetConnection()){
                PizzaLocal = BD.QueryFirstOrDefault<Pizza>(sp2, new{ pId = Id}, commandType: commandType.StoredProcedures);
            }
            if(PizzaLocal==null){
                return PizzaLocal;
            }else{
                    using(SqlConnection BD=basededatos.GetConnection()){
                        temp = BD.Execute(sp, new{pId = Id, pNombre=MiPizza.Nombre,pLibreGluten=MiPizza.LibreGluten,pImporte=MiPizza.Importe,pDescripcion=MiPizza.Descripcion}, commandType: commandType.StoredProcedures);
                    }
                return new Pizza();
                }
        }

        public static Pizza Delete(int Id){
            Pizza MiPizza=null;
            string sp = "sp_DELETE";
            string sp2 = "sp_GetById";
            int temp=0;

            using(SqlConnection BD=basededatos.GetConnection()){
                MiPizza = db.QueryFirstOrDefault<Pizza>(sp2, new{ pId = Id}, commandType: commandType.StoredProcedures);
            }
            if(MiPizza==null){
                return MiPizza;
            }else{
                using(SqlConnection BD=basededatos.GetConnection()){
                temp = db.Execute(sp, new{pId = Id}, commandType: commandType.StoredProcedures);
            }
                return new Pizza();
            }
        }
    }
}