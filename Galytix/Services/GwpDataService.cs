using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting; // For IWebHostEnvironment
using Galytix.WebApi.Models;  // Replace with your actual namespace

namespace Galytix.WebApi.Services 
{
    public class GwpDataService
    {
        private readonly List<GwpRecord> _records;

        // IWebHostEnvironment helps find the project's root folder
        public GwpDataService(IWebHostEnvironment env)
        {
            // Build the path to the CSV file in the Data folder
            var filePath = Path.Combine(env.ContentRootPath, "Data", "gwpByCountry.csv");
            _records = LoadCsvData(filePath);
        }

        private List<GwpRecord> LoadCsvData(string filePath)
        {
            var records = new List<GwpRecord>();
            // Read all lines from the CSV file
            var lines = File.ReadAllLines(filePath);

            // Assuming the first line is a header, skip it
            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                // Check if we have at least 3 columns
                if (parts.Length >= 3)
                {
                    // Use TryParse for safer conversion
                    if (double.TryParse(parts[2].Trim(), out double value))
                    {
                        records.Add(new GwpRecord
                        {
                            Country = parts[0].Trim(),
                            Lob = parts[1].Trim(),
                            Value = value
                        });
                    }
                }
            }
            return records;
        }

        public Task<CountryGwpResponse> ComputeAveragesAsync(string country, List<string> lobs)
        {
            return Task.Run(() =>
            {
                var response = new CountryGwpResponse();

                // For each requested lob, compute the average
                foreach (var lob in lobs)
                {
                    // Filter records by matching country and lob (case-insensitive)
                    var filtered = _records.Where(r =>
                        r.Country.Equals(country, StringComparison.OrdinalIgnoreCase) &&
                        r.Lob.Equals(lob, StringComparison.OrdinalIgnoreCase));

                    // If records exist, compute the average; otherwise, return 0
                    response[lob] = filtered.Any()
                        ? filtered.Average(r => r.Value)
                        : 0;
                }

                return response;
            });
        }
    }
}
