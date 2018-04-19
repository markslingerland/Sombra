using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sombra.TemplateService.Templates.Mongo
{
    public interface IDocumentEntity
    {
        [BsonId]
        ObjectId Id { get; set; }
    }
}