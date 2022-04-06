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
using Pizzas.API.Helpers;
using Usuarios.API.Services;


namespace Pizzas.API.Services
{
    public class BD{
        public static List<Pizza> GetAll(){
            List<Pizza> ListaPizzas;
            string sp= "SELECT * FROM Pizzas";
            using(SqlConnection BD=basededatos.GetConnection()){
                ListaPizzas = BD.Query<Pizza>(sp).ToList();
            }
            return ListaPizzas;
        }


        public static Pizza ConsultaPizza(int Id){
            Pizza MiPizza = null;
            string sp = "SELECT * FROM Pizzas WHERE Id=@pId";
            using(SqlConnection BD=basededatos.GetConnection()){
                MiPizza = BD.QueryFirstOrDefault<Pizza>(sp, new{ pId = Id});
            }
            return MiPizza;
        }

        public static Pizza AgregarPizza(Pizza MiPizza){
            string sp = "INSERT INTO Pizzas (Nombre,LibreGluten,Importe,Descripcion) Values(@pNombre, @pLibreGluten, @pImporte, @pDescripcion)";
            int temp=0;
            using(SqlConnection BD=basededatos.GetConnection()){
                temp = BD.Execute(sp, new{ pNombre=MiPizza.Nombre,pLibreGluten=MiPizza.LibreGluten,pImporte=MiPizza.Importe,pDescripcion=MiPizza.Descripcion});
            }
            return new Pizza();
        }

        public static Pizza Update(int Id, Pizza MiPizza){
            Pizza PizzaLocal;
            string sp = "UPDATE Pizzas SET  Nombre=@pNombre, LibreGluten=@pLibreGluten, Importe=@pImporte, Descripcion=@pDescripcion WHERE id=@pid";
            string sp2 = "SELECT * FROM Pizzas WHERE Id=@pId";
            int temp=0;

            using(SqlConnection BD=basededatos.GetConnection()){
                PizzaLocal = BD.QueryFirstOrDefault<Pizza>(sp2, new{ pId = Id});
            }
            if(PizzaLocal==null){
                return PizzaLocal;
            }else{
                    using(SqlConnection BD=basededatos.GetConnection()){
                        temp = BD.Execute(sp, new{pId = Id, pNombre=MiPizza.Nombre,pLibreGluten=MiPizza.LibreGluten,pImporte=MiPizza.Importe,pDescripcion=MiPizza.Descripcion});
                    }
                return new Pizza();
                }
        }

        public static Pizza Delete(int Id){
            Pizza MiPizza=null;
            string sp = "DELETE FROM Pizzas WHERE Id=@pid";
            string sp2 = "SELECT * FROM Pizzas WHERE Id=@pId";
            int temp=0;

            using(SqlConnection BD=basededatos.GetConnection()){
                MiPizza = BD.QueryFirstOrDefault<Pizza>(sp2, new{ pId = Id});
            }
            if(MiPizza==null){
                return MiPizza;
            }else{
                using(SqlConnection BD=basededatos.GetConnection()){
                temp = BD.Execute(sp, new{pId = Id});
            }
                return new Pizza();
            }
        }
    }
}