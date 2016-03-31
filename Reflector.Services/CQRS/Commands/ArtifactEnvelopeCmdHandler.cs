using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflector.Services.CQRS.Commands
{
    public class ArtifactEnvelopeCmdHandler : AsyncRequestHandler<ArtifactEnvelope>
    {
        private readonly ArtifactDataContext _db;
        public ArtifactEnvelopeCmdHandler()
        {
            _db = new ArtifactDataContext(ConnectionStringResolver.Resolve());
        }
        protected override async Task HandleCore(ArtifactEnvelope msg)
        {
            await processMsg(msg);
        }

        internal Task<int> processMsg(ArtifactEnvelope msg)
        {
            var artifactHeader = _db.ArtifactHeaders.FirstOrDefault(x => x.Number == msg.Identifier);
            var artifacts = mapFacts(msg.Facts).ToList();

            if(artifactHeader == null)
            {
                artifactHeader = mapHeader(msg, artifacts);
                _db.ArtifactHeaders.Add(artifactHeader);
            }
            else
            {
                foreach (var artifact in artifacts)
                    artifactHeader.Artifacts.Add(artifact);
            }

            return _db.SaveChangesAsync();
        }

        internal ArtifactHeader mapHeader(ArtifactEnvelope msg, List<Artifact> artifacts)
        {
            return new ArtifactHeader
            {
                Number = msg.Identifier,
                Name = msg.Label,
                Artifacts = artifacts
            };
        }

        internal IEnumerable<Artifact> mapFacts(IEnumerable<ReportableFact> facts)
        {
            foreach (var fact in facts)
                yield return new Artifact
                {
                    Classification = fact.Classification,
                    Fact = fact.Descriptor,
                    Label = fact.Label,
                    DateTimeLogged = DateTimeOffset.Now
                };
        }

    }
}
