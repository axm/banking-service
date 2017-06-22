using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Base.Types
{
    [DataContract]
    public abstract class TypedGuid : IEquatable<TypedGuid>
    {
        [DataMember]
        public readonly Guid Id;

        public TypedGuid() : this(Guid.NewGuid())
        {
        }

        public TypedGuid(string id) : this(new Guid(id)) { }

        public TypedGuid(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            if(!(obj is TypedGuid))
            {
                return false;
            }

            if(ReferenceEquals(this, obj))
            {
                return true;
            }

            return ((TypedGuid)obj).Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static implicit operator Guid(TypedGuid id)
        {
            return id.Id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public bool Equals(TypedGuid other)
        {
            if(!ReferenceEquals(this, other))
            {
                return false;
            }

            return Id == other.Id;
        }
    }
}
