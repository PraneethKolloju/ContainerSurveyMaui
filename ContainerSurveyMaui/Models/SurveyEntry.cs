using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSurveyMaui.Models
{
    public class SurveyEntry
    {
        public string? Port { get; set; }
        public string? Yard { get; set; }
        public string? Shipping_line { get; set; }
        public string? Container_No { get; set; }
        public string? Container_Selection { get; set; }
        public byte[]? Attachment_1 { get; set; }
        public byte[]? Attachment_2 { get; set; }
        public byte[]? Attachment_3 { get; set; }
        public byte[]? Attachment_4 { get; set; }
        public string? Remarks { get; set; }
        public string? Location { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
