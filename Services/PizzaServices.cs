using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Utils;
using System.Data.SqlClient;
using Dapper;

namespace Pizzas.API.Services
{
    public class BD{
        public static List<Pizza> GetAll(){
            List<Pizza> ListaPizzas;
            string sp= "GetAll";
            using(SqlConnection db = new SqlConnection(_connectionString)){
                ListaPizzas = db.Query<Pizza>(sp,commandType: commandType.StoredProcedures).ToList();
            }
            return ListaPizzas;
        }

        public static Pizza ConsultaPizza(int Id){
            Pizza MiPizza = null;
            string sql = "SELECT * from Pizzas WHERE Id = @pId";
            using(SqlConnection db = new SqlConnection(_connectionString)){
                MiPizza = db.QueryFirstOrDefault<Pizza>(sql, new{ pId = Id});
            }
            return MiPizza;
        }

        public static Pizza AgregarPizza(Pizza MiPizza){
            string sql = "INSERT INTO Pizzas(Nombre,LibreGluten,Importe,Descripcion) VALUES(@pNombre, @pLibreGluten, @pImporte,@pDescripcion)";
            int temp=0;
            using(SqlConnection db = new SqlConnection(_connectionString)){
                temp = db.Execute(sql, new{ pNombre=MiPizza.Nombre,pLibreGluten=MiPizza.LibreGluten,pImporte=MiPizza.Importe,pDescripcion=MiPizza.Descripcion});
            }
            return new Pizza();
        }

        public static Pizza Update(int Id, Pizza MiPizza){
            Pizza PizzaLocal;
            string sql = "UPDATE Pizzas SET Nombre=@pNombre, LibreGluten=@pLibreGluten, Importe=@pImporte, Descripcion=@pDescripcion WHERE Id=@pId";
            string sql2 = "SELECT * FROM Pizzas WHERE Id=@pId";
            int temp=0;

            using(SqlConnection db = new SqlConnection(_connectionString)){
                PizzaLocal = db.QueryFirstOrDefault<Pizza>(sql2, new{ pId = Id});
            }
            if(PizzaLocal==null){
                return PizzaLocal;
            }else{
                    using(SqlConnection db = new SqlConnection(_connectionString)){
                        temp = db.Execute(sql, new{pId = Id, pNombre=MiPizza.Nombre,pLibreGluten=MiPizza.LibreGluten,pImporte=MiPizza.Importe,pDescripcion=MiPizza.Descripcion});
                    }
                return new Pizza();
                }
        }

        public static Pizza Delete(int Id){
            Pizza MiPizza=null;
            string sql = "DELETE FROM Pizzas WHERE Id=@pId";
            string sql2 = "SELECT * FROM Pizzas WHERE Id=@pId";
            int temp=0;

            using(SqlConnection db = new SqlConnection(_connectionString)){
                MiPizza = db.QueryFirstOrDefault<Pizza>(sql2, new{ pId = Id});
            }
            if(MiPizza==null){
                return MiPizza;
            }else{
                using(SqlConnection db = new SqlConnection(_connectionString)){
                temp = db.Execute(sql, new{pId = Id});
            }
                return new Pizza();
            }
        }
    }
}