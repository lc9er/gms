using Xunit;
using gmslib;
using System.IO;

namespace gms.tests;

public class UnitTest1
{
    static readonly string dbFile = "../../../MyServersTest.db";
    static readonly string connectionString = "DataSource=" + dbFile;

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

    [Fact]
    public void CreateDb()
    {
        DatabaseManager databaseManager = new();
        databaseManager.CreateTable(connectionString);

        Assert.True(File.Exists(dbFile));
    }

    [Fact]
    public void GetAllServers()
    {
        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();

        results = myServerController.GetAll();

        Assert.True(results.Any());
        Assert.Equal(2, results.Count);
    }

    [Theory]
    [InlineData("lilserver")]
    [InlineData("bigserver")]
    public void GetByName(string value)
    {
        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();

        results = myServerController.GetByProperty("Name", value);

        foreach (var item in results)
            Assert.Equal(value, item.Name);
    }

    [Theory]
    [InlineData("LILSERVER")]
    [InlineData("bIgSeRvEr")]
    public void GetByNameNoCase(string value)
    {
        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();

        results = myServerController.GetByProperty("Name", value);

        foreach (var item in results)
            Assert.Equal(value.ToLower(), item.Name.ToLower());
    }

    [Theory]
    [InlineData("lilserver.fake.domain")]
    [InlineData("bigserver.fake.domain")]
    public void GetByFQDN(string value)
    {
        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();

        results = myServerController.GetByProperty("FQDN", value);

        foreach (var item in results)
            Assert.Equal(value, item.FQDN);
    }

    [Fact]
    public void GetByIpAddress()
    {
        string ipAddr = "10.0.0.6";

        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();

        results = myServerController.GetByProperty("IPAddress", ipAddr);
        Assert.Equal("lilserver", results[0].Name);
    }

    [Fact]
    public void GetByRole()
    {
        string role = "IIS";

        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();

        results = myServerController.GetByProperty("Role", role);
        Assert.Equal("lilserver", results[0].Name);
    }

    [Fact]
    public void GetByEnv()
    {
        string env = "PROD";

        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();

        results = myServerController.GetByProperty("ENV", env);
        Assert.Equal("bigserver", results[0].Name);
    }

    [Fact]
    public void GetByStatus()
    {
        string status = "staging";

        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();

        results = myServerController.GetByProperty("Status", status);
        Assert.Equal("lilserver", results[0].Name);
    }

    [Fact]
    public void AddServer()
    {
        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();
        MyServer myServer = new MyServer("SomeServer", "Someserver.fake.domain", "10.0.0.10", 
                                     "Files", "PROD", "RedHat", "live", "SMB Server");

        myServerController.AddMyServer(myServer);
        results = myServerController.GetByProperty("FQDN", myServer.FQDN);
        Assert.Equal("SomeServer", results[0].Name);

        // Cleanup
        myServerController.DeleteMyServer(myServer);
    }

    [Fact]
    public void DeleteServer()
    {
        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();
        MyServer myServer = new MyServer("OtherServer", "OtherServer.fake.domain", "10.0.0.11", 
                                     "Files", "PROD", "RedHat", "live", "SMB Server");

        // Add Server
        myServerController.AddMyServer(myServer);
        results = myServerController.GetByProperty("FQDN", myServer.FQDN);
        Assert.Equal("OtherServer", results[0].Name);

        // Remove Server
        myServerController.DeleteMyServer(myServer);
        results = myServerController.GetByProperty("FQDN", myServer.FQDN);
        Assert.True(!results.Any());
    }

    [Fact]
    public void DeleteServerNoCase()
    {
        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();
        MyServer myServer = new MyServer("OtherServer2", "OtherServer2.fake.domain", "10.0.0.21", 
                                     "Files", "PROD", "RedHat", "live", "SMB Server");

        // Add Server
        myServerController.AddMyServer(myServer);
        results = myServerController.GetByProperty("FQDN", "oTHERsERVER2.fake.domaiN");
        Assert.Equal("OtherServer2".ToLower(), results[0].Name.ToLower());

        // Remove Server
        myServerController.DeleteMyServer(myServer);
        results = myServerController.GetByProperty("FQDN", myServer.FQDN);
        Assert.True(!results.Any());
    }

    [Fact]
    public void EditServerRecord()
    {
        MyServerController myServerController = new(connectionString);
        List<MyServer> results = new();
        MyServer myServer = new MyServer("OtherServer3", "OtherServer3.fake.domain", "10.0.0.31", 
                                     "SQL", "PROD", "Ubuntu", "staging", "Postgres");
        MyServer editServer = new MyServer();
        editServer.FQDN = "OtherServer3.fake.domain";
        editServer.IPAddress = "10.10.10.31";
        editServer.OperatingSystem = "Debian";

        // Add Server
        myServerController.AddMyServer(myServer);
        results = myServerController.GetByProperty("FQDN", "OtherServer3.fake.domain");
        Assert.Equal("OtherServer3".ToLower(), results[0].Name.ToLower());

        // Edit Server
        myServerController.EditMyServer(editServer);
        List<MyServer> newResults = myServerController.GetByProperty("FQDN", myServer.FQDN);

        // Test
        Assert.Equal(myServer.FQDN, newResults[0].FQDN);
        Assert.Equal(myServer.Name, newResults[0].Name);
        Assert.Equal(editServer.IPAddress, newResults[0].IPAddress);
        Assert.Equal(myServer.Role, newResults[0].Role);
        Assert.Equal(myServer.ENV, newResults[0].ENV);
        Assert.Equal(editServer.OperatingSystem, newResults[0].OperatingSystem);
        Assert.Equal(myServer.Status, newResults[0].Status);
        Assert.Equal(myServer.Notes, newResults[0].Notes);

        // Cleanup
        myServerController.DeleteMyServer(myServer);
        Thread.Sleep(500);
    }
}
