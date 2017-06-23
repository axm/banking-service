using Base.Types;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Base.Serialization
{
    public class MoneySerializer : SerializerBase<Money>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Money value)
        {
            context.Writer.WriteDecimal128(value.Amount);
        }

        public override Money Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new Money(Decimal128.ToDecimal(context.Reader.ReadDecimal128()));
        }
    }
}
