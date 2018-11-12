using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AyaxApi.Models;

namespace AyaxApi.Models
{
    public class Realtor
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public long DivisionId { get; set; }
        public Division Division { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}