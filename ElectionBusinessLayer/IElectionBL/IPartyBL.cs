using ElectionModelLayer.ElectionModel;
using ElectionModelLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionBusinessLayer.IElectionBL
{
   public interface IPartyBL
    {
        Task<PartyResponse> AddParty(PartyModel partyModel);
    }
}
