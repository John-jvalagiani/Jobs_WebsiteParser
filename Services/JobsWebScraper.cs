using AngleSharp.Html;
using HtmlAgilityPack;
using Jobs.Abstraction;
using Jobs.Controllers;
using Jobs.Dtos;
using Jobs.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Services
{
    public class JobsWebScraper : WebParser<Statement, VacancyNode>, IJobswebparser
    {

        public StatementDetails statmentdetails { get; set; } = new StatementDetails();

        public JobsWebScraper()
        {

        }

        public async Task<Dictionary<string, List<Statement>>> GetMainPageData(string url, int index)
        {
            var result = new Dictionary<string, List<Statement>>();


            Website = new HtmlWeb();


            htmldocument = Website.Load(url);



            var HtmlNodeArray = htmldocument.DocumentNode.SelectNodes("//*[@class='ann-listing']").ToArray();

            


            var VIPChildNode = HtmlNodeArray[index].Descendants()
                .Where(node => node.GetAttributeValue("class", "")
                 .Equals("ann-listing-item vip    "))
                  .Select(s => new VacancyNode
                  {

                      Titel = GetHtmlNode(s, "ann-listing-item__title"),

                      Company = GetHtmlNode(s, "ann-listing-item__customer"),



                      Date_Location = GetHtmlNode(s, "ann-listing-item__dates-location"),



                  }).ToList();


            List<Statement> VIPjobs = null;
            if (VIPChildNode.Count != 0)
            {
                VIPjobs = await Task.FromResult(CreateObjectFromNode(VIPChildNode));
                result.Add("VIP", VIPjobs);
            }

            index++;

            var ExclusiveChildNode = HtmlNodeArray[2].Descendants()
                   .Where(node => node.GetAttributeValue("class", "")
                    .Equals("ann-listing-item exclusive    "))
                     .Select(s => new VacancyNode
                     {
                         Titel = GetHtmlNode(s, "ann-listing-item__title"),

                         Company = GetHtmlNode(s, "ann-listing-item__customer"),

                         Date_Location = GetHtmlNode(s, "ann-listing-item__dates-location"),



                     }).ToList();


            

            List<Statement> Excjobs = null;
            if (ExclusiveChildNode.Count != 0)
            {
                Excjobs = CreateObjectFromNode(ExclusiveChildNode);
                result.Add("Exclusive", Excjobs);
            }


            index++;


            var p2ChildNode = HtmlNodeArray[3].Descendants()
                    .Where(node => node.GetAttributeValue("class", "")
                     .Equals("ann-listing-item p2    "))
                      .Select(s => new VacancyNode
                      {
                          Titel = GetHtmlNode(s, "ann-listing-item__title"),

                          Company = GetHtmlNode(s, "ann-listing-item__customer"),

                          Date_Location = GetHtmlNode(s, "ann-listing-item__dates-location"),


                      }).ToList();


            

            List<Statement> p2jobs = null;
            if (p2ChildNode.Count != 0)
            {
                p2jobs = CreateObjectFromNode(p2ChildNode);
                result.Add("P2", p2jobs);
            }

            index++;


            var p1ChildNode = HtmlNodeArray[4].Descendants()
                   .Where(node => node.GetAttributeValue("class", "")
                    .Equals("ann-listing-item p1    "))
                     .Select(s => new VacancyNode
                     {
                         Titel = GetHtmlNode(s, "ann-listing-item__title"),

                         Company = GetHtmlNode(s, "ann-listing-item__customer"),

                         Date_Location = GetHtmlNode(s, "ann-listing-item__dates-location"),


                     }).ToList();




            List<Statement> p1jobs = null;
            if (p1ChildNode.Count != 0)
            {
                p1jobs = await Task.FromResult(CreateObjectFromNode(p1ChildNode));
                result.Add("P1", p1jobs);
            }


            


            return result;



        }

       

        public async Task<StatementDetails> GetDetalsPageData(string url)
        {

            Website= new HtmlWeb();
            htmldocument = Website.Load(url);



            //get Statement head data

            var htmlNodehead = htmldocument.DocumentNode.SelectSingleNode("//*[@class='anncmt-details']");



            var StatementHead = htmlNodehead.Descendants()
                .Where(node => node.GetAttributeValue("class", "").Contains("left-side"))
                  .Select(s =>
                  new Customer
                  {
                      JobTitel = s.ChildNodes.FirstOrDefault(c => c.GetAttributeValue("class", "")
                       .Equals("anncmt-title")).InnerText
                       ,
                      CustomerName = s.ChildNodes.FirstOrDefault(c => c.GetAttributeValue("class", "")
                       .Equals("anncmt-customer")).InnerText,

                      CustomerPageLink = htmldocument.DocumentNode.SelectSingleNode("/html/body/div[2]/div[1]/div[1]/div[2]/div[1]/div/div[1]/div[1]/div[2]/h2/a")
                      .Attributes["href"].Value
                  }

                  ).ToArray();

            statmentdetails.Customer = StatementHead[0];


            //vacancy details
            var htmlNode = htmldocument.DocumentNode.SelectSingleNode("//*[@class='posting-details']");


            var descriptionnodes = htmlNode.ChildNodes.Where(n => n.EndNode.Name != "#text").Select(n => n).ToArray();


            var detailsfirstnode = descriptionnodes[0].ChildNodes.Where(n => n.EndNode.Name != "#text")
                  .Select(n => n.InnerText).ToArray();


            var detailssecondnode = descriptionnodes[1].ChildNodes.Where(n => n.EndNode.Name != "#text")
                  .Select(n => n.InnerText).ToArray();


            var detailsthirdnode = descriptionnodes[2].ChildNodes.Where(n => n.EndNode.Name != "#text")
                  .Select(n => n.InnerText).ToArray();



            //description 
            var Descriptionnode = htmldocument.DocumentNode.SelectSingleNode("//*[@class='firm-descr']");


            statmentdetails.Description = await Task.FromResult(Descriptionnode.InnerText);

            //get  customer  data
            var logo = htmldocument.DocumentNode.SelectSingleNode("//*[@class='company-logo']");


            ////get brand logo
            statmentdetails.BrandLogo = logo.ChildNodes["a"].ChildNodes["img"].Attributes["src"].Value;

            //get brand websait link 
            statmentdetails.BrandLink = logo.ChildNodes["a"].Attributes["href"].Value;


            return statmentdetails;
        }
        public async Task<CustomerDetails> GetCustomerPageData(string url)
        {
            Website = new HtmlWeb();


            htmldocument = Website.Load(url);



            //get customer data
            var CustomerName = await Task.FromResult(htmldocument.DocumentNode.SelectSingleNode("/html/body/div[2]/div[1]/div/div[1]/h1").InnerText);

            var CustomerLogo = await Task.FromResult(htmldocument.DocumentNode.SelectSingleNode("/html/body/div[2]/div[1]/div/div[2]/img").Attributes["src"].Value);



            //get vacancies
            var CustomerVacanciesNode = htmldocument.DocumentNode.SelectSingleNode("/html/body/div[2]/div[2]/div/table");


            //VIP
            var VIPVacancies = CustomerVacanciesNode.Descendants()
                .Where(node => node.GetAttributeValue("class", "")
                 .Equals("vip")).Select(s => new Statement()
                 {
                     Title = new Title()
                     {
                         JobTitel = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/div/div[1]/a").InnerText,
                         JobLink = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/div/div[1]/a").Attributes["href"].Value
                     },

                     Company = new Company()
                     {
                         Logo = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[3]/a/img").Attributes["src"].Value,
                         Name = "",
                     },


                     TermStart = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/span/span[1]").InnerText,

                     TermEnd = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/span/span[2]").InnerText,

                     City = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/div").InnerText


                 }).ToList();

            //exclusive
            var ExclusiveVacancies = CustomerVacanciesNode.Descendants()
               .Where(node => node.GetAttributeValue("class", "")
                .Equals("exclusive")).Select(s => new Statement()
                {

                    Title = new Title()
                    {
                        JobTitel = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/div/div[1]/a").InnerText,
                        JobLink = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/div/div[1]/a").Attributes["href"].Value
                    },

                    Company = new Company()
                    {
                        Logo = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[3]/a/img").Attributes["src"].Value,
                        Name = "",
                    },


                    TermStart = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/span/span[1]").InnerText,

                    TermEnd = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/span/span[2]").InnerText,

                    City = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/div").InnerText


                }).ToList();

            //p2Vacancies
            var p2Vacancies = CustomerVacanciesNode.Descendants()
                .Where(node => node.GetAttributeValue("class", "")
                 .Equals("p2")).Select(s => new Statement()
                 {

                     Title = new Title()
                     {
                         JobTitel = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/div/div[1]/a").InnerText,
                         JobLink = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/div/div[1]/a").Attributes["href"].Value
                     },

                     Company = new Company()
                     {
                         Logo = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[3]/a/img").Attributes["src"].Value,
                         Name = "",
                     },


                     TermStart = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/span/span[1]").InnerText,

                     TermEnd = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/span/span[2]").InnerText,

                     City = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/div").InnerText


                 }).ToList();

            //p1Vacancies
            var p1Vacancies = CustomerVacanciesNode.Descendants()
                .Where(node => node.GetAttributeValue("class", "")
                 .Equals("p1")).Select(s => new Statement()
                 {

                     Title = new Title()
                     {
                         JobTitel = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/div/div[1]/a").InnerText,
                         JobLink = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/div/div[1]/a").Attributes["href"].Value
                     },

                     Company = new Company()
                     {
                         Logo = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[3]/a/img").Attributes["src"].Value,
                         Name = "",
                     },


                     TermStart = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/span/span[1]").InnerText,

                     TermEnd = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/span/span[2]").InnerText,

                     City = s.SelectSingleNode("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]/div").InnerText


                 }).ToList();

            var customerDetails = new CustomerDetails();

            customerDetails.CustomerLogo = CustomerLogo;
            customerDetails.CustomerName = CustomerName;

            customerDetails.CustomerStatements = new Dictionary<string, List<Statement>>();
            customerDetails.CustomerStatements.Add("VIPVacancies", VIPVacancies);
            customerDetails.CustomerStatements.Add("ExclusiveVacancies", ExclusiveVacancies);
            customerDetails.CustomerStatements.Add("p2Vacancies", p2Vacancies);
            customerDetails.CustomerStatements.Add("p1Vacancies", p1Vacancies);

            return customerDetails;



        }



        protected override List<Statement> CreateObjectFromNode(List<VacancyNode> vacancies)
        {

            

            var result = vacancies.Select(n => new Statement()
            {
                



                Title = new Title()
                {
                    JobTitel = n.Titel.InnerText.Trim(),

                    JobLink = n.Titel.ChildNodes["a"]
                                     .Attributes["href"].Value
                                     .Substring(13, 6)
                },

               

             Company = new Company()
            {
                 
            
                Name = n.Company.InnerText.Trim(),
            
                Logo = n.Company.SelectSingleNode("/html/body/div[3]/div/div[3]/div[5]/div[4]/a/img").Attributes["src"].Value
           
             },

                TermStart = n.Date_Location.SelectSingleNode("/html/body/div[3]/div/div[3]/div[5]/div[7]/div[1]/span/span[1]").InnerText,

                TermEnd = n.Date_Location.SelectSingleNode("/html/body/div[3]/div/div[3]/div[5]/div[7]/div[1]/span/span[2]").InnerText,


                City = n.Date_Location.ChildNodes
                                     .FirstOrDefault(c => c.GetAttributeValue("class", "").Equals("place grey ann-listing-item__location"))
                                     .InnerText

            }).ToList();

            return result;
        }


        protected override HtmlNode GetHtmlNode(HtmlNode s, string Class)
        {
            return s.ChildNodes.FirstOrDefault(c => c.GetAttributeValue("class", "")
                                   .Equals(Class));
        }

        public async Task<List<string>> GetDataByCategory(string url)
        {

            Website = new HtmlWeb();


            htmldocument = Website.Load(url);



            var HtmlNodeArray = htmldocument.DocumentNode.SelectNodes("//*[@class='ann-listing']").ToArray();


            var s = HtmlNodeArray.Select( n => n.InnerText
            ).ToList();

            return s;
            
        }
    }

}