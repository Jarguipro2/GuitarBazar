using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace GuitarBazar.Data
{
    public class ActionLogger
    {
        public string LogFilePath { get; set; }
        static private readonly Mutex mutex = new Mutex();
        public ActionLogger(string filePath)
        {
            LogFilePath = filePath;
        }

        public void Add(string computerName, string actionName)
        {
            mutex.WaitOne();
            StreamWriter sw = new StreamWriter(LogFilePath,true);
            sw.WriteLine(DateTime.Now.ToString()+" PC:" + computerName + " Action:" + actionName);
            sw.Dispose();
            mutex.ReleaseMutex();
        }

        public List<string> Dump()
        {
            mutex.WaitOne();
            StreamReader sr = new StreamReader(LogFilePath);
            List<string> lines = new List<string>();
            while (!sr.EndOfStream)
            {
                lines.Add(sr.ReadLine());
            }
            sr.Dispose();
            mutex.ReleaseMutex();
            return lines;

        }
    }
    public class GuitarBazarContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public ActionLogger log = new ActionLogger(HttpContext.Current.Server.MapPath("~/App_Data/log.txt"));
        public GuitarBazarContext() : base("name=GuitarBazarContext") { }
       

        public System.Data.Entity.DbSet<GuitarBazar.Models.Guitar> Guitars { get; set; }

        public System.Data.Entity.DbSet<GuitarBazar.Models.Seller> Sellers { get; set; }

        public SelectList SellersToSelectList(bool noEmptyEntry = false)
        {
            var items = Sellers.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name }).ToList();
            if (!noEmptyEntry)
                items.Insert(0, new SelectListItem { Value = "", Text = "" });
            return new SelectList(items, "Value", "Text");
        }
    }
}
