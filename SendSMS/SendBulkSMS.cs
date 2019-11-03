using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.Drawing;




namespace SendSMS
{
    [ToolboxBitmap("SendSMS.sms (1).bmp")]
    public class SendBulkSMS:CodeActivity
    {

        [Category("Authentication")]
        [RequiredArgument]
        [Description("API Ke obtained from Gateway Provider")]
        [DisplayName("API Key")]
        public InArgument<String> APIKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Recepients mobile number and Message Dictionary")]
        [DisplayName("Number-Message Dicionary")]
        public InArgument<Dictionary<String,String>> UserMsgDict { get; set; }


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
            Dictionary<String,String> MobileMsgDict = UserMsgDict.Get(context);
            String result = Methods.sendBulkSMS(MobileMsgDict, apiKey);
            Response.Set(context, result);
            Status.Set(context, Methods.getStatusFromResponse(result));
        }
    }
}
