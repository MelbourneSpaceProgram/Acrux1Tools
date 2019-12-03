using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Acrux1Tools.Web.Models.Acrux1
{
    public class DecodeViewModel
    {
        [Display(Name = "Error")]
        public string Error { get; set; }

        [Display(Name = "Input Payload")]
        public string InputPayload { get; set; }

        [Display(Name = "Decoded Result")]
        public string DecodedResult { get; set; }
    }
}
