using Fiorello.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Services
{
    public class SettingService
    {
        private AppDbContext _context { get; }
        public SettingService(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetAllSettings()
        {
            return _context.Settings.ToDictionary(s => s.Key, s => s.Value);
        }
    }
}
