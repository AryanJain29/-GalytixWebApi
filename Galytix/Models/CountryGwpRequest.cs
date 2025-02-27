using System.Collections.Generic;

namespace Galytix.WebApi.Models
{
    public class CountryGwpRequest
    {
        public string Country { get; set; }
        public List<string> Lob { get; set; }
    }
}
