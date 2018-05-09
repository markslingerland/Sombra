using System;
using System.Collections.Concurrent;
using System.Reflection;
using EasyNetQ;

namespace Sombra.Messaging.Infrastructure
{
    public class CustomTypeNameSerializer : ITypeNameSerializer
    {
        private readonly string _messagesAssemblyName;
        private readonly string _messagesNamespacePrefix;
        private readonly ConcurrentDictionary<string, Type> _deserializedTypes = new ConcurrentDictionary<string, Type>();
        private readonly ConcurrentDictionary<Type, string> _serializedTypes = new ConcurrentDictionary<Type, string>();

        public CustomTypeNameSerializer()
        {
            var baseMessageType = typeof(Message);
            _messagesAssemblyName = baseMessageType.Assembly.GetName().Name;
            _messagesNamespacePrefix = $"{baseMessageType.Namespace}.";
        }

        public Type DeSerialize(string typeName)
        {
            return _deserializedTypes.GetOrAdd(typeName, t =>
            {
                var type = Type.GetType($"{_messagesNamespacePrefix}{t}, {_messagesNamespacePrefix}");
                if (type == null)
                {
                    throw new EasyNetQException("Cannot find type {0}", t);
                }
                return type;
            });
        }

        public string Serialize(Type type)
        {
            return _serializedTypes.GetOrAdd(type, t =>
            {
                if (!t.FullName.StartsWith(_messagesNamespacePrefix))
                    throw new EasyNetQException($"The type must be in {_messagesNamespacePrefix}.");

                if (t.Assembly.GetName().Name != _messagesAssemblyName)
                    throw new EasyNetQException($"The type must be in {_messagesAssemblyName}.");

                var typeName = t.FullName.Replace(_messagesNamespacePrefix, "");
                if (typeName.Length > 255)
                    throw new EasyNetQException("The serialized name of type '{0}' exceeds the AMQP " +
                                                "maximum short string length of 255 characters.", t.Name);
                return typeName;
            });
        }
    }
}
