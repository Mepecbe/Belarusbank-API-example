using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Newtonsoft.Json;


namespace sss
{
    public static class BelarusbankExchangers
    {
        public class Exchanger
        {
            public string USD_in;
            public string USD_out;
            public string EUR_in;
            public string EUR_out;
            public string RUB_in;
            public string RUB_out;
            public string GBP_in;
            public string GBP_out;
            public string CAD_in;
            public string CAD_out;
            public string PLN_in;
            public string PLN_out;
            public string UAH_in;
            public string UAH_out;
            public string SEK_in;
            public string SEK_out;
            public string CHF_in;
            public string CHF_OUT;
            public string USD_EUR_in;
            public string USD_EUR_out;
            public string USD_RUB_in;
            public string USD_RUB_out;
            public string RUB_EUR_in;
            public string RUB_EUR_out;
            public string JPY_in;
            public string JPY_out;
            public string CNY_in;
            public string CNY_out;
            public string CZK_in;
            public string CZK_out;
            public string NOK_in;
            public string NOK_out;
            public string filial_id;
            public string sap_id;
            public string info_worktime;
            public string street_type;
            public string street;
            public string filials_text;
            public string home_number;
            public string name;
            public string name_type;
        }

        public static List<Exchanger> GetCityExchangers(string City)
        {
            HttpWebRequest Req = HttpWebRequest.CreateHttp($"https://belarusbank.by/api/kursExchange?city={City}");
            HttpWebResponse Resp = (HttpWebResponse)Req.GetResponse();

            StreamReader reader = new StreamReader(Resp.GetResponseStream());

            string Data = reader.ReadToEnd();

            reader.Close();

            Regex regex = new Regex(@"{([\s\S]+?)}");

            List<Exchanger> ex = new List<Exchanger>();

            //Parse JSON 
            foreach (Match match in regex.Matches(Data))
            {
                ex.Add(JsonConvert.DeserializeObject<Exchanger>(match.Value));
            }
            
            return ex;
        }
    }

    


    class Program
    {
        static void Main(string[] args)
        {
            List<BelarusbankExchangers.Exchanger> exchangers = BelarusbankExchangers.GetCityExchangers("Минск");

            //Print
            foreach(BelarusbankExchangers.Exchanger ex in exchangers)
            {
                Console.WriteLine($"Address {ex.name_type} {ex.name}  {ex.street_type}  {ex.street}  {ex.home_number} +  : {ex.filials_text}; Id {ex.filial_id}  ");
            }
        }
    }
}