using LantanaComfyAPI.Data;
using LantanaComfyAPI.Interfaces;
using LantanaComfyAPI.Models;

namespace LantanaComfyAPI.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _context;

        public ContactRepository(DataContext context)
        {
            _context = context;
        }


        public ICollection<Contact> GetContacts()
        {
            return _context.Contacts.OrderBy(c=>c.Id).ToList();
        }
        public Contact GetContact(int contactId)
        {
            return _context.Contacts.FirstOrDefault(c => c.Id == contactId);
        }

        public bool ContactExists(int contactId)
        {
            return _context.Contacts.Any(c=>c.Id  == contactId);
        }

        public bool CreateContact(Contact contact)
        {
            _context.Add(contact);
            return Save();
        }

        public bool DeleteContact(Contact contact)
        {
            _context.Remove(contact);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
