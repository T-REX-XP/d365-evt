using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

namespace EVTPlugins
{

    public class GetEnvironmentVersion : PluginBase
    {

        public GetEnvironmentVersion() : base(typeof(GetEnvironmentVersion))
        {
        }

        protected override void ExecuteCdsPlugin(ILocalPluginContext localPluginContext)
        {
            var context = localPluginContext.PluginExecutionContext;
            var tracingService = localPluginContext.TracingService;

            if (context.MessageName.Equals("yn_GetEnvironmentVersion2") && context.Stage.Equals(30))
            {
                try
                {
                    var request = new RetrieveVersionRequest();
                    var response = (RetrieveVersionResponse)localPluginContext.CurrentUserService.Execute(request);

                    if (!string.IsNullOrEmpty(response?.Version))
                    {
                        //Simply reversing the characters of the string
                        context.OutputParameters["Version"] = response?.Version;
                    }
                }
                catch (Exception ex)
                {
                    tracingService.Trace("yn_GetEnvironmentVersion2: {0}", ex.ToString());
                    throw new InvalidPluginExecutionException("An error occurred in yn_GetEnvironmentVersion.", ex);
                }
            }
            else
            {
                tracingService.Trace("yn_GetEnvironmentVersion2 plug-in is not associated with the expected message or is not registered for the main operation.");
            }
        }
    }
}
