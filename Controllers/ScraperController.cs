using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AngleSharp.Html.Parser;
using System.Net.Http;
using HtmlAgilityPack;
using System.Text.Json;
using Newtonsoft.Json;
using AngleSharp.Common;
using AngleSharp.Dom;
using System.Web;
using Jobs.Services;
using Jobs.Abstraction;
using Jobs.Models;
using Jobs.Exeptions;

namespace Jobs.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class ScraperController : Controller
    {
        public  string websiteUrl= "https://www.hr.ge/";

        private readonly IJobswebparser _scraper;

        public ScraperController(IJobswebparser scraper)
        {
            _scraper = scraper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllDataAsync()
        {

            try
            {
                var result = await _scraper.GetMainPageData(websiteUrl, 1);
           


                if (result == null)
                    return NotFound();

            return Ok(result);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        [HttpGet("Id")] 
        public async Task<IActionResult> GetByIdAsync(string Id)
        {
            string announcement = "announcement/";

            if (Id == null)
                throw new ArgumentNullException($"Argument was null");


            var result = await _scraper.GetDetalsPageData(websiteUrl+ announcement+ Id);


            if (result == null)
             throw  new  StatementNotFoundExeption(Id);

            return Ok(result);
           
        }


        [HttpGet("Category")]
        public async Task<IActionResult> GetByCategoryAsync(string category)
        {
            if (category == null)
                throw new ArgumentNullException("Argument was null");


             var search = new SearchCategories();

            var value= search.Categories.FirstOrDefault(e => e.Key.ToLower() == category.ToLower()).Value;

            if (value == 0)
               throw new  CategoryDoesNotExitsException(category);


            string searchbycategory = "search-posting?category=";

            

                var result = await _scraper.GetDataByCategory(websiteUrl + searchbycategory + value);

                

                return Ok(result);
           
        }


        [HttpGet("Customer")]
        public async Task<IActionResult> GetCustomerAsync(string CustomerLink)
        {
            if (CustomerLink == null)
                throw new ArgumentNullException("Argument was null");

            var result = await _scraper.GetCustomerPageData(CustomerLink);

            
            if (result == null)
                return NotFound();

            return Ok(result);



        }
    }
}
