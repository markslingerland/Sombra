using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sombra.Infrastructure.DAL.Mongo
{
    public interface IDocumentEntity
    {
        [BsonId]
        ObjectId Id { get; set; }
    }
}