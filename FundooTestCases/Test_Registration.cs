//using BusinessLayer.Services;
//using CommonLayer.Model;
//using CommonLayer.Request;
//using Moq;
//using RepositoryLayer.Interface;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xunit;

//namespace FundooTestCases
//{
//    public class Test_Registration
//    {

//        [Fact]
//public void Registration()
//{
//    //// Arrange
//    var Repository = new Mock<IAccountRepositoryLayer>();
//    var businesslayer = new AccountBusinessLayer(Repository.Object);


//    var model = new SignUpRequest()
//    {
//        FirstName = "FirstName",
//        LastName = "LastName",
//        MobileNumber = 123456789,
//        Email = "Email",
//        Password = "Password"
//    };

//    //// Act
//   var data = businesslayer.Registration(model);
//    //// Assert  
//    Assert.NotNull(data);

//        }
//        //[Fact]
//        //public void ChechEqualsRegistration()
//        //{
//        //    // Arrange

//        //    var register = new AccountModel()
//        //    {
//        //        Id = 1,
//        //        FirstName = "FirstName",
//        //        LastName = "LastName",
//        //        MobileNumber = 123456789,
//        //        Email = "Email",
//        //        Password = "Password",
//        //        Image = "image",
//        //        TypeOfUser = "basic"

//        //    };
//        //    // Act
//        //    var Repository = new Mock<IAccountRepositoryLayer>();
//        //    var businesslayer = new AccountBusinessLayer(Repository.Object);

//        //    var data = businesslayer.Registration(register);

//        //    // Assert
//        //    Assert.Equal(data, register);
//        //}
//    }
//}
