using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Models
{
    
    
    public class SearchCategories
    {

        public SearchCategories()
        {

            Categories.Add("Accounting Finance", 6);
            Categories.Add("Art Media ", 17);
            Categories.Add("Automotive service", 28);
            Categories.Add("Aviation Airport", 3);
            Categories.Add("Banking", 5);
            Categories.Add("Beauty Aesthetic Medicine", 26);
            Categories.Add("Casino gambling", 682);
            Categories.Add("Cleaning", 29);
            Categories.Add("Tourism entertainment industry", 704);
            Categories.Add("Energy", 30);
            Categories.Add("Engineering", 12);
            Categories.Add(" Hotels restaurants service", 705);
            Categories.Add("Human resources", 24);
            Categories.Add("Information technology", 13);
            Categories.Add("Installation Maintenance Repair", 19);
            Categories.Add("Insurance", 23);
            Categories.Add(" Legal",25);
            Categories.Add("Logistics transportation",14);
            Categories.Add("Maritime port",31);
            Categories.Add("Marketing Advertising PR",15);
            Categories.Add("Medical",16);
            Categories.Add("Non profit non governmental organization",20);
            Categories.Add("Other",8);
            Categories.Add("Procurement",9);
            Categories.Add("Production operations",27);
            Categories.Add("Public service",7);
            Categories.Add("Quality control environment safety",11);
            Categories.Add("Real estate construction",21);
            Categories.Add("Sales",2);
            Categories.Add("Security" ,10);
            Categories.Add("Tender Competition" , 33);
            Categories.Add("Top management" , 18);
            Categories.Add("Training courses Educational programmes" , 607);
            Categories.Add("Science education trainings", 606);
        }

        public  Dictionary<string, int> Categories = new Dictionary<string, int>();

    }

   

    }
