using ImageStock.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ImageStock.Data
{
    public class AppDataContext
    {
        private static AppDataContext _instance = null;
        public static AppDataContext Instance 
        { 
            get => _instance;
            set
            {
                if (_instance != null) throw new InvalidOperationException($"{nameof(Instance)} is already set.");
                _instance = value;
            }
        }

        private AppDbContext _appDbContext;
        private HttpContext _httpContext;
        public UserProfile CurrentUser
        {
            get
            {
                if (_httpContext.User.Identity.IsAuthenticated)
                {
                    return _appDbContext.Users.FirstOrDefault(u => u.Username == _httpContext.User.Identity.Name);
                }
                return null;
            }
        }
        public AppDataContext(IServiceProvider services)
        {
            _appDbContext = services.GetRequiredService<AppDbContext>();
            _httpContext = services.GetRequiredService<HttpContextAccessor>().HttpContext;
        }

        public AppDataContext(AppDbContext appDbContext, HttpContextAccessor httpContextAccesor)
        {
            _appDbContext = appDbContext;
            _httpContext = httpContextAccesor.HttpContext;
        }
    }
}
