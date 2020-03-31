using System;
using System.Collections.Generic;

namespace CoreWebAPIApp.Models
{
    public partial class TblEvent
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventTime { get; set; }
        public string EventLocation { get; set; }
        public string EventMembers { get; set; }
        public string EventOrganizer { get; set; }
    }
}
