// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationContext.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------


namespace RepositoryLayer.Context
{
    using CommonLayer.Model;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// AuthenticationContext is inherit the DbContext 
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class AuthenticationContext : DbContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options):base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user account table.
        /// </summary>
        /// <value>
        /// The user account table.
        /// </value>
        public DbSet<AccountModel> UserAccountTable { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public DbSet<NotesModel> Notes { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// 
        /// </value>
        public DbSet<LabelModel> Label { get; set; }

        /// <summary>
        /// Gets or sets the label model table.
        /// </summary>
        /// <value>
        /// The label model table.
        /// </value>
        public DbSet<NoteLabel> NoteLabel { get; set; }

        public DbSet<CollabratorModel> Collabrator { get; set; }
        //public DbSet<LabelModel> LabelTable { get; set; }       
    }
}
