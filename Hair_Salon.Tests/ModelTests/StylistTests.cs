using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTests : IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
        }

        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=kayla_ondracek_test";
        }

        //update when Save method is created
        [TestMethod]
        public void DeleteAll_RemoveAllStylists_Void()
        {
            //arrange
            Stylist newStylist = new Stylist("Carol", "Smith", "Hair");

            //act
            Stylist.DeleteAll();
            int result = Stylist.GetAllStylists().Count;

            //assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAllStylists_DBEmptyAtFirst_0()
        {
            //arrange, act
            int result = Stylist.GetAllStylists().Count;

            //assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Getters_GettersReturnAppropriately_StringsAndInts()
        {
            //arrange
            Stylist newStylist = new Stylist("Carol", "Smith", "Curly Hair", 1);
            string controlFirst = "Carol";
            string controlLast = "Smith";
            int controlId = 1;
            string controlSpecialty = "Curly Hair";

            //act
            string resultFirst = newStylist.GetFirstName();
            string resultLast = newStylist.GetLastName();
            int resultId = newStylist.GetId();
            string resultSpecialty = newStylist.GetSpecialty();

            //assert
            Assert.AreEqual(controlFirst, resultFirst);
            Assert.AreEqual(controlLast, resultLast);
            Assert.AreEqual(controlId, resultId);
            Assert.AreEqual(controlSpecialty, resultSpecialty);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfFirstNameSame_Stylist()
        {
            //arrange, act
            Stylist firstStylist = new Stylist("Carol", "Smith", "Curly Hair", 1);
            Stylist secondStylist = new Stylist("Carol", "Smith", "Curly Hair", 1);

            //assert
            Assert.AreEqual(firstStylist, secondStylist);
        }

        [TestMethod]
        public void Save_SavesToDatabase_StylistList()
        {
            //arrange
            Stylist newStylist = new Stylist("Carol", "Smith", "Curly Hair");

            //act
            newStylist.Save();
            List<Stylist> result = Stylist.GetAllStylists();
            Console.WriteLine(result.Count);
            List<Stylist> testList = new List<Stylist>{newStylist};
            Console.WriteLine(testList.Count);

            //assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
            //arrange
            Stylist newStylist = new Stylist("Carol", "Smith", "Curly Hair");

            //act
            newStylist.Save();
            Stylist savedStylist = Stylist.GetAllStylists()[0];
            int result = savedStylist.GetId();
            int testId = newStylist.GetId();

            //assign
            Assert.AreEqual(result, testId);
        }
    }
}
