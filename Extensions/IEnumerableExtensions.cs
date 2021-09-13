﻿using PostalCodeRangeFilter.Models;
using System.Collections.Generic;

namespace PostalCodeRangeFilter.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<AddressDetails> ToAddress(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                yield return new AddressDetails
                {
                    Zip_code = int.Parse(columns[0]),
                    City = columns[1],
                    Lat = double.Parse(columns[2]),
                    Lng = double.Parse(columns[3]),
                    Ne_lat = double.Parse(columns[4]),
                    Ne_lng = double.Parse(columns[5]),
                    Sw_lat = double.Parse(columns[6]),
                    Sw_lng = double.Parse(columns[7])
                };
            }
        }
    }
}
