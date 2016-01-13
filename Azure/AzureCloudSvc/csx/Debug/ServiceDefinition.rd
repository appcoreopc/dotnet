<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AzureCloudSvc" generation="1" functional="0" release="0" Id="0c2a78f6-d6d5-4aff-9455-22abc8cc7c5a" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AzureCloudSvcGroup" generation="1" functional="0" release="0">
      <settings>
        <aCS name="AzureWorkProcess:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudSvc/AzureCloudSvcGroup/MapAzureWorkProcess:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="AzureWorkProcessInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AzureCloudSvc/AzureCloudSvcGroup/MapAzureWorkProcessInstances" />
          </maps>
        </aCS>
      </settings>
      <maps>
        <map name="MapAzureWorkProcess:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudSvc/AzureCloudSvcGroup/AzureWorkProcess/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapAzureWorkProcessInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AzureCloudSvc/AzureCloudSvcGroup/AzureWorkProcessInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="AzureWorkProcess" generation="1" functional="0" release="0" software="D:\test\demo\C#\AzureCloudSvc\csx\Debug\roles\AzureWorkProcess" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;AzureWorkProcess&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AzureWorkProcess&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AzureCloudSvc/AzureCloudSvcGroup/AzureWorkProcessInstances" />
            <sCSPolicyUpdateDomainMoniker name="/AzureCloudSvc/AzureCloudSvcGroup/AzureWorkProcessUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/AzureCloudSvc/AzureCloudSvcGroup/AzureWorkProcessFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="AzureWorkProcessUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="AzureWorkProcessFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="AzureWorkProcessInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
</serviceModel>