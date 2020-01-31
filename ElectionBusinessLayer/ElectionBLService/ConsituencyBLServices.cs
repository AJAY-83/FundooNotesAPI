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
  public  class ConsituencyBLServices:IConsituency
    {

        private readonly IConsituencyRL consituencyRL;

        public ConsituencyBLServices(IConsituencyRL consituencyRL)
        {
            this.consituencyRL = consituencyRL;
        }

      public async  Task<ConsituencyReponseModel> AddConsituenct(ConsituencyModel consituencyModel)
        {
            try
            {
                var result = await this.consituencyRL.AddConsituenct(consituencyModel);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteConsituency(int Id)
        {
            try
            {
                var result = await this.consituencyRL.DeleteConsituency(Id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ConsituencyReponseModel> UpdateConsituency(ConsituencyModel consituencyModel)
        {
            try
            {
                var result = await this.consituencyRL.UpdateConsituency(consituencyModel);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       public IList<ConsituencyModel> DisplayConsituency()
        {
            try
            {
                var result =  this.consituencyRL.DisplayConsituency();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
