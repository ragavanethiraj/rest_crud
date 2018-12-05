using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RestApidemo_ado.Models
{
    /// <summary>
    /// This Class is used to get and assgign values to the variables for the Employee Rest Api demo 
    /// </summary>
    public class Employee
    {
        public Guid Id { get; set; }

        public int ApplicationId { get; set; }

        public string Type { get; set; }

        public string Summary { get; set; }

        public double Amount { get; set; }

        public DateTime PostingDate { get; set; }

        public string IsCleared { get; set; }

        public DateTime ClearedDate { get; set; }
    }
}