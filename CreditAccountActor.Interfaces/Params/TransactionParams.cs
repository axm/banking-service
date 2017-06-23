﻿using Banking.Domain;
using Base.Types;
using Credits.Domain;
using System;
using System.Runtime.Serialization;

namespace CreditAccountActor.Interfaces.Params
{
    [DataContract]
    public class TransactionParams
    {
        [DataMember]
        public CreditAccountGuid Id { get; set; }
        [DataMember]
        public Money Amount { get; set; }
        [DataMember]
        public DateTimeOffset Timestamp { get; set; }
    }
}