using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class testDAL
    {
        string cadena = "Server=JONNYJSC-DELL; Initial Catalog=test; User ID=sa; Password=admin123; Trusted_Connection=false";

        public async Task<test> CreateMetodo(test t)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(cadena))
                {
                    SqlCommand cmd = cnn.CreateCommand();
                    cnn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CreateTest";
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = t.Name;
                    Task<int> mcResult = cmd.ExecuteNonQueryAsync();
                    await Task.WhenAll(mcResult);
                    return t;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<test> EditMetodo(int id, test t)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(cadena))
                {
                    SqlCommand cmd = cnn.CreateCommand();
                    cnn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "EditarTest";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = t.Name;
                    Task<int> mcResult = cmd.ExecuteNonQueryAsync();
                    await Task.WhenAll(mcResult);
                    return t;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> DeleteMetodo(int id)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(cadena))
                {
                    //SqlConnection cnn = new SqlConnection(cadena);
                    SqlCommand cmd = cnn.CreateCommand();
                    cnn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DeleteTest";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    Task<int> mcResult = cmd.ExecuteNonQueryAsync();
                    await Task.WhenAll(mcResult);
                    return id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
