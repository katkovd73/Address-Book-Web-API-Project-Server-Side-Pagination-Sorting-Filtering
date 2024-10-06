using AddressBookAPI.DTOs;
using AddressBookAPI.Models;

namespace AddressBookAPI.Mappers
{
    public class MyMapper
    {
        public ContactDTO ToContactDTO(Contact contact)
        {
            var contactDTO = new ContactDTO();
            contactDTO.Id = contact.Id;
            contactDTO.FirstName = contact.FirstName;
            contactDTO.LastName = contact.LastName;
            contactDTO.PhoneNumber = contact.Phone;
            contactDTO.Address = contact.Address;

            return contactDTO;
        }

        public List<ContactDTO> ToContactDTOList(List<Contact> contacts)
        {

            List<ContactDTO> contactsDTO = new List<ContactDTO>();

            contacts.ForEach(contact =>
            {
                var contactDTO = new ContactDTO();
                contactDTO.Id = contact.Id;
                contactDTO.FirstName = contact.FirstName;
                contactDTO.LastName = contact.LastName;
                contactDTO.PhoneNumber = contact.Phone;
                contactDTO.Address = contact.Address;

                contactsDTO.Add(contactDTO);
            });

            return contactsDTO;
        }
    }
}
