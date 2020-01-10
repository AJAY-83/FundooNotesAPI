// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountModel.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------

namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// AccountModel class have the all properties of user
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
      //  [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
         [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        [Required]
        public int MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
          [Required]
        public string Password { get; set; }


        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the type of user.
        /// Admin or User.
        /// </summary>
        /// <value>
        /// The type of user.
        /// </value>
        public string  TypeOfUser { get; set; }

        /// <summary>
        /// Gets or sets the services.
        /// Basic or Advance User.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        public string  Services { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is facebook.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is facebook; otherwise, <c>false</c>.
        /// </value>
        public bool IsFacebook { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is google.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is google; otherwise, <c>false</c>.
        /// </value>
        public bool IsGoogle { get; set; }
    }
}
