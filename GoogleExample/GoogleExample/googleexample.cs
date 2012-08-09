using System;
using System.Net;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace Dela.Mono.Examples
{
   class GoogleSearch
   {
      static void Main(string[] args)
      {
         Console.Write("Please enter string to search google for: ");
         string searchString = HttpUtility.UrlEncode(Console.ReadLine());
         
         Console.WriteLine();
         Console.Write("Please wait...\r");

         // Query google.
         WebClient webClient = new WebClient();
         byte[] response =
              webClient.DownloadData("http://www.google.com/search?&num=5&q="
              + searchString);

         // Check response for results
         string regex = "g><a\\shref=\"?(?<URL>[^\">]*)[^>]*>(?<Name>[^<]*)";
         MatchCollection matches
                  = Regex.Matches(Encoding.ASCII.GetString(response), regex);

         // Output results
         Console.WriteLine("===== Results =====");
         if(matches.Count > 0)
         {
            foreach(Match match in matches)
            {
               Console.WriteLine(HttpUtility.HtmlDecode(
                  match.Groups["Name"].Value) + 
                  " - " + match.Groups["URL"].Value);
            }
         }
         else
         {
            Console.WriteLine("0 results found");
         }
      }
   }
}