using ElectionModelLayer.ElectionModel;
using ElectionModelLayer.ResponseModel;
using ElectionRepositoryLayer.Context;
using ElectionRepositoryLayer.IElectionRL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionRepositoryLayer.ElectionRLServices
{
    public class CandidateRLServices : ICandidateRL
    {
        private readonly AuthenticationContext authenticationContext;
   

        
        public CandidateRLServices(AuthenticationContext authenticationContext)
        {
            this.authenticationContext = authenticationContext;
        }
        public async Task<CandidateResponseModel> AddCandidate(CandidateModel candidateModel)
        {
            try
            {

                var data = new CandidateModel()
                {
                    FirstName=candidateModel.FirstName,
                    LastName=candidateModel.LastName,
                    Id=candidateModel.Id,
                    ConsituencyId=candidateModel.ConsituencyId,
                    PartyId=candidateModel.PartyId,
                    CreatedDate=candidateModel.CreatedDate,
                    ModifiedDate=candidateModel.ModifiedDate
                };

                this.authenticationContext.Candidates.Add(data);
                var result = await this.authenticationContext.SaveChangesAsync();
                if (result != null)
                {
                    var response = new CandidateResponseModel()
                    {
                        Id = candidateModel.Id,
                        FirstName = candidateModel.FirstName,
                        LastName = candidateModel.LastName,                       
                        ConsituencyId = candidateModel.ConsituencyId,
                        PartyId = candidateModel.PartyId,
                        CreatedDate = candidateModel.CreatedDate,
                        ModifiedDate = candidateModel.ModifiedDate
                    };
                    return response;
                }

                else
                {
                    return null;
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public async Task<bool> DeleteCandidate(int Id)
        {
            try
            {
                //foreach (var ConstuencyId in this.authenticationContext.Consituency)
                //{
                var data = this.authenticationContext.Candidates.Where(u => u.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    var result = this.authenticationContext.Candidates.Remove(data);
                    await this.authenticationContext.SaveChangesAsync();
                    if (result != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                //}
                //return false;
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
                List<CandidateModel> candidatesList = new List<CandidateModel>();


                foreach (var ConstuencyData in this.authenticationContext.Candidates)
                {
                    candidatesList.Add(ConstuencyData);
                }

                return candidatesList;
                //return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
