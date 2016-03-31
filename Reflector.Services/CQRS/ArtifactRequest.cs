using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflector.Services.CQRS
{
    public class ArtifactRequest :  IRequest<ArtifactEnvelope>
    {
        public int? Identifier { get; set; }
        public DateTimeOffset? BeginningRange { get; set; }
        public DateTimeOffset? EndingRange { get; set; }
        public string Category { get; set; }
    }
}
