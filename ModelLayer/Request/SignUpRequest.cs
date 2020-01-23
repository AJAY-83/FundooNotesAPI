﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Request
{
  public class SignUpRequest
    {
      

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
        /// Gets or sets the type of user.
        /// Admin or User.
        /// </summary>
        /// <value>
        /// The type of user.
        /// </value>
        public string TypeOfUser { get; set; }

        /// <summary>
        /// Gets or sets the services.
        /// Basic or Advance User.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        public string Services { get; set; }

       // public string Status { get; set; }

        
    }
}
