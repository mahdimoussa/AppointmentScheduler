using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Library.Models
{
    public class WorkingHour
    {
        [Key]
        public int Id { get; set; }
        public Nullable<DateTime> FromHour { get; set; }
        public Nullable<DateTime> ToHour { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public bool IsDeleted { get; set; }




    }
}
