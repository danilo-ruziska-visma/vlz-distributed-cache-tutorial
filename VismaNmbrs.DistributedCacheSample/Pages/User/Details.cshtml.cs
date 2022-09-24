using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VismaNmbrs.DistributedCacheSample.Data;
using UserEntity = VismaNmbrs.DistributedCacheSample.Entities.User;

namespace VismaNmbrs.DistributedCacheSample.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly IAsyncDatabase<UserEntity> _asyncDatabase;

        public DetailsModel(IAsyncDatabase<UserEntity> asyncDatabase)
        {
            _asyncDatabase = asyncDatabase;
        }
        [BindProperty]
        public UserEntity UserEntity { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }            
            
            var user = await _asyncDatabase.GetById(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                UserEntity = user;
            }
            return Page();
        }
    }
}
