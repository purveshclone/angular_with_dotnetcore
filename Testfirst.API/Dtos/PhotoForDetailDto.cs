using System;

namespace Testfirst.API.Dtos
{
    public class PhotoForDetailDto
    {
         public int PhotoId { get; set; }
        public string Url   { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }

    }
}