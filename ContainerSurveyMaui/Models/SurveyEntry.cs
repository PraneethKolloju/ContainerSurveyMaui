using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSurveyMaui.Models
{
    public class SurveyEntry
    {
        public int? id { get; set; } 
        public string? port { get; set; }
        public string? yard { get; set; }
        public string? shipping_line { get; set; }
        public string? container_No { get; set; }
        public string? container_Selection { get; set; }
        public byte[]? attachment_1 { get; set; }
        public byte[]? attachment_2 { get; set; }
        public byte[]? attachment_3 { get; set; }
        public byte[]? attachment_4 { get; set; }
        public string? remarks { get; set; }
        public string? location { get; set; }

        public DateTime? createdOn { get; set; }
    }
}
