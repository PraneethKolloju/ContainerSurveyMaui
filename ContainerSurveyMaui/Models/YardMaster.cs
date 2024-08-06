using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContainerSurveyMaui.Models
{
    public class YardMaster
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("portID")]
        public int? PortID { get; set; }

        [JsonPropertyName("yard")]
        public string? Yard { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("createdBy")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("createdOn")]
        public DateTime? CreatedOn { get; set; }

    }
}
