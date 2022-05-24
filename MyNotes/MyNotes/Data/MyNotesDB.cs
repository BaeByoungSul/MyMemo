using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyNotes.Models;
using SQLite;

namespace MyNotes.Data
{
    public class MyNotesDB
    {
        readonly SQLiteAsyncConnection _connection;
        public MyNotesDB( string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<MyNote>().Wait();
        }
        
        public Task<List<MyNote>> GetMyNotesAsync()
        {
            return _connection.Table<MyNote>().OrderByDescending(x=>x.LastUpdate).ToListAsync();
            
        }
    
        public Task<MyNote> GetMyNoteAsync(int id)
        {
            return _connection.Table<MyNote>()
                                .Where(x => x.NoteID == id)
                                .FirstOrDefaultAsync();

        }

        public Task<int> SaveMyNoteAsync(MyNote note)
        {
            if (note.NoteID != 0)
            {
                return _connection.UpdateAsync(note);
            }
            else
            {
                return _connection.InsertAsync(note);
            }
        }

        public Task<int> DeleteMyNoteAsync(MyNote note)
        {
            return _connection.DeleteAsync(note);
        }
    }
}
