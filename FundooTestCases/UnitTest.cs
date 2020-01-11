// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundooTestCases
{
    using RepositoryLayer.Interface;
    using RepositoryLayer.Services;
    using Moq;
    using System.Diagnostics.CodeAnalysis;
    using System;
    using Xunit;
    using CommonLayer.Model;
    using BusinessLayer.Services;


    /// <summary>
    ///  Unit test case 
    /// </summary>
    public class UnitTest
    {
        /// <summary>
        /// Registrations this instance.
        /// fact use to pass the all task
        /// </summary>
        [Fact]
        public void Registration()
        {
            var Repository = new Mock<IAccountRepositoryLayer>();
            var businesslayer = new AccountBusinessLayer(Repository.Object);

            var model = new AccountModel()
            {
                FirstName="FirstName",
                LastName = "LastName",
                MobileNumber = 123456789,
                Email = "Email",
                Password = "Password"
            };

            var data = businesslayer.Registration(model);
            Assert.NotNull(data);
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        [Fact]
        public void Login()
        {
            var Repository = new Mock<IAccountRepositoryLayer>();
            var businesslayer = new AccountBusinessLayer(Repository.Object);

            var model = new LoginModel()
            {
                Email = "Email",
                Password = "Password"
            };
            var data = businesslayer.Login(model);
            Assert.NotNull(data);
           
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        [Fact]
        public void ForgetPassword()
        {
            var Repository = new Mock<IAccountRepositoryLayer>();
            var businesslayer = new AccountBusinessLayer(Repository.Object);

            var model = new ForgetPasswordModel()
            {
                Email = "ajaylodale45@gamil.com"
               
            };
            var data = businesslayer.ForgetPassword(model);
            Assert.NotNull(data);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        [Fact]
        public void ResetPassword()
        {
            var Repository = new Mock<IAccountRepositoryLayer>();
            var businesslayer = new AccountBusinessLayer(Repository.Object);

            var model = new ResetPasswordModel()
            {
                Password = "Password",
                Token="adasdFDCSFSD.345435fssf.hfgh6"

            };
            var data = businesslayer.ResetPassword(model);
            Assert.NotNull(data);
        }

        /// <summary>
        /// Noteses this instance.
        /// </summary>
        //[Fact] 
        //public void Notes()
        //{
        //    var Repository = new Mock<INotesRepositoryLayer>();
        //    var businesslayer = new NotesBusinessLayerServices(Repository.Object);

        //    var model = new NotesModel()
        //    {
        //        Id = 4,
        //        Title = "string",
        //        Content="string",
        //        Image="image",
        //        Color="color",
        //        IsActive=true,
        //        UserId=3

        //    };

        //    var data = businesslayer.AddNotes(model);
        //    Assert.NotNull(data);           
        //}

        //[Fact]
        //public void Label()
        //{
        //    var Repository = new Mock<ILabelRepositoryLayer>();
        //    var businesslayer = new AdminBusinessService(Repository.Object);

        //    var model = new LabelModel()
        //    {
        //        NoteId = 4,
        //        Id = 2,
        //        UserId = 3,
        //        Label = "string"
        //    };

        //    var data = businesslayer.AddLabel(model);
        //    Assert.NotNull(data);
        //}
        

    }
}
