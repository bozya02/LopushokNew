using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LopushokNew.DB
{
    public partial class LopushokNewBozyaEntities
    {
        private static LopushokNewBozyaEntities _context;

        public static LopushokNewBozyaEntities GetContext()
        {
            if (_context == null)
                _context = new LopushokNewBozyaEntities();

            return _context;
        }
    }
}
