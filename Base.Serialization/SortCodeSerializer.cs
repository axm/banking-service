using Base.Types;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;

namespace Base.Serialization
{
    public class SortCodeSerializer : SerializerBase<SortCode>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, SortCode value)
        {
            context.Writer.WriteString(value.Code);
        }

        public override SortCode Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new SortCode(context.Reader.ReadString());
        }
    }
}
