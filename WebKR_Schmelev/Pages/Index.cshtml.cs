using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WebKR_Schmelev.Pages
{
    public class IndexModel : PageModel
    {
        private readonly postgresContext _postgresContext;
        private readonly mssqlContext _mssqlContext;

        public Dictionary<int, TimeSpan> TimesPostgres { get; set; }
        public Dictionary<int, TimeSpan> TimesMSSql { get; set; }

        public IndexModel(postgresContext dbPostgres, mssqlContext dbMSSql)
        {
            _postgresContext = dbPostgres;
            _mssqlContext = dbMSSql;
        }

        public void OnPostExecuteTest()
        {
            TimesPostgres = GetTimesOfWritingLargeObjects_Postgres();
            TimesMSSql = GetTimesOfWritingLargeObjects_MSSql();
        }

        public void OnGet()
        {
        }

        private Dictionary<int, TimeSpan> GetTimesOfWritingLargeObjects_Postgres()
        {
            Dictionary<int, TimeSpan> outputTimes = new Dictionary<int, TimeSpan>();
            for (int i = 1000; i < 1000000000; i *= 10)
            {
                Stopwatch stopwatch = new Stopwatch();
                Test test = new Test { Test1 = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss:fff"), Test2 = get_unique_string(i) };

                stopwatch.Start();
                _postgresContext.Tests.Add(test);
                _postgresContext.SaveChanges();
                stopwatch.Stop();

                outputTimes.Add(i, stopwatch.Elapsed);
            }
            return outputTimes;
        }

        private Dictionary<int, TimeSpan> GetTimesOfWritingLargeObjects_MSSql()
        {
            Dictionary<int, TimeSpan> outputTimes = new Dictionary<int, TimeSpan>();
            for (int i = 1000; i < 1000000000; i *= 10)
            {
                Stopwatch stopwatch = new Stopwatch();
                MssqlTest test = new MssqlTest { TestId = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss:fff"), TestText = get_unique_string(i) };

                stopwatch.Start();
                _mssqlContext.MssqlTests.Add(test);
                _mssqlContext.SaveChanges();
                stopwatch.Stop();

                outputTimes.Add(i, stopwatch.Elapsed);
            }
            return outputTimes;
        }

        string get_unique_string(int string_length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bit_count = (string_length * 6);
                var byte_count = ((bit_count + 7) / 8); // rounded up
                var bytes = new byte[byte_count];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
