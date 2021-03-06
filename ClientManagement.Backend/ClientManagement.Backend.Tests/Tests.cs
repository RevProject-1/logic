﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ClientManagement.Backend.Client;
using ClientManagement.Backend.Logic;
using ClientManagement.Backend.Logic.Models;
using ClientManagement.Backend.Logic.ServiceReference1;
namespace ClientManagement.Backend.Tests
{
   
    public class Tests
    {
      appLogic myLogic = new appLogic();

      #region getaspnetuser tests
      [Fact]
      public void testGetAspNetUsers()
      {
         List<AspNetUsers> result = myLogic.getAspNetUsers();
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetAspNetUsersByName()
      {
         List<AspNetUsers> result = myLogic.getAspNetUsersByName("revature");
         Assert.NotNull(result);
      }
      #endregion

      #region getclient tests
      [Fact]
      public void testGetClients()
      {
         List<ClientDTO> result = myLogic.getClients();
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetClientsByName()
      {
         List<ClientDTO> result = myLogic.getClientsByName("derek");
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetClientsByPhoneNumber()
      {
         List<ClientDTO> result = myLogic.getClientsByPhoneNumber("1234567890");
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetClientsByEmail()
      {
         List<ClientDTO> result = myLogic.getClientsByEmail("A@B.coM");
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetClientsByAddressId()
      {
         List<ClientDTO> result = myLogic.getClientsByAddressId(1);
         Assert.NotNull(result);
      }
      [Fact]
      #endregion

      #region getaddress tests
      public void testGetAddresses()
      {
         List<AddressDTO> result = myLogic.getAddressDTOs();
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetAddressByZip()
      {
         var result = myLogic.getAddressDTOsByZip("22222");
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetAddressByState()
      {
         var result = myLogic.getAddressDTOsByState("vA");
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetAddressByCity()
      {
         var result = myLogic.getAddressDTOsByCity("reston");
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetAddressByStreet()
      {
         var result = myLogic.getAddressDTOsByStreet("123 main");
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetAddressByStreetandCity()
      {
         var result = myLogic.getAddressDTOsByStreetandCity("123 main", "reston");
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetAddressByStreetandCityandState()
      {
         var result = myLogic.getAddressDTOsByStreetandCityandState("123 main", "reston", "va");
         Assert.NotNull(result);
      }

      [Fact]
      public void testGetAddressByStreetandCityandStateandZip()
      {
         var result = myLogic.getAddressDTOsByStreetandCityandStateandZip("123 main", "reston", "va", "20190");
         Assert.NotNull(result);
      }
      #endregion

      #region insert and delete tests for client and aspnetuser
      [Fact]
      public void testInsertandDeleteClient()
      {
         ClientDTO testClient = new ClientDTO();
         testClient.Address = new AddressDTO();
         testClient.Address.City = "lewisburg";
         testClient.Address.State = "wv";
         testClient.Address.Street = "main st.";
         testClient.Address.Zip = "9999";
         testClient.Email = "a@b.z";
         testClient.Name = "joe blow";
         testClient.PhoneNumber = "111-222-3333";
         var result = myLogic.insertClients(testClient);
         //var result2 = myLogic.deleteClient(testClient);
         Assert.True(result /*&& result2*/);
      }

      [Fact]
      public void testInsertandDeleteAspNetUser()
      {
         AspNetUsers testUser = new AspNetUsers();

         testUser.City = "lewisburg";
         testUser.State = "wv";
         testUser.StreetAddress = "main st.";
         testUser.Zip = "99999";
         testUser.Email = "a@b.z";
         testUser.Name = "joe blow";
         testUser.PhoneNumber = "111-222-3333";
         testUser.AccessFailedCount = 0;
         testUser.EmailConfirmed = false;
         testUser.Id = "kjdfhdakvdalvbjn";
         testUser.LockoutEnabled = false;
         testUser.PasswordHash = "kjjbjafbjbva";
         testUser.PhoneNumberConfirmed = false;
         testUser.TwoFactorEnabled = false;
         testUser.UserName = "a@b.z";

         var result = myLogic.insertAspNetUsers(testUser);

         Assert.True(result);
      }
      #endregion

      #region job tests
      [Fact]
      public void testInsertJob()
      {
         var testjob = new jobDTO();
         testjob.client = myLogic.getClientsByName("Derek Geter").FirstOrDefault();
         testjob.type = myLogic.getServiceTypes().Where(m => m.Name.Equals("Butcher")).FirstOrDefault();
         testjob.user = myLogic.getAspNetUsersByName("My Company").FirstOrDefault();        
         testjob.ClientId = testjob.client.Id;
         testjob.UserId = testjob.user.Id;
         testjob.ServiceTypeId = testjob.type.Id;
         var result = myLogic.insertJob(testjob);
         Assert.True(result);
      }

      [Fact]
      public void testUpdateJob()
      {
         var testjob = myLogic.getJobs().ElementAt(2);
         testjob.Notes = "this job has been updated";
         var result = myLogic.updateJob(testjob);
         Assert.True(result);
      }

      [Fact]
      public void testDeleteJob()
      {
         var testjob = myLogic.getJobs().ElementAt(2);

         var result = myLogic.deleteJob(testjob);         
         Assert.True(result);
      }
      #endregion

      #region invoice tests
      [Fact]
      public void testGenerateInvoice()
      {
         var testjob = myLogic.getJobs().FirstOrDefault();
         testjob.StartDate = DateTime.Now;
         testjob.Notes = "job updated";
         var result = myLogic.updateJob(testjob);
         var result2 = myLogic.generateInvoice(testjob);
      } 
      #endregion
      
      #region expense tests
      [Fact]
      public void testInsertExpense()
      {
         var result = myLogic.insertExpense(new ExpenseDTO { Name = "test expense", Cost = 25.99m });
         var result2 = myLogic.insertJobExpense(myLogic.getJobs().FirstOrDefault(), myLogic.getExpenses().Where(m => m.Name.Equals("test expense")).FirstOrDefault());
         Assert.True(result && result2);
      } 
      #endregion


      [Fact]
      public void phoneformattertest()
      {
         var result = myLogic.phoneFormatter("(555)987-6541");
         var result2 = myLogic.phoneFormatter("555-987-6541");
         var result3 = myLogic.phoneFormatter("5559876541");
         Assert.IsType<StringBuilder>(result);
      }

      [Fact]
      public void stringFormatterTest()
      {
         var result = myLogic.stringFormatter("thiS iS a veRy crAzy lonG ciTy nAme");
         var result2 = myLogic.stringFormatter("st.ST");
         Assert.IsType<StringBuilder>(result);
      }
   }
}
