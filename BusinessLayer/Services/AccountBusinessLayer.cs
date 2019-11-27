using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AccountBusinessLayer : IAccountBusinessLayer
    {
        private IAccountRepositoryLayer account;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountBusinessLayer"/> class.
        /// </summary>
        /// <param name="account">The account.</param>
        public AccountBusinessLayer(IAccountRepositoryLayer account)
        {
            this.account = account;
        }

        /// <summary>
        /// Registrations the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<string> Registration(AccountModel model)
        {
            if (model != null)
            {
                var result = await account.Registration(model);
                return result.ToString();
            }
            else {
                return " Model is Empty";
            }
        }
    }
}
