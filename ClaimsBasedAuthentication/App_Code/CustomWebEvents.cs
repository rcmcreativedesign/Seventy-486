using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.UI;

namespace Samples.AspNet.Management
{
    public class CustomWebEvents : Page, IHttpModule
    {
        public override void Dispose()
        {
        }

        public new void Init(HttpApplication httpApp)
        {
            httpApp.BeginRequest += new EventHandler(OnBeginRequest);
            httpApp.EndRequest += new EventHandler(OnEndRequest);
        }

        private void OnBeginRequest(Object sender, EventArgs e)
        {
            HttpApplication httpApp = sender as HttpApplication;

            try
            {
                System.Int32 myCode = WebEventCodes.WebExtendedBase + 30;
                SampleWebRequestEvent swre = new SampleWebRequestEvent("SampleWebRequestEvent Start", this, myCode);
                swre.Raise();
            }
            catch (Exception ex)
            {
                httpApp.Context.Response.Output.WriteLine(ex.ToString());
            }
        }

        private void OnEndRequest(Object sender, EventArgs e)
        {
            HttpApplication httpApp = sender as HttpApplication;

            try
            {
                System.Int32 myCode = WebEventCodes.WebExtendedBase + 40;
                SampleWebRequestEvent swre = new SampleWebRequestEvent("SampleWebRequestEvent End", this, myCode);
                swre.Raise();
            }
            catch (Exception ex)
            {
                httpApp.Context.Response.Output.WriteLine(ex.ToString());
            }
        }

    }

    internal class SampleWebRequestEvent : System.Web.Management.WebRequestEvent
    {
        private string customCreateMsg;
        private string customRaiseMsg;

        public SampleWebRequestEvent(string msg, object eventSource, int eventCode) : base(msg, eventSource, eventCode)
        {
            customCreateMsg = string.Format("Event created at: {0}", EventTime.ToString());
        }

        public SampleWebRequestEvent(string msg, object eventSource, int eventCode, int eventDetailCode) : base(msg, eventSource, eventCode, eventDetailCode)
        {
            customCreateMsg = string.Format("Event created at: {0}", EventTime.ToString());
        }

        public override void Raise()
        {
            customRaiseMsg = string.Format("Event raised at: {0}", EventTime.ToString());

            base.Raise();
        }

        public override void FormatCustomEventDetails(WebEventFormatter formatter)
        {
            formatter.AppendLine("");

            formatter.IndentationLevel += 1;
            formatter.AppendLine("* Custom Request Information Start *");

            formatter.AppendLine(customCreateMsg);
            formatter.AppendLine(customRaiseMsg);

            formatter.AppendLine("* Custom Request Information End *");

            formatter.IndentationLevel -= 1;
        }
    }
}