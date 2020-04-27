using System;

namespace Testfirst.API.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string Url   { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public Users Users { get; set; }
        public int UsersId { get; set; }
    }
}