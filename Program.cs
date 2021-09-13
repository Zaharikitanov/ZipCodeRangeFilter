using PostalCodeRangeFilter.Extensions;
using PostalCodeRangeFilter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.IO;
using System.Linq;

namespace PostalCodeRangeFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            var workingDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName; ;
            var csv_file_path = $"{projectDirectory}\\zip_codes_de_10km.csv";

            var addresses = ProcessFile(csv_file_path);

            var filteredAddresses = RangeFilter(20, addresses);

            foreach (var address in filteredAddresses)
            {
                var startLocation = new GeoCoordinate(47.6064, 8.16714);
                var endLocation = new GeoCoordinate(address.Lat, address.Lng);
                Console.WriteLine($"Distance from Freiburg to {address.City} is {Math.Round(startLocation.GetDistanceTo(endLocation)/1000, 1)} km");
            }

        }

        private static List<AddressDetails> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .ToAddress()
                .ToList();
        }

        private static List<AddressDetails> RangeFilter(int range, List<AddressDetails> addressList)
        {
            var filteredList = new List<AddressDetails>();

            foreach (var address in addressList)
            {
                var startLocation = new GeoCoordinate(47.9908, 7.70853);
                var endLocation = new GeoCoordinate(address.Lat, address.Lng);

                if (range >= Math.Round(startLocation.GetDistanceTo(endLocation) / 1000, 1))
                {
                    filteredList.Add(address);
                }
            }

            return filteredList;
        }
    }
}
