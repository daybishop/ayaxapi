using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AyaxApi.Models
{
    public class Division
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}