using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSurveyMaui.Models
{
    public class SurveyDetails
    {
        public int id {  get; set; }
        public byte[]? attachment_1 { get; set; }
        public byte[]? attachment_2 { get; set; }
        public byte[]? attachment_3 { get; set; }
        public byte[]? attachment_4 { get; set; }

        public bool IsLoading { get; set; } = true;


    }
}
