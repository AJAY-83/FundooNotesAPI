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
    public class VoterBusinessLayer : IVoterBusinessLayer
    {
        private readonly IVoterRL voterRL;
        public VoterBusinessLayer(IVoterRL voterRL)
        {
            this.voterRL = voterRL;
        }

        public async Task<VoterModel> AddVoter(VoterModel voterModel)
        {
            try
            {
                var result = await this.voterRL.AddVoter(voterModel);
                return result;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteVoter(int Id)
        {
            try
            {

                var result = await this.voterRL.DeleteVoter(Id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<CandidateResult> ConstituencyWise(int constotuencyId)
        {
            var result = this.voterRL.ConstituencyWise(constotuencyId);
            return result;
        }

        public IList<PartyResult> PartyWise(string state)
        {
            try
            {
                var result = this.voterRL.PartyWise(state);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
