using System;
using System.Linq;
using AtlasEngine.Modelling.C5.Config;
using Xunit;

namespace AtlasEngine.Modelling.C5.Core.Tests.Configuration
{
    public class ConfigurationTests
    {
        
        [Fact]
        public void Test_AddUser_ThrowsAnException_WhenANullUsernameIsSpecified() 
        {
            try
            {
                WorkspaceConfiguration configuration = new WorkspaceConfiguration();
                configuration.AddUser(null, Role.ReadWrite);
                throw new TestFailedException();
            }
            catch (ArgumentException iae)
            {
                Assert.Equal("A username must be specified.", iae.Message);
            }
        }

        [Fact]
        public void Test_AddUser_ThrowsAnException_WhenAnEmptyUsernameIsSpecified()
        {
            try
            {
                WorkspaceConfiguration configuration = new WorkspaceConfiguration();
                configuration.AddUser(" ", Role.ReadWrite);
                throw new TestFailedException();
            }
            catch (ArgumentException iae)
            {
                Assert.Equal("A username must be specified.", iae.Message);
            }
        }

        [Fact]
        public void Test_AddUser_AddsAUser()
        {
            WorkspaceConfiguration configuration = new WorkspaceConfiguration();
            configuration.AddUser("user@domain.com", Role.ReadOnly);

            Assert.Equal(1, configuration.Users.Count);
            Assert.Equal("user@domain.com", configuration.Users.First().Username);
            Assert.Equal(Role.ReadOnly, configuration.Users.First().Role);
        }
        
    }
}