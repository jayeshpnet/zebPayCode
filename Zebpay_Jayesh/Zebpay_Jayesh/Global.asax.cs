using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Zebpay_Jayesh
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.Timers.Timer timer = new System.Timers.Timer();
            //Set interval of repeated execution in millisecond
            timer.Interval =Convert.ToDouble(System.Web.Configuration.WebConfigurationManager.AppSettings["periodicalTimer"].ToString());// set 1 min.;
            //set name of the method to be called
            timer.Elapsed += GetLatestCurrency_Rate;
            timer.Start();
            Application.Add("timer", timer);
        }

        static async void GetLatestCurrency_Rate(object sender, System.Timers.ElapsedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                // New code:
                client.BaseAddress = new Uri("http://zebpaytest.azurewebsites.net/");
               
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Values/");
            }
        }
       
    }
}