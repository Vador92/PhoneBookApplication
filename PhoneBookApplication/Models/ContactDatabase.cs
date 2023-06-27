using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PhoneBookApplication.Models
{
    public class ContactDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        //passing in file path so that the program can access the data
        public ContactDatabase(string dbPath)
        {
            //knows where to find the database
            _database = new SQLiteAsyncConnection(dbPath);
            //creates the table for contacts if not already existing
            _database.CreateTableAsync<Contact>();
        }

        //method for getting contacts, utilize this when doing on appear command for front end
        public Task<List<Contact>> GetContactsAsync()
        {
            return _database.Table<Contact>().ToListAsync();
        }

        //saving the contact into the table, and the int is the primary key
        public Task<int> SaveContactAsync(Contact contact)
        {
            return _database.InsertAsync(contact);
        }

        //in theory would (not tested with this program yet) delete the contact selected from the table
        public Task<int> DeleteContactAsync(Contact contact)
        {
            return _database.DeleteAsync(contact);
        }


        //update the current contact in the detailview
        public Task<int> UpdateContactAsync(Contact contact)
        {
            return _database.UpdateAsync(contact);
        }

        //gets one contact based on id
        public Task<Contact> GetContactAsync(Guid id)
        {
            return _database.GetAsync<Contact>(id);
        }
    }
}
