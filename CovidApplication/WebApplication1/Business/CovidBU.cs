using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using WebApplication1.Model;

namespace WebApplication1.Business
{
    public static class CovidBU
    {
        static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        static CovidBU() 
        {
            builder.DataSource = "./";
            builder.UserID = "";
            builder.Password = "";
            builder.InitialCatalog = "Covid";
        }
        //public static List<CoronaModel> GetAllData() 
        //{
        //    List<CoronaModel> listCv = new List<CoronaModel>();
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        //        {
        //            connection.Open();

        //            String sql = @"SELECT * FROM VGetAll_Covid_Data";

        //            using (SqlCommand command = new SqlCommand(sql, connection))
        //            {
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    DataTable myTable = new DataTable();
        //                    myTable.Load(reader);

        //                    foreach (DataRow row in myTable.Rows)
        //                    {
        //                        CoronaModel cr = new CoronaModel();

        //                        cr.CovidID = row.Field<int?>("CovidID");
        //                        cr.Date = row.Field<string>("Date");
        //                        cr.New_Amount = row.Field<int?>("New_Amount");
        //                        cr.Old_GetWell_Amount = row.Field<int?>("Old_GetWell_Amount");
        //                        cr.Died_Amount = row.Field<int?>("Died_Amount");
        //                        cr.SumOld = row.Field<int?>("SumOld");
        //                        cr.SumNew = row.Field<int?>("SumNew");
        //                        //cr.Update_Date = row.Field<DateTime?>("Update_Date");
        //                        //cr.Update_User = row.Field<string>("Update_User");
        //                        //cr.Create_Date = row.Field<DateTime?>("Create_Date");
        //                        //cr.Create_User = row.Field<string>("Create_User");
        //                        //cr.IsDelete = row.Field<bool>("IsDelete");
        //                        listCv.Add(cr);
        //                    }
        //                    //while (reader.Read())
        //                    //{
        //                    //    Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
        //                    //}
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    return listCv;
        //}
        public static List<CoronaModel> GetWithDate(DateTime date)
        {
            List<CoronaModel> listCv = new List<CoronaModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Get_Covid_WithDate", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Date", date));


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var xx = date.ToString("dd/MM/yyyy");
                            DataTable myTable = new DataTable();
                            myTable.Load(reader);

                            foreach (DataRow row in myTable.Rows)
                            {
                                CoronaModel cr = new CoronaModel();

                                cr.CovidID = row.Field<int?>("CovidID");
                                cr.Date = row.Field<DateTime?>("Date");
                                cr.New_Amount = row.Field<int?>("New_Amount");
                                cr.Old_GetWell_Amount = row.Field<int?>("Old_GetWell_Amount");
                                cr.Died_Amount = row.Field<int?>("Died_Amount");
                                cr.SumOld = row.Field<int?>("SumOld");
                                cr.SumNew = row.Field<int?>("SumNew");
                                //cr.Update_Date = row.Field<DateTime?>("Update_Date");
                                //cr.Update_User = row.Field<string>("Update_User");
                                //cr.Create_Date = row.Field<DateTime?>("Create_Date");
                                //cr.Create_User = row.Field<string>("Create_User");
                                //cr.IsDelete = row.Field<bool>("IsDelete");
                                listCv.Add(cr);
                            }
                            //while (reader.Read())
                            //{
                            //    Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                            //}
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return listCv;
        }
        public static void UpdateCovidData(CoronaModel param)
        {
            //var year = param.Date.Split('/')[2];
            //var day = param.Date.Split('/')[1];
            //var month = param.Date.Split('/')[0];
            //var newDate = $"{day}/{month}/{year}";
            var xx = param.Date;
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = String.Format(@"UPDATE [Covid].[dbo].[Coronavirus] 
                                                SET Date = '{0}', 
                                                    New_Amount = {1}, 
                                                    Old_GetWell_Amount = {2},
                                                    Died_Amount = {3},
                                                    Update_Date = GETDATE()                  
                                                WHERE CovidID = {4}",
                                                param.Date, 
                                                param.New_Amount, 
                                                param.Old_GetWell_Amount, 
                                                param.Died_Amount, 
                                                param.CovidID);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();                        
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteCovidData(CoronaModel param)
        {
            List<CoronaModel> listCv = new List<CoronaModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = String.Format(@"UPDATE [Covid].[dbo].[Coronavirus] 
                                                SET IsDelete = 1, 
                                                    Update_Date = GETDATE()                       
                                                WHERE CovidID = {0}",                                         
                                                param.CovidID);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void CreateCovidData(CoronaModel param)
        {
            //var year = param.Date.Split('/')[2];
            //var day = param.Date.Split('/')[1];
            //var month = param.Date.Split('/')[0];
            //var newDate = $"{day}/{month}/{year}";
            List<CoronaModel> listCv = new List<CoronaModel>();
            DataTable myTable = new DataTable();
            CoronaModel cr = new CoronaModel();
            DataRow row;
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    CultureInfo culture = new CultureInfo("en-US");
                    DateTime tempDate = Convert.ToDateTime(param.Date, culture);
                    var newDate = $"{param.Date.Value.Day}/{param.Date.Value.Month.ToString().PadLeft(2,'0')}/{param.Date.Value.Year.ToString().PadLeft(2, '0')}";
                    String sql = String.Format(@"SELECT * FROM Covid.dbo.Coronavirus where CONVERT(varchar(10), Date, 103) = '{0}'", newDate);

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {                            
                            myTable.Load(reader);

                            //foreach (DataRow row in myTable.Rows)
                            //{

                            //row = myTable.Rows[0];
                            //cr.CovidID =row.Field<int?>("CovidID");
                            //cr.Date = row.Field<string>("Date");
                            //cr.New_Amount = row.Field<int?>("New_Amount");
                            //cr.Old_GetWell_Amount = row.Field<int?>("Old_GetWell_Amount");
                            //cr.Died_Amount = row.Field<int?>("Died_Amount");
                            //cr.SumOld = row.Field<int?>("SumOld");
                            //cr.SumNew = row.Field<int?>("SumNew");
                            //cr.Update_Date = row.Field<DateTime?>("Update_Date");
                            //cr.Update_User = row.Field<string>("Update_User");
                            //cr.Create_Date = row.Field<DateTime?>("Create_Date");
                            //cr.Create_User = row.Field<string>("Create_User");
                            //cr.IsDelete = row.Field<bool>("IsDelete");
                            //    listCv.Add(cr);
                            //}
                            //while (reader.Read())
                            //{
                            //    Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                            //}
                        }
                    }
                }
                if (myTable.Rows.Count > 0)
                {
                    row = myTable.Rows[0];
                    cr.CovidID = row.Field<int?>("CovidID");
                    cr.Date = row.Field<DateTime?>("Date");
                    cr.New_Amount = param.New_Amount;
                    cr.Old_GetWell_Amount = param.Old_GetWell_Amount;
                    cr.Died_Amount = param.Died_Amount;
               
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        connection.Open();
                        String sql = String.Format(@"UPDATE [Covid].[dbo].[Coronavirus] 
                                                SET Date = '{0}', 
                                                    New_Amount = {1}, 
                                                    Old_GetWell_Amount = {2},
                                                    Died_Amount = {3},
                                                    Update_Date = GETDATE(),
                                                    IsDelete = {5}
                                                WHERE CovidID = {4}",
                                                    cr.Date,
                                                    cr.New_Amount,
                                                    cr.Old_GetWell_Amount,
                                                    cr.Died_Amount,
                                                    cr.CovidID,
                                                    0);
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        connection.Open();
                        String sql = String.Format(@"INSERT INTO [dbo].[Coronavirus]
                                                       ([Date]
                                                       ,[New_Amount]
                                                       ,[Old_GetWell_Amount]
                                                       ,[Died_Amount]
                                                       ,[Update_Date]
                                                       ,[Update_User]
                                                       ,[Create_Date]
                                                       ,[Create_User])
                                                 VALUES
                                                       ('{0}'
                                                       ,{1}
                                                       ,{2}
                                                       ,{3}
                                                       ,null
                                                       ,null
                                                       ,GETDATE()
                                                       ,null)",
                                                    param.Date,
                                                    param.New_Amount,
                                                    param.Old_GetWell_Amount,
                                                    param.Died_Amount);
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
