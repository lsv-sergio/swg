using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace swg.Models {
    public class OperationModel {

        [Display(Name = "Operation")]
        public string OperationName { get; set; }

        public IEnumerable<object> Operations { get; set; }

        //[Display(Name = "Argument1")]
        //public int Arg1 { get; set; }

        //[Display(Name = "Argument1")]
        //public int Arg2 { get; set; }
    }
}