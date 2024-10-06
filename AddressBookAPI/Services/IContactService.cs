using AddressBookAPI.DTOs;
using AddressBookAPI.Models;

namespace AddressBookAPI.Services
{
    public interface IContactService
    {
        Task<ContactDTO> CreateContact(ContactDTO newContact);
        Task<bool> DeleteContact(int id);
        Task<List<ContactDTO>> GetAllContacts();
        Task<ContactPageDTO> GetContactsByPage(int pageIndex, int pageSize, string? sortColumn, string? sortOrder, string? filterValue);
        Task<ContactDTO> GetContactById(int id);
        Task<ContactDTO> UpdateContact(ContactDTO updateContact);
    }
}
