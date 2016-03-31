using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Reflector.Services.CQRS
{
    public class ArtifactEnvelope : IAsyncRequest
    {
        public ArtifactEnvelope()
        {
            this.Label = "Customer not instantiated";
        }
        public int Identifier { get; set; }
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
 