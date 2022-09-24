using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VismaNmbrs.DistributedCacheSample.Data;
using VismaNmbrs.DistributedCacheSample.Cache;
using UserEntity = VismaNmbrs.DistributedCacheSample.Entities.User;
using Microsoft.Extensions.Options;
using VismaNmbrs.DistributedCacheSample.Options;

namespace VismaNmbrs.DistributedCacheSample.Pages.User
{
    public class CreateModel : PageModel
    {
        
        private readonly ICacheProvider _cacheProvider;
        private readonly IAsyncDatabase<UserEntity> _asyncDatabase;
        private readonly IOptions<CacheSettingsOptions> _cacheSettingsOptions;

        public CreateModel(ICacheProvider cacheProvider,
                           IAsyncDatabase<UserEntity> asyncDatabase,
                           IOptions<CacheSettingsOptions> cacheSettingsOptions)
        {
            _cacheProvider = cacheProvider;
            _asyncDatabase = asyncDatabase;
            _cacheSettingsOptions = cacheSettingsOptions;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserEntity UserEntity { get; set; } = default!;        

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || User == null)
            {
                return Page();
            }

            await _asyncDatabase.Add(UserEntity);

            IList<UserEntity> users = await _asyncDatabase.GetAll();            

            await _cacheProvider.SetCache("cache_key_list_users", users, TimeSpan.FromMinutes(_cacheSettingsOptions.Value.DefaultSlidingExpirationInMinutes));

            return RedirectToPage("./Index");
        }
    }
}
