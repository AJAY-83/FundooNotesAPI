using ElectionModelLayer.ElectionModel;
using ElectionModelLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionBusinessLayer.IElectionBL
{
  public  interface IConsituency
    {
        Task<ConsituencyReponseModel> AddConsituenct(ConsituencyModel consituencyModel);
        Task<bool> DeleteConsituency(int Id);
        Task<ConsituencyReponseModel> UpdateConsituency(ConsituencyModel consituencyModel);
        IList<ConsituencyModel> DisplayConsituency();
    }
}
