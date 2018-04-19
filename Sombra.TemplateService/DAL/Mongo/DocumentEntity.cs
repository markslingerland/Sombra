using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sombra.TemplateService.Templates.Mongo
{
    public abstract class DocumentEntity : IDocumentEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}