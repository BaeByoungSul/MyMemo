using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyNotes.Models
{
    public class MyNote
    {
        [PrimaryKey, AutoIncrement]
        public int NoteID { get; set; }
        public string Subject { get; set; }
        public string NoteText { get; set; }
        public DateTime LastUpdate { get; set; }


    }
}
