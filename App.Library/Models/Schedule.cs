using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Library.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public string Weekdays { get; set; }
        public string NumberOfAppointments { get; set; }
        public DateTime StartingTime { get; set; }
        public string BreakBetweenApps { get; set; }
        public string AppointmentDuration { get; set; }

    }
}
