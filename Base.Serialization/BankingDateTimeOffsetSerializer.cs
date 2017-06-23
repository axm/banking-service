using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;

namespace Base.Serialization
{
    public class BankingDateTimeOffsetSerializer : SerializerBase<DateTimeOffset>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTimeOffset value)
        {
            context.Writer.WriteString(value.ToString());
        }

        public override DateTimeOffset Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return DateTimeOffset.Parse(context.Reader.ReadString());
        }
    }
}
