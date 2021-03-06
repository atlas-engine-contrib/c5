﻿using System.IO;
using AtlasEngine.Modelling.C5.IO.Json;
using Xunit;

namespace AtlasEngine.Modelling.C5.Api.Tests.IO
{
    public class JsonReaderTests
    {
        
        [Fact]
        public void Test_DeserializationOfProperties() {
            Workspace workspace = new Workspace("Name", "Description");
            SoftwareSystem softwareSystem = workspace.Model.AddSoftwareSystem("Name", "Description");
            softwareSystem.AddProperty("Name", "Value");
            
            StringWriter stringWriter = new StringWriter();
            new JsonWriter(false).Write(workspace, stringWriter);
                        
            StringReader stringReader = new StringReader(stringWriter.ToString());
            workspace = new JsonReader().Read(stringReader);
            Assert.Equal("Value", workspace.Model.GetSoftwareSystemWithName("Name").Properties["Name"]);
        }
        
    }
}