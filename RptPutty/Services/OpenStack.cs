using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

// openstack-sdk-dotnet @ https://github.com/stackforge/openstack-sdk-dotnet
using OpenStack;
using OpenStack.Identity;
using OpenStack.Storage;

namespace RptPutty.Services
{
    public class OpenStack
    {
        static public List<String> getExports()
        {
            // Login
            var credential = new OpenStackCredential(new Uri("http://10.2.4.20:5000/v2.0"), "rptdynamo", "3IF0unuGJ2Qi4kOtYq1M", "itreporting", "RegionOne");
            var client = OpenStackClientFactory.CreateClient(credential);
            client.Connect().Wait();
            // Get public URL
            var swiftPublicEndpoint = credential.ServiceCatalog.GetPublicEndpoint("swift", "RegionOne");
            // Get Object List
            var swiftClient =  client.CreateServiceClient<IStorageServiceClient>();
            var swiftListObjects = swiftClient.ListStorageObjects("exports");
            swiftListObjects.Wait();
            var swiftObjects = swiftListObjects.Result;

            List<string> SwiftObjURL = new List<string>();
            foreach (var sObject in swiftObjects)
            {
                SwiftObjURL.Add(swiftPublicEndpoint + "/" + "exports" + "/" + sObject.FullName);
            }
            return SwiftObjURL;
        }
    }
}