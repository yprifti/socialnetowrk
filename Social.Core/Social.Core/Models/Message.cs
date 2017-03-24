using Social.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Models
{
    public class Message:IMessage
    {
        public IUser User { get; set; }
        public string MessageText { get; set; }
        public DateTime? Posted { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} ({2})", User.UserName, MessageText, Ago());
        }
        public string CompactToString() {
            return string.Format("{0} ({1})", MessageText, Ago());
        }

        private string Ago() {
            if (!Posted.HasValue) return "Not Posted";
            var timespan = (DateTime.Now - Posted.Value);
            if (timespan.TotalSeconds < 60) return string.Format("{0} {1} ago", (int)timespan.TotalSeconds, (int)timespan.TotalSeconds ==1? "second":"seconds");
            if (timespan.TotalMinutes < 60) return string.Format("{0} {1} ago", (int)timespan.TotalMinutes, (int)timespan.TotalMinutes ==1? "minute":"minutes");
            if (timespan.TotalHours < 24) return string.Format("{0} {1} ago", (int)timespan.TotalHours, (int)timespan.TotalHours==1? "hour":"hours");
            return string.Format("{0} {1} ago", (int)timespan.TotalDays, (int)timespan.TotalDays==1?"day":"days");
        }
    }
}
