using AddressBookAPI.DTOs;
using AddressBookAPI.Mappers;
using AddressBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBookAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly MyMapper _mymapper;
        private readonly dbcontext _dbcontext;

        public ContactService(MyMapper mymapper, dbcontext context)
        {
            _mymapper = mymapper;
            _dbcontext = context;
        }

        public async Task<List<ContactDTO>> GetAllContacts()
        {
            var contacts = await _dbcontext.Contacts.ToListAsync();

            return _mymapper.ToContactDTOList(contacts);
        }

        public async Task<ContactPageDTO> GetContactsByPage(int pageIndex, int pageSize, string? sortColumn, string? sortOrder, string? filterValue)
        {
            //var contacts = await _dbcontext.Contacts.ToListAsync();

            var query = _dbcontext.Contacts.Select(contacts => contacts);

            if (!string.IsNullOrEmpty(filterValue))
            {
                query = query.Where(c => c.FirstName.ToLower().Contains(filterValue.ToLower()) || c.LastName.ToLower().Contains(filterValue.ToLower()));
            }

            switch (sortColumn)
            {
                case "firstName":
                    query = sortOrder == "desc" ? query.OrderByDescending(c => c.FirstName) : query.OrderBy(c => c.FirstName);
                    break;
                case "lastName":
                    query = sortOrder == "desc" ? query.OrderByDescending(c => c.LastName) : query.OrderBy(c => c.LastName);
                    break;
                case "phoneNumber":
                    query = sortOrder == "desc" ? query.OrderByDescending(c => c.Phone) : query.OrderBy(c => c.Phone);
                    break;
                case "address":
                    query = sortOrder == "desc" ? query.OrderByDescending(c => c.Address) : query.OrderBy(c => c.Address);
                    break;
                default:
                    query = query.OrderBy(c => c.Id);
                    break;
            }

            // query to get data for one page
            var onePageContactsQuery = query.Skip(pageSize*pageIndex).Take(pageSize);
            // query is executed
            var pageContactsResult = await onePageContactsQuery.ToListAsync();
            // query is executed - getting total number of records
            var totalCount = await query.CountAsync();

            // create object ContactPageDTO
            var contactPageDTO = new ContactPageDTO();
            contactPageDTO.ContactsTotalCount = totalCount;
            contactPageDTO.PageContacts = _mymapper.ToContactDTOList(pageContactsResult);

            return contactPageDTO;
        }


        public async Task<ContactDTO> GetContactById(int id)
        {
            var contact = await _dbcontext.Contacts.FirstOrDefaultAsync(contact => contact.Id == id);

            if (contact != null)
            {
                return _mymapper.ToContactDTO(contact);
            }

            return null;
        }

        public async Task<ContactDTO> CreateContact(ContactDTO newContact)
        {
            var contact = new Contact();
            contact.FirstName = newContact.FirstName;
            contact.LastName = newContact.LastName;
            contact.Phone = newContact.PhoneNumber;
            contact.Address = newContact.Address;

            _dbcontext.Contacts.Add(contact);
            await _dbcontext.SaveChangesAsync();

            return _mymapper.ToContactDTO(contact);
        }

        public async Task<ContactDTO> UpdateContact(ContactDTO updateContact)
        {
            var contact =await _dbcontext.Contacts.FirstOrDefaultAsync(contact => contact.Id == updateContact.Id);
            if (contact != null)
            {
                contact.FirstName = updateContact.FirstName;
                contact.LastName = updateContact.LastName;
                contact.Phone = updateContact.PhoneNumber;
                contact.Address = updateContact.Address;

                _dbcontext.Contacts.Update(contact);
                await _dbcontext.SaveChangesAsync();

                return _mymapper.ToContactDTO(contact);
            }

            return null;
        }

        public async Task<bool> DeleteContact(int id)
        {
            var contact = _dbcontext.Contacts.FirstOrDefault(contact => contact.Id == id);
            if (contact == null)
            {
                return false;
            }

            _dbcontext.Contacts.Remove(contact);
            await _dbcontext.SaveChangesAsync();

            return true;
        }

    }
}
