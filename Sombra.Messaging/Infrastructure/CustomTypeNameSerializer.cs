using System;
using System.Collections.Concurrent;
using System.Reflection;
using EasyNetQ;

namespace Sombra.Messaging.Infrastructure
{
    public class CustomTypeNameSerializer : ITypeNameSerializer
    {
        private readonly string _messagesAssemblyName;
        private readonly string __messagesAssemblySubstitute = "[a]";
        private readonly ConcurrentDictionary<string, Type> _deserializedTypes = new ConcurrentDictionary<string, Type>();
        private readonly ConcurrentDictionary<Type, string> _serializedTypes = new ConcurrentDictionary<Type, string>();

        public CustomTypeNameSerializer()
        {
            _messagesAssemblyName = typeof(Message).Assembly.GetName().Name;
        }

        public Type DeSerialize(string typeName)
        {
            return _deserializedTypes.GetOrAdd(typeName, t =>
            {
                var type = Type.GetType($"{t.Replace(__messagesAssemblySubstitute, _messagesAssemblyName)}, {_messagesAssemblyName}");
                if (type == null)
                {
                    var nameParts = t.Split(':');
                    if (nameParts.Length != 2)
                        throw new EasyNetQException("type name {0}, is not a valid EasyNetQ type name. Expected Type:Assembly", t);

                    type = Type.GetType(nameParts[0] + ", " + nameParts[1]);
                    if (type == null)
                        throw new EasyNetQException("Cannot find type {0}", t);
                }
                return type;
            });
        }

        public string Serialize(Type type)
        {
            return _serializedTypes.GetOrAdd(type, t =>
            {
                var typeName = t.FullName + ":" + t.GetTypeInfo().Assembly.GetName().Name;
                if (t.Assembly.GetName().Name == _messagesAssemblyName)
                    typeName = t.FullName.Replace(_messagesAssemblyName, __messagesAssemblySubstitute);

                if (typeName.Length > 255)
                    throw new EasyNetQException("The serialized name of type '{0}' exceeds the AMQP " +
                                                "maximum short string length of 255 characters.", t.Name);
                return typeName;
            });
        }
    }
}