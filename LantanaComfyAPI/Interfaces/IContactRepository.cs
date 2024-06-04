using LantanaComfyAPI.Models;

namespace LantanaComfyAPI.Interfaces
{
    public interface IContactRepository
    {
        ICollection<Contact> GetContacts();
        Contact GetContact(int contactId);
        bool ContactExists(int contactId);
        bool CreateContact(Contact contact);
        bool DeleteContact(Contact contact);
        bool Save();
    }
}
