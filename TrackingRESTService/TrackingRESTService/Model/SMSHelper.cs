using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;

namespace TrackingRESTService.Model
{
    public class SMSHelper
    {
        string AccountSid = "AC35fbc6390f77c0315c6db7b027c5d851";
        string AuthToken = "a1f25d67813af956d6b948aa02d0c81c";
        TwilioRestClient twilio;

        public SMSHelper() {
            twilio = new TwilioRestClient(AccountSid, AuthToken);
        }

        public int sendMessage(string destination, string messageToSend) {
            try
            {
                var twilio = new TwilioRestClient(AccountSid, AuthToken);
                var msg = twilio.SendMessage("+15016536511", destination, messageToSend);
                return 1;
            }
            catch (Exception e) {
                throw new Exception(e.Message);
                //return -1;
            }


        }
    }
}