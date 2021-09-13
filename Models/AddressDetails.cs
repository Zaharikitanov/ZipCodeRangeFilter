namespace PostalCodeRangeFilter.Models
{
    public class AddressDetails
    {
        public int Zip_code { get; set; }
        public string City { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public double Ne_lat { get; set; }

        public double Ne_lng { get; set; }

        public double Sw_lat { get; set; }

        public double Sw_lng { get; set; }
    }
}
