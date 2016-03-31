using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Reflector.Client.Services.Models
{
    public class ArtifactEnvelope
    {
        public ArtifactEnvelope()
        {
            this.Label = "Entity not instantiated";
        }
        public int Identifer { get; set; }
        public string Label { get; set; }
        public IEnumerable<ReportableFact> Facts { get; set; }
    }
    /*classification, label, descriptor*/
    public class ReportableFact
    {
        public string Classification { get; set; }
        public string Label { get; set; }
        public string Descriptor { get; set; }
        public DateTimeOffset ActionDateTime { get; set; }
    }


}
