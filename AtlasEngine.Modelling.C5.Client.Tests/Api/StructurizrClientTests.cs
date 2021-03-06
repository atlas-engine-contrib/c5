﻿using System;
using Xunit;

namespace AtlasEngine.Modelling.C5.Api.Tests
{
    public class StructurizrClientTests
    {
        private StructurizrClient _structurizrClient;

        [Fact]
        public void Test_Construction_WithTwoParameters()
        {
            _structurizrClient = new StructurizrClient("key", "secret");
            Assert.Equal("https://api.structurizr.com", _structurizrClient.Url);
            Assert.Equal("key", _structurizrClient.ApiKey);
            Assert.Equal("secret", _structurizrClient.ApiSecret);
        }

        [Fact]
        public void Test_Construction_WithThreeParameters()
        {
            _structurizrClient = new StructurizrClient("https://localhost", "key", "secret");
            Assert.Equal("https://localhost", _structurizrClient.Url);
            Assert.Equal("key", _structurizrClient.ApiKey);
            Assert.Equal("secret", _structurizrClient.ApiSecret);
        }

        [Fact]
        public void Test_Construction_WithThreeParameters_TruncatesTheApiUrl_WhenTheApiUrlHasATrailingSlashCharacter()
        {
            _structurizrClient = new StructurizrClient("https://localhost/", "key", "secret");
            Assert.Equal("https://localhost", _structurizrClient.Url);
            Assert.Equal("key", _structurizrClient.ApiKey);
            Assert.Equal("secret", _structurizrClient.ApiSecret);
        }

        [Fact]
        public void Test_Construction_ThrowsAnException_WhenANullApiKeyIsUsed()
        {
            try
            {
                _structurizrClient = new StructurizrClient(null, "secret");
                throw new TestFailedException();
            }
            catch (ArgumentException iae)
            {
                Assert.Equal("The API key must not be null or empty.", iae.Message);
            }
        }

        [Fact]
        public void Test_Construction_ThrowsAnException_WhenAnEmptyApiKeyIsUsed()
        {
            try
            {
                _structurizrClient = new StructurizrClient(" ", "secret");
                throw new TestFailedException();
            }
            catch (ArgumentException iae)
            {
                Assert.Equal("The API key must not be null or empty.", iae.Message);
            }
        }

        [Fact]
        public void Test_Construction_ThrowsAnException_WhenANullApiSecretIsUsed()
        {
            try
            {
                _structurizrClient = new StructurizrClient("key", null);
                throw new TestFailedException();
            }
            catch (ArgumentException iae)
            {
                Assert.Equal("The API secret must not be null or empty.", iae.Message);
            }
        }

        [Fact]
        public void Test_Construction_ThrowsAnException_WhenAnEmptyApiSecretIsUsed()
        {
            try
            {
                _structurizrClient = new StructurizrClient("key", " ");
                throw new TestFailedException();
            }
            catch (ArgumentException iae)
            {
                Assert.Equal("The API secret must not be null or empty.", iae.Message);
            }
        }

        [Fact]
        public void Test_Construction_ThrowsAnException_WhenANullApiUrlIsUsed()
        {
            try
            {
                _structurizrClient = new StructurizrClient(null, "key", "secret");
                throw new TestFailedException();
            }
            catch (ArgumentException iae)
            {
                Assert.Equal("The API URL must not be null or empty.", iae.Message);
            }
        }

        [Fact]
        public void Test_Construction_ThrowsAnException_WhenAnEmptyApiUrlIsUsed()
        {
            try
            {
                _structurizrClient = new StructurizrClient(" ", "key", "secret");
                throw new TestFailedException();
            }
            catch (ArgumentException iae)
            {
                Assert.Equal("The API URL must not be null or empty.", iae.Message);
            }
        }

        [Fact]
        public void Test_GetWorkspace_ThrowsAnException_WhenTheWorkspaceIdIsNotValid()
        {
            try
            {
                _structurizrClient = new StructurizrClient("key", "secret");
                _structurizrClient.GetWorkspace(0);
                throw new TestFailedException();
            }
            catch (ArgumentException iae)
            {
                Assert.Equal("The workspace ID must be a positive integer.", iae.Message);
            }
        }

        [Fact]
        public void Test_PutWorkspace_ThrowsAnException_WhenTheWorkspaceIdIsNotValid()
        {
            try
            {
                _structurizrClient = new StructurizrClient("key", "secret");
                _structurizrClient.PutWorkspace(0, new Workspace("Name", "Description"));
                throw new TestFailedException();
            }
            catch (ArgumentException iae)
            {
                Assert.Equal("The workspace ID must be a positive integer.", iae.Message);
            }
        }

        [Fact]
        public void Test_PutWorkspace_ThrowsAnException_WhenANullWorkspaceIsSpecified()
        {
            try
            {
                _structurizrClient = new StructurizrClient("key", "secret");
                _structurizrClient.PutWorkspace(1234, null);
                throw new TestFailedException();
            }
            catch (ArgumentException iae)
            {
                Assert.Equal("The workspace must not be null.", iae.Message);
            }
        }
    }
}