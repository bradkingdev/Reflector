using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflector.Services.CQRS.Queries
{
    public class ArtifactRequestQueryHandler : IRequestHandler<ArtifactRequest, ArtifactEnvelope>
    {
        private readonly ArtifactDataContext _db;

        public ArtifactRequestQueryHandler()
        {
            _db = new ArtifactDataContext(ConnectionStringResolver.Resolve());
        }
        public ArtifactEnvelope Handle(ArtifactRequest parms)
        {
            return getEnvelope(parms);

        }

        private ArtifactEnvelope getEnvelope(ArtifactRequest parms)
        {
            var customer = _db.ArtifactHeaders.FirstOrDefault(x => x.Number == parms.Identifier);

            if (customer == null)
                return new ArtifactEnvelope();

            return queryEnvelopeArtifacts(parms, customer);
        }

        private ArtifactEnvelope queryEnvelopeArtifacts(ArtifactRequest parms, ArtifactHeader artifactHeader)
        {
            ArtifactEnvelope envelope = makeEnvelope(artifactHeader);
            var artifacts = _db.Artifacts.AsQueryable();

            if(parms.Identifier.HasValue)
                artifacts = artifacts.Where(x => x.ArtifactHeaderId == artifactHeader.Id);

            if (!string.IsNullOrEmpty(parms.Category))
                artifacts = artifacts.Where(x => x.Classification == parms.Category);

            if (parms.BeginningRange.HasValue)
                artifacts = artifacts.Where(x => x.DateTimeLogged >= parms.BeginningRange.Value);

            if (parms.EndingRange.HasValue)
                artifacts = artifacts.Where(x => x.DateTimeLogged <= parms.EndingRange.Value);

            if (artifacts.Any())
                envelope.Facts = artifacts.Select(x => new ReportableFact
                {
                    ActionDateTime = x.DateTimeLogged,
                    Classification = x.Classification,
                    Descriptor = x.Fact,
                    Label = x.Label
                });

            return envelope;
        }

        private static ArtifactEnvelope makeEnvelope(ArtifactHeader artifactHeader)
        {
            return new ArtifactEnvelope
            {
                Label = artifactHeader.Name,
                Identifier = artifactHeader.Number
            };
        }

    }
}
