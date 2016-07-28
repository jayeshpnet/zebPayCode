using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using RestSharp;

namespace Zebpay_Jayesh.Controllers
{
    public class RateRequest
    {
        public string currency;
        public double amount;
    }

    public class RateRequestReturn
    {
        public string SourceCurrency;
        public decimal ConversionRate;
        public decimal Amount;
        public decimal Total;
        public double returncode;
        public string err;
        public string timestamp;       
    }

    public class ValuesController : ApiController
    {
        // POST api/values
        [HttpPost]
        public System.Web.Mvc.JsonResult RateConversation([FromBody]string value)
        {
            //Get Latest Covnersation rate from DB using Entity Framework
            RateRequest obj = JsonConvert.DeserializeObject<RateRequest>(value);
            if (obj != null)
                return FetchRateConversation(obj.currency, (decimal)obj.amount);
            else
                return new System.Web.Mvc.JsonResult();
        }

        [NonActionAttribute]
        public System.Web.Mvc.JsonResult FetchRateConversation(string currency = "USD", decimal amount = 1)
        {
            string result = string.Empty;
            //Get Latest Covnersation rate from DB using Entity Framework
            using (CurrencyConversionEntities context = new CurrencyConversionEntities())
            {
                Currency objResult = context.Currencies.Where(x => x.fromcurrency == currency.ToUpper()).FirstOrDefault();
                if (objResult != null)
                {                   
                   // result = amount + " " + currency + " = " + (objResult.rate * amount) + " " + objResult.tocurrency;
                    var model = new RateRequestReturn()
                    {
                        SourceCurrency = currency,
                        ConversionRate = Math.Round(objResult.rate,2),
                        Amount = amount,
                        Total = Math.Round(objResult.rate * amount,2),
                        returncode = 1,
                        err = "success",
                        timestamp = DateTime.UtcNow.Ticks.ToString()                       
                    };
                    return new System.Web.Mvc.JsonResult() { Data = model, JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet };

                }
            }

            var modelReturn = new RateRequestReturn()
            {
                SourceCurrency = currency,
                returncode = 1,
                err = "error",
                timestamp = DateTime.UtcNow.Ticks.ToString()               
            };
            return new System.Web.Mvc.JsonResult() { Data = modelReturn, JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet };

        }

        [NonActionAttribute]
        public decimal GetRateFromGoogleUsingRestSharp(string from, string to, double amount)
        {
            string url = string.Format("https://www.google.com/finance/converter?a={2}&from={0}&to={1}", from.ToUpper(), to.ToUpper(), amount);
            var client = new RestClient(url);
            var request = new RestRequest("", Method.POST);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            request.AddParameter("a", amount.ToString());
            request.AddParameter("from", from.ToUpper());
            request.AddParameter("to", to.ToUpper());

            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            // execute the request
            IRestResponse RestResponse = client.Execute(request);
            string response = RestResponse.Content; // raw content as string

            response = response.Substring(response.IndexOf("<span class=bld>"), 26);
            response = response.Replace("INR", "");
            response = response.Replace("IN", "");
            response = response.Replace("I", "");
            response = response.Replace("<span class=bld>", "");
            response = response.Trim();
            //Regex regex = new Regex("rhs: \\\"(\\d*.\\d*)");
            decimal rate = System.Convert.ToDecimal(response);
            return rate;
        }

        [NonActionAttribute]
        public decimal GetRateFromGoogle(string from, string to, double amount)
        {
            WebClient web = new WebClient();
            string url = string.Format("https://www.google.com/finance/converter?a={2}&from={0}&to={1}", from.ToUpper(), to.ToUpper(), amount);
            string response = web.DownloadString(url);
            response = response.Substring(response.IndexOf("<span class=bld>"), 26);
            response = response.Replace("INR", "");
            response = response.Replace("IN", "");
            response = response.Replace("I", "");
            response = response.Replace("<span class=bld>", "");
            response = response.Trim();
            //Regex regex = new Regex("rhs: \\\"(\\d*.\\d*)");
            decimal rate = System.Convert.ToDecimal(response);
            return rate;
        }

        [HttpGet]
        [System.Web.Mvc.OutputCache(Duration = 2000)]
        public string GetRate()
        {
            string[] Currencies = { "USD", "GBP", "AUD", "EUR", "CAD", "SGD" };
            ///Get data from google and store to DB
            ///
            decimal cRate = 0;
            Currency objCurr = null;
            using (CurrencyConversionEntities context = new CurrencyConversionEntities())
            {
                foreach (string curr in Currencies)
                {
                    //cRate = GetRateFromGoogle(curr, "INR", 1);
                    cRate = GetRateFromGoogleUsingRestSharp(curr, "INR", 1);
                    objCurr = new Currency();
                    objCurr.fromcurrency = curr;
                    objCurr.tocurrency = "INR";
                    objCurr.rate = cRate;
                    
                    List<Currency> objDup = context.Currencies.Where(x => x.fromcurrency == curr && x.tocurrency == "INR").ToList();
                    if (objDup != null && objDup.Count > 0)
                    {
                        objCurr.id = objDup[0].id;
                    }
                    else
                    {
                        objCurr.id = 0;
                        context.Currencies.Add(objCurr);
                    }
                };
                context.SaveChanges();
            }
            return "value";
        }        
    }
}