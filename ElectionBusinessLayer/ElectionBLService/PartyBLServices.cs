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
   public class PartyBLServices:IPartyBL
    {
        private readonly IPartyRL partyRL;

        public PartyBLServices(IPartyRL partyRL)
        {
            this.partyRL = partyRL;
        }

        public async Task<PartyResponse> AddParty(PartyModel partyModel)
        {
            try
            {
                var result = await this.partyRL.AddParty(partyModel);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
