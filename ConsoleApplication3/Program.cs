using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Security.Application;

namespace ConsoleApplication3
{
    class Program
    {
        private const string XssExample = "<img onerror='alert(\"could run arbitrary JS here\")' src=bogus><script>alert(document.cookie)</script>";
        private static readonly Stopwatch RunStopwatch = new Stopwatch();

        static void Main(string[] args)
        {
            var samples = new List<string>();
            var encoded = new List<string>();

            // try to reduce any latency related to lazy init
            HttpUtility.HtmlEncode(XssExample);
            Microsoft.Security.Application.Encoder.HtmlEncode(XssExample);

            for (int i = 0; i < 100000; i++)
            {
                samples.Add(XssExample + RandomString(10));
            }

            RunStopwatch.Start();
            foreach (var sample in samples)
            {
                encoded.Add(HttpUtility.HtmlEncode(sample));
            }
            RunStopwatch.Stop();
            Console.WriteLine("({0} ms)", RunStopwatch.ElapsedMilliseconds);

            encoded.Clear();

            RunStopwatch.Start();
            foreach (var sample in samples)
            {
                encoded.Add(Microsoft.Security.Application.Encoder.HtmlEncode(sample));
            }
            RunStopwatch.Stop();
            Console.WriteLine("({0} ms)", RunStopwatch.ElapsedMilliseconds);

            Console.ReadKey();
        }

        private static Random random = new Random((int)DateTime.Now.Ticks);
        private static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
