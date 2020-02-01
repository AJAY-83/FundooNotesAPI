using ElectionModelLayer.ElectionModel;
using ElectionModelLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionRepositoryLayer.IElectionRL
{
  public  interface IVoterRL
    {
        Task<VoterModel> AddVoter(VoterModel voterModel);

        Task<bool> DeleteVoter(int Id);

        IList<CandidateResult> ConstituencyWise(int constotuencyId);

        IList<PartyResult> PartyWise(string state);
    }
}
