using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Business;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CovidController : ControllerBase
    {

        [HttpGet("/GetALL")]
        public List<CoronaModel> GetALL()
        {
            //List<CoronaModel> listCv = new List<CoronaModel> ();
            //try
            //{

            //    listCv = CovidBU.GetAllData();
            //}
            //catch (SqlException e)
            //{
            //    throw new Exception(e.Message);
            //}
            ////Console.WriteLine("\nDone. Press enter.");
            ////Console.ReadLine();

            return new List<CoronaModel>();
        }

        [HttpGet("/GetWithDate")]
        public List<CoronaModel> GetWithDate(DateTime date)
        {
            List<CoronaModel> listCv = new List<CoronaModel>();
            try
            {

                listCv = CovidBU.GetWithDate(date);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            //Console.WriteLine("\nDone. Press enter.");
            //Console.ReadLine();

            return listCv;
        }
        [HttpPost("/UpdateCovidData")]
        public List<CoronaModel> UpdateCovidData(CoronaModel param)
        {
            List<CoronaModel> listCv = new List<CoronaModel>();
            try
            {
                CultureInfo culture = new CultureInfo("en-US");
                DateTime tempDate = Convert.ToDateTime(param.Date, culture);
                var newDate = $"{param.Date.Value.Day}/{param.Date.Value.Month}/{param.Date.Value.Year}";
                param.Date = Convert.ToDateTime(newDate);
 
                CovidBU.UpdateCovidData(param);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            //Console.WriteLine("\nDone. Press enter.");
            //Console.ReadLine();

            return listCv;
        }
        [HttpPost("/DeleteCovidData")]
        public List<CoronaModel> DeleteCovidData(CoronaModel param)
        {
            List<CoronaModel> listCv = new List<CoronaModel>();
            try
            {

                CovidBU.DeleteCovidData(param);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            //Console.WriteLine("\nDone. Press enter.");
            //Console.ReadLine();

            return listCv;
        }
        [HttpPost("/CreateCovidData")]
        public List<CoronaModel> CreateCovidData(CoronaModel param)
        {
            List<CoronaModel> listCv = new List<CoronaModel>();
            try
            {

                CovidBU.CreateCovidData(param);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            //Console.WriteLine("\nDone. Press enter.");
            //Console.ReadLine();

            return listCv;
        }
    }
}
