using System.Collections.Generic;
using Data_Access_Logic;
using Models;

namespace Business_Logic
{
    public class StoreFrontBL : IStoreFrontBL
    {
        private IRepository _repo;
        public StoreFrontBL(IRepository p_repo)
        {
            _repo = p_repo;
        }
        public List<StoreFront> GetStoreFrontList()
        {
            return _repo.GetStoreFrontList();
        }
    }
}