using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarblePassword.Win
{
    class Entry
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
