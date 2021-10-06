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
        private static string workingDirectory = Environment.CurrentDirectory;
        private static string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        private static string csv_file_path = $"{projectDirectory}\\zip_codes_de_10km.csv";

        static void Main(string[] args)
        {
            var addresses = GetAddressesFromFileAsync();
            Console.WriteLine($"Total addresses are: {addresses.Count()}");

            var addressesInRange = AddressesInRange("30171", 15);
            Console.WriteLine($"Total addresses in range are: {addressesInRange.Count()}");
        }

        public static AddressDetails GetOneAsync(string zipCode)
        {
            var addresses = GetAddressesFromFileAsync();

            return addresses.SingleOrDefault(z => z.Zip_code == zipCode);
        }

        private static List<AddressDetails> GetAddressesFromFileAsync()
        {
            var lines = File.ReadAllLines(csv_file_path);

            return lines
                .Skip(1)
                .Where(line => line.Length > 1)
                .ToAddress()
                .OrderBy(x => x.City)
                .ToList();
        }

        private static List<AddressDetails> AddressesInRange(string zipCode, int range)
        {
            var addresses = GetAddressesFromFileAsync();
            var originZipCode = addresses.SingleOrDefault(z => z.Zip_code == zipCode);

            var filteredList = new List<AddressDetails>();

            foreach (var address in addresses)
            {
                var startLocation = new GeoCoordinate(originZipCode.Lat, originZipCode.Lng);
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
