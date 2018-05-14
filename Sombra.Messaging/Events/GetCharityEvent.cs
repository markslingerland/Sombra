﻿using Sombra.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sombra.Messaging.Events
{
    public class GetCharityEvent : Event
    {
        public Guid CharityKey { get; set; }
        public string NameOwner { get; set; }
        public string NameCharity { get; set; }
        public string EmailCharity { get; set; }
        public Category Category { get; set; }
        public int KVKNumber { get; set; }
        public string IBAN { get; set; }
    }
}