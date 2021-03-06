﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ClientManagement.Backend.Logic.Models
{
   [DataContract]
   public class AspNetUserClaims
   {
      [DataMember]
      public int Id { get; set; }
      [DataMember]
      public string UserId { get; set; }
      [DataMember]
      public string ClaimType { get; set; }
      [DataMember]
      public string ClaimValue { get; set; }
   }
}