using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace MovieTicketAPI.Models
{
    public class MovieAPI
    {
        [Key]
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string Descriptions { get; set; }
        public string MovieType { get; set; }
        public string Languages { get; set; }

    }
}
