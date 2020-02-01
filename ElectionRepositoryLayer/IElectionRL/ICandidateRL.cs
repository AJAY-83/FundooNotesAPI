using ElectionModelLayer.ElectionModel;
using ElectionModelLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionRepositoryLayer.IElectionRL
{
   public interface ICandidateRL
    {
        Task<CandidateResponseModel> AddCandidate(CandidateModel candidateModel);
        Task<bool> DeleteCandidate(int Id);
        IList<CandidateModel> DisplayCandidates();
    }
}
