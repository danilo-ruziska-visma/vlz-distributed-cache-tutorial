using Microsoft.AspNetCore.Mvc.RazorPages;
using VismaNmbrs.DistributedCacheSample.Data;
using VismaNmbrs.DistributedCacheSample.Cache;
using UserEntity = VismaNmbrs.DistributedCacheSample.Entities.User;
using Microsoft.Extensions.Options;
using VismaNmbrs.DistributedCacheSample.Options;
using VismaNmbrs.DistributedCacheSample.ViewModel;
using AutoMapper;

namespace VismaNmbrs.DistributedCacheSample.Pages.User
{
    public class IndexModel : PageModel
    {        
        private readonly ICacheProvider _cacheProvider;
        private readonly IAsyncDatabase<UserEntity> _asyncDatabase;
        private readonly IOptions<CacheSettingsOptions> _cacheSettingsOptions;
        private readonly IMapper _mapper;

        public IndexModel(ICacheProvider cacheProvider,
                          IAsyncDatabase<UserEntity> asyncDatabase,
                          IOptions<CacheSettingsOptions> cacheSettingsOptions,
                          IMapper mapper)
        {            
            _cacheProvider = cacheProvider;
            _asyncDatabase = asyncDatabase;
            _cacheSettingsOptions = cacheSettingsOptions;
            _mapper = mapper;
        }

        public IList<UserViewModel> Users { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var cachedUsers = await _cacheProvider.GetOrCreateFromCache("cache_key_list_users", TimeSpan.FromMinutes(_cacheSettingsOptions.Value.DefaultSlidingExpirationInMinutes), _asyncDatabase.GetAll);
            if (cachedUsers != null)
            {
                Users = _mapper.Map<IList<UserViewModel>>(cachedUsers);
            }
        }
    }
}