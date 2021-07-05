using App.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Library.Repositories
{
    public interface IStatusRepository
    {
        Task<Tuple<bool, List<Lookup>>> GetAllStatuses();
        Task<Tuple<bool>> SaveStatus(Lookup lookup);
        Task<Tuple<bool, bool>> DeleteStatus(int Id);

    }
}
