using ElectionModelLayer.ElectionModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionRepositoryLayer.Context
{
   public class AuthenticationContext:DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
        }

       public DbSet<VoterModel> Voter { get; set; }

        public DbSet<CandidateModel> Candidates { get; set; }

        public DbSet<ConsituencyModel> Consituency { get; set; }

        public DbSet<PartyModel> Party { get; set; }

        public DbSet<AdminModel> Admin { get; set; }
    }
}
