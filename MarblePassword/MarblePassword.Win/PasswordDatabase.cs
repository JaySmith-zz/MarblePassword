using System;
using System.Collections.Generic;

namespace MarblePassword.Win
{
    public class PasswordDatabase
    {
       

        public PasswordDatabase()
        {
            Items = new List<Entry>();
            Created = DateTime.Now;
        }

        private int ItemCount
        {
            get
            {
                return Items.Count;
            }
        }

        public string Password { get; set; }

        public string Filename { get; set; }

        public List<Entry> Items { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

    }
}
