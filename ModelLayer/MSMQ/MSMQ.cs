// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MSMQ.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------

namespace CommonLayer.MSMQ
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading.Tasks;

    using System.Net.Mail;
    using System.Messaging;

    public class MSMQ
    {
        public void SendTokenQueue(string email, string token)
        {
            MessageQueue msmqObject = null;
            const string QueueName = @".\private$\EmailQueue";
            if (!MessageQueue.Exists(QueueName))
            {
                msmqObject = MessageQueue.Create(QueueName);
            }
            else
            {
                msmqObject = new MessageQueue(QueueName);
            }
            try
            {
                msmqObject.Send("hello");
            }
            catch (MessageQueueException mqe)
            {
                Console.Write(mqe.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                msmqObject.Close();
            }

        }
    }
}
