using ElectionModelLayer.ElectionModel;
using ElectionModelLayer.ResponseModel;
using ElectionRepositoryLayer.Context;
using ElectionRepositoryLayer.IElectionRL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionRepositoryLayer.ElectionRLServices
{
  public  class PartyRLServices: IPartyRL
    {
        private readonly AuthenticationContext authenticationContext;
      IConfiguration configuration;

       
        public PartyRLServices(AuthenticationContext authenticationContext, IConfiguration configuration)
        {
            this.authenticationContext = authenticationContext;
            this.configuration = configuration;
        }
       
        public async Task<PartyResponse> AddParty(PartyModel partyModel)
         {
            try
            {

                var data = new PartyModel()
                {
                    Id = partyModel.Id,
                    Name =partyModel.Name,
                    CreatedDate=partyModel.CreatedDate,
                    ModifiedDate=partyModel.ModifiedDate
                    
                };

                this.authenticationContext.Add(data);
                var result = await this.authenticationContext.SaveChangesAsync();
                if (result != null)
                {
                    var response = new PartyResponse()
                    {
                        Id = data.Id,
                        Name = data.Name,                      
                        CreatedDate = data.CreatedDate
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


        public async Task<bool> DeleteParty(int Id)
        {
            try
            {
                //foreach (var ConstuencyId in this.authenticationContext.Consituency)
                //{
                var data = this.authenticationContext.Party.Where(u => u.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    var result = this.authenticationContext.Party.Remove(data);
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
    }
}
