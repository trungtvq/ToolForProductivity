using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace GetResponds
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://linksvip.net/GetLinkFs");
            //request.AllowAutoRedirect = true;
            //request.CookieContainer = cookies;
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            using (var client = new WebClientEx())
            {
                //var response1 = client.DownloadString("https://linksvip.net/GetLinkFs");
                //System.Diagnostics.Debug.WriteLine(response1);
                var data = new NameValueCollection
            {
                { "link", "https://www.fshare.vn/file/NCDJDZKJPRC8?token=1545546551" },
                { "pass", "undefined" },
                { "hash", "0yoHgvl4CsyS8tIu49oEknnEYjvvenuJ13Rljnt0ony4CVuMMq28riOnvqoMk42eMtv1.2522heijy9TD4ur" },
                 { "captcha", "" },
            };
                client.Headers["X-Requested-With"] = "XMLHttpRequest";
                //client.Headers["Content-Type"] = "application/x-www-form-urlencoded; charset=UTF-8";
                client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/18.17763";
                var response2 = client.UploadValues("https://linksvip.net/GetLinkFs", data);
                System.Diagnostics.Debug.WriteLine("ra lua mi");
                System.Diagnostics.Debug.WriteLine(response2);

                Object bytes = response2.GetValue(1);

                Console.WriteLine(Encoding.Default.GetString(response2));
            }
            Console.ReadKey();
        }


        public class WebClientEx : WebClient
        {
            private CookieContainer _cookieContainer = new CookieContainer();

            protected override WebRequest GetWebRequest(Uri address)
            {
                Console.WriteLine("Hello World!");
                CookieContainer cookies = new CookieContainer();

                Cookie PHPSESSID = new Cookie();
                PHPSESSID.Name = "PHPSESSID";
                PHPSESSID.Value = "498mbig7ne977p1jsqn8647fs1";

                Cookie __atuvc = new Cookie();
                __atuvc.Name = "__atuvc";
                __atuvc.Value = "5%7C52";

                Cookie user = new Cookie();
                user.Name = "user";
                user.Value = "1513752%40hcmut.edu.vn";

                Cookie _gid = new Cookie();
                _gid.Name = "_gid";
                _gid.Value = "GA1.2.1753196334.1545543236";

                Cookie _ga = new Cookie();
                _ga.Name = "_ga";
                _ga.Value = "GA1.2.1150109288.1545543236";

                Cookie __cfduid = new Cookie();
                __cfduid.Name = "__cfduid";
                __cfduid.Value = "d72cfbf92b614f79da5519565b63fa7f71545543228";

                Cookie pass = new Cookie();
                pass.Name = "pass";
                pass.Value = "25d55ad283aa400af464c76d713c07ad";

                PHPSESSID.Domain = "linksvip.net";
                __atuvc.Domain = "linksvip.net";
                user.Domain = "linksvip.net";
                _gid.Domain = "linksvip.net";
                _ga.Domain = "linksvip.net";
                __cfduid.Domain = "linksvip.net";
                pass.Domain = "linksvip.net";
                cookies.Add(PHPSESSID);
                cookies.Add(__atuvc);
                cookies.Add(user);
                cookies.Add(_gid);
                cookies.Add(_ga);
                cookies.Add(__cfduid);
                cookies.Add(pass);
                _cookieContainer = cookies;
                WebRequest request = base.GetWebRequest(address);
                if (request is HttpWebRequest)
                {
                    (request as HttpWebRequest).CookieContainer = _cookieContainer;
                }
                return request;
            }
        }

      
    }
}
