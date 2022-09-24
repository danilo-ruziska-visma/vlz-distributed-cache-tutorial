using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VismaNmbrs.DistributedCacheSample.Data;
using UserEntity = VismaNmbrs.DistributedCacheSample.Entities.User;

namespace VismaNmbrs.DistributedCacheSample.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly IAsyncDatabase<UserEntity> _asyncDatabase;

        public DeleteModel(IAsyncDatabase<UserEntity> asyncDatabase)
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _asyncDatabase.Delete(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
