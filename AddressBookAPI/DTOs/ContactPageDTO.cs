namespace AddressBookAPI.DTOs
{
    public class ContactPageDTO
    {
        public int ContactsTotalCount { get; set; }
        public List<ContactDTO> PageContacts { get; set; }
    }
}
