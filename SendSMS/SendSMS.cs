using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.Drawing;


internal class resfinder
{
}


namespace SendSMS
{  
    [ToolboxBitmap("SendSMS.sms (1).bmp")]
    public class SendSMS:CodeActivity
    {

        [Category("Authentication")]
        [RequiredArgument]
        [Description("API Ke obtained from Gateway Provider")]
        [DisplayName("API Key")]
        public InArgument<String> APIKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Recepients mobile number")]
        [DisplayName("Mobile Number")]
        public InArgument<String> mobileNumber { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter message to send")]
        [DisplayName("Message")]
        public InArgument<String> message { get; set; }

        [Category("Output")]
        [DisplayName("Response")]
        [Description("Response from SMS Gateway")]
        public OutArgument<String> Response { get; set; }

        [Category("Output")]
        [DisplayName("Status")]
        [Description("Message sending status")]
        public OutArgument<Boolean> Status { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            String apiKey = APIKey.Get(context);
            String Destination = mobileNumber.Get(context);
            String Message = message.Get(context);
            String result = Methods.sendSMS(Message, Destination, apiKey);
            Response.Set(context, result);
            Status.Set(context,Methods.getStatusFromResponse(result));
        }
    }
}
