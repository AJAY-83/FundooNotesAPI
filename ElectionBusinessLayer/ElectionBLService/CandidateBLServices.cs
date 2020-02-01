using ElectionBusinessLayer.IElectionBL;
using ElectionModelLayer.ElectionModel;
using ElectionModelLayer.ResponseModel;
using ElectionRepositoryLayer.IElectionRL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionBusinessLayer.ElectionBLService
{
    public class CandidateBLServices : ICandidateBL
    {
        private readonly ICandidateRL candidateRL;
        public CandidateBLServices(ICandidateRL candidateRL)
        {
            this.candidateRL = candidateRL;
        }
        public async Task<CandidateResponseModel> AddCandidate(CandidateModel candidateModel)
        {
            try
            {
                var result = await this.candidateRL.AddCandidate(candidateModel);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteCandidate(int Id)
        {
         try
            {
                var result = await this.candidateRL.DeleteCandidate(Id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
         }

        public IList<CandidateModel> DisplayCandidates()
        {
            try
            {
                var result =  this.candidateRL.DisplayCandidates();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
