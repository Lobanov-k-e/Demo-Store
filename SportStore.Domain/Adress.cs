using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;

namespace SportStore.Domain
{
    public class Adress
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }

        public override string ToString()
        {
            return Line1 + Line2 + Line3 + Country + State + City + Zip;
        }
    }

    
}
