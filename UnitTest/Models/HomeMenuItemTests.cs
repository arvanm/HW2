using System;
using System.Collections.Generic;
using System.Text;
using Mine.Models;
using NUnit.Framework;

namespace UnitTest.Models
{
    [TestFixture]
    public class HomeMenuItemTests
    {
        [Test]
        public void HomeMenuItem_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new HomeMenuItem();
            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void HomeMenuItem_Set_Get_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new HomeMenuItem();
            result.Title = "Title";
            result.Id = MenuItemType.Items;


            // Reset

            // Assert
            Assert.AreEqual("Title", result.Title);
            Assert.AreEqual(MenuItemType.Items, result.Id);
        }

    }

}

