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
    public class VoterRLServices:IVoterRL
    {
        private readonly AuthenticationContext authenticationContext;
        public VoterRLServices(AuthenticationContext authenticationContext)
        {
            this.authenticationContext = authenticationContext;
        }
        public async Task<VoterModel> AddVoter(VoterModel voterModel)
        {
            try
            {
                var UserExists = authenticationContext.Voter.Where(x => x.MobileNumber == voterModel.MobileNumber).FirstOrDefault();
               // var userdata = this.authenticationContext.Voter.All(u => u.MobileNumber == voterModel.MobileNumber && u.FirstName==voterModel.FirstName);
                if (UserExists == null)
                {
                    var data = new VoterModel()
                    {
                      
                        FirstName = voterModel.FirstName,
                        LastName = voterModel.LastName,
                        MobileNumber = voterModel.MobileNumber,                       
                        CreatedDate = voterModel.CreatedDate,
                        ModifiedDate = voterModel.ModifiedDate,
                        CandidateId = voterModel.CandidateId

                    };

                    this.authenticationContext.Voter.Add(data);
                    var result = await this.authenticationContext.SaveChangesAsync();
                    if (result != null)
                    {
                        var response = new VoterModel()
                        {
                            Id = voterModel.Id,
                            FirstName = voterModel.FirstName,
                            LastName = voterModel.LastName,
                            MobileNumber = voterModel.MobileNumber,                           
                            CreatedDate = voterModel.CreatedDate,
                            ModifiedDate = voterModel.ModifiedDate,
                             CandidateId = voterModel.CandidateId
                        };
                        return response;
                    }

                    else
                    {
                        return null;
                    }
                }
                else {
                    return null;
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }



        public async Task<bool> DeleteVoter(int Id)
        {
            try
            {
                //foreach (var ConstuencyId in this.authenticationContext.Consituency)
                //{
                var data = this.authenticationContext.Voter.Where(u => u.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    var result = this.authenticationContext.Voter.Remove(data);
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


        public IList<CandidateResult> ConstituencyWise(int constotuencyId)
        {
            try
            {
                if (!constotuencyId.Equals(null))
                {
                    var candidates = from table in this.authenticationContext.Candidates where table.Id == constotuencyId select table;
                   // List<CandidateResult> list = new List<CandidateResult>();
                    if (candidates != null)
                    {
                        //foreach (var constotuency in authenticationContext.Consituency)
                        //{

                            List<CandidateResult> candidateJoin = (from can in candidates
                                                                   join party in authenticationContext.Party on
                                                                  can.PartyId equals party.Id
                                                                   //join userVote in authenticationContext.UserVoter on
                                                                   //can.Id equals userVote.CandidateId
                                                                   select new CandidateResult
                                                                   {
                                                                       Id = can.Id,
                                                                       FirstName = can.FirstName,
                                                                       LastName = can.LastName,
                                                                       PartyName = party.Name,
                                                                       //TotalVotes=userVote.CandidateId
                                                                       //TotalVotes= 
                                                                   }).ToList();

                            foreach (CandidateResult candidate in candidateJoin)
                            {
                                var countVotes = authenticationContext.Voter.
                                    Where(cand => cand.CandidateId == candidate.Id).ToList();

                                candidate.TotalVotes = countVotes.Count();

                            }


                            //var totalVotes = 0;

                            //foreach (var count in candidates)
                            //{
                            //    if (count.ca)
                            //}

                            var data = candidateJoin.ToList();
                        //}
                        return data;

                    }

                    //var userVotes = from table in this.authenticationContext.UserVoter where table.CandidateId ==  select table;


                    //var totalVotes = 0;

                    //foreach(var count in candidates)
                    //{
                    //    if(count.ca)
                    //}

                    return null;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public IList<PartyResult> PartyWise(string state)
        {
            try
            {
                //var party=from table in this.authenticationContext.Constituency where table.
                //var candidate = from table in this.authenticationContext.Candidate select table;
                var constituencyState = from table in this.authenticationContext.Consituency where table.State == state select table;

                List<PartyResult> partyJoin = (from p in authenticationContext.Party
                                               join c in authenticationContext.Candidates on
                                               p.Id equals c.PartyId
                                               join con in constituencyState on
                                               c.ConsituencyId equals con.Id
                                               select new PartyResult
                                               {
                                                   Id = c.Id,
                                                   PartyName = p.Name
                                               }).ToList();


                foreach (PartyResult party in partyJoin)
                {
                    var countVotes = authenticationContext.Voter.
                        Where(can => can.CandidateId == party.Id).ToList();

                    party.Total = countVotes.Count();
                    party.Won = countVotes.Count();
                }


                return partyJoin;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
