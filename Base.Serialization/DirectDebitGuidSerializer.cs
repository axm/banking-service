using Accounts.Domain;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;

namespace Base.Serialization
{
    public class DirectDebitGuidSerializer : SerializerBase<DirectDebitGuid>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DirectDebitGuid value)
        {
            context.Writer.WriteString(value.Id.ToString());
        }

        public override DirectDebitGuid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new DirectDebitGuid(new Guid(context.Reader.ReadString()));
        }
    }
}
