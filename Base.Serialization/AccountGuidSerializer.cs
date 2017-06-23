using Accounts.Domain;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using Base.Types;

namespace Base.Serialization
{
    public class AccountGuidSerializer : SerializerBase<AccountGuid>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, AccountGuid value)
        {
            context.Writer.WriteString(value.Id.ToString());
        }

        public override AccountGuid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new AccountGuid(new Guid(context.Reader.ReadString()));
        }
    }
}
