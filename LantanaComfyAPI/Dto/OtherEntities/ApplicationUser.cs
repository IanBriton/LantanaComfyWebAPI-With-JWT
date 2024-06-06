using Microsoft.AspNetCore.Identity;

namespace LantanaComfyAPI.Dto.OtherEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

}
