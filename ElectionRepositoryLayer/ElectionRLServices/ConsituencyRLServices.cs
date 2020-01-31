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
    public class ConsituencyRLServices : IConsituencyRL
    {
        private readonly AuthenticationContext  authenticationContext;
        IConfiguration configuration;

        public ConsituencyRLServices(AuthenticationContext authenticationContext, IConfiguration configuration)
        {
            this.authenticationContext = authenticationContext;
            this.configuration = configuration;
        }

        public async Task<ConsituencyReponseModel> AddConsituenct(ConsituencyModel consituencyModel)
        {
            try
            {
          
                var data = new ConsituencyModel()
                {
                    Name=consituencyModel.Name,
                    State=consituencyModel.State,
                    CreatedDate=consituencyModel.CreatedDate,
                    ModifiedDate=consituencyModel.ModifiedDate
                };

                this.authenticationContext.Add(data);
               var result= await this.authenticationContext.SaveChangesAsync();
                if (result != null)
                {
                    var response = new ConsituencyReponseModel()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        State=data.State,
                        CreatedDate = data.CreatedDate
                    };
                    return response;
                }

                else {
                    return null;
                }

            } catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public async Task<bool> DeleteConsituency(int Id)
        {
            try
            {
                //foreach (var ConstuencyId in this.authenticationContext.Consituency)
                //{
                    var data = this.authenticationContext.Consituency.Where(u => u.Id == Id).FirstOrDefault();
                    if (data != null)
                    {
                        var result = this.authenticationContext.Consituency.Remove(data);
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


        public async Task<ConsituencyReponseModel> UpdateConsituency(ConsituencyModel consituencyModel)
        {
            try
            {
                //foreach (var ConstuencyId in this.authenticationContext.Consituency)
                //{
                var data = this.authenticationContext.Consituency.Where(u => u.Id ==consituencyModel.Id).FirstOrDefault();
                if (data != null)
                {
                    if (data.Name != null)
                    {
                        data.Name = consituencyModel.Name;
                        data.ModifiedDate = consituencyModel.ModifiedDate;
                        await this.authenticationContext.SaveChangesAsync();

                        var response = new ConsituencyReponseModel()
                        {
                            Id = data.Id,
                            Name = data.Name,
                            State = data.State
                        };
                        return response;
                    }
                    else if (data.State != null)
                    {
                        data.State=consituencyModel.State;
                        data.ModifiedDate = consituencyModel.ModifiedDate;
                        await this.authenticationContext.SaveChangesAsync();

                        var response = new ConsituencyReponseModel()
                        {
                            Id = data.Id,
                            Name = data.Name,
                            State = data.State
                            
                        };

                        return response;

                    }
                }
                return null;
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
                List<ConsituencyModel> consituencyList = new List<ConsituencyModel>();

               
                foreach (var ConstuencyData in this.authenticationContext.Consituency)
                {
                    consituencyList.Add(ConstuencyData);
                }

                return  consituencyList;
                //return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
