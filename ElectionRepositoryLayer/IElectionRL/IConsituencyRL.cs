using ElectionModelLayer.ElectionModel;
using ElectionModelLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionRepositoryLayer.IElectionRL
{
  public  interface IConsituencyRL
    {
        Task<ConsituencyReponseModel> AddConsituenct(ConsituencyModel consituencyModel);
        Task<bool> DeleteConsituency(int Id);
        Task<ConsituencyReponseModel> UpdateConsituency(ConsituencyModel consituencyModel);
        IList<ConsituencyModel> DisplayConsituency();
    }
}
