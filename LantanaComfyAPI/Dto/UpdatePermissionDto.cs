using System.ComponentModel.DataAnnotations;

namespace LantanaComfyAPI.Dto
{
    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
    }
}
