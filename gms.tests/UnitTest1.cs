using Xunit;
using gms.lib;

namespace gms.tests;

public class UnitTest1
{
    [Fact]
    public void InstantiateClass()
    {
        string name   = "Box1";
        string fqdn   = "Box1.somedomain.org";
        string ip     = "10.0.0.1";
        string role   = "dchp";
        string env    = "PROD";
        string os     = "WindowsServer2022";
        string status = "Staging";
        string notes  = "replaces win2019 box";

        var myServer = new MyServer(name, fqdn, ip, role, env, os, status, notes);

        Assert.Equal(name, myServer.Name);
        Assert.Equal(fqdn, myServer.FQDN);
        Assert.Equal(ip, myServer.IPAddress);
        Assert.Equal(env, myServer.ENV);
        Assert.Equal(role, myServer.Role);
        Assert.Equal(os, myServer.OperatingSystem);
        Assert.Equal(status, myServer.Status);
        Assert.Equal(notes, myServer.Notes);
    }

    [Fact]
    public void GetSetProperties()
    {
        string name   = "Box1";
        string fqdn   = "Box1.somedomain.org";
        string ip     = "10.0.0.1";
        string role   = "dchp";
        string env    = "PROD";
        string os     = "WindowsServer2022";
        string status = "live";
        string notes  = "replaces win2019 box";

        var myServer = new MyServer();

        myServer.Name            = name;
        myServer.FQDN            = fqdn;
        myServer.IPAddress       = ip;
        myServer.ENV             = env;
        myServer.Role            = role;
        myServer.OperatingSystem = os;
        myServer.Status          = status;
        myServer.Notes           = notes;

        Assert.Equal(name, myServer.Name);
        Assert.Equal(fqdn, myServer.FQDN);
        Assert.Equal(ip, myServer.IPAddress);
        Assert.Equal(env, myServer.ENV);
        Assert.Equal(role, myServer.Role);
        Assert.Equal(os, myServer.OperatingSystem);
        Assert.Equal(status, myServer.Status);
        Assert.Equal(notes, myServer.Notes);
    }
}
