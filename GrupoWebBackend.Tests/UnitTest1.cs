using System;
using System.Collections.Generic;
using GrupoWebBackend.DomainDistrict.Domain.Models;
using GrupoWebBackend.DomainReport;
using GrupoWebBackend.Security.Controllers;
using GrupoWebBackend.Security.Domain.Entities;
using NUnit.Framework;
using Org.BouncyCastle.Crypto.Macs;

namespace GrupoWebBackend.Tests
{
    public class Tests
    {
        private User _user;
        [SetUp]
        public void Setup()
        {
            _user = new User();
        }

        [Test]
        public void UserIsAuthenticatedTest()
        {
            _user = new User{
                Id = 0,
                Name = "Pablo",
                LastName = "Marmol",
                Dni = "01010101",
                Ruc = "10101010101",
                Email = "marmol@gmail.com",
                Phone = "987654321",
                District = new District(),
                Reports = new List<Report>()
            };
            Assert.AreEqual(true, _user.IsAuthenticated());
        }
        
        [Test]
        public void UserIsNotReportedTest()
        {
            _user.Reports.Add(new Report());
            Assert.AreEqual(true, _user.IsReported());
        }
        
    }
}