using ElectionModelLayer.ElectionModel;
using ElectionModelLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionRepositoryLayer.IElectionRL
{
   public interface IPartyRL
    {
        Task<PartyResponse> AddParty(PartyModel partyModel);
    }
}
