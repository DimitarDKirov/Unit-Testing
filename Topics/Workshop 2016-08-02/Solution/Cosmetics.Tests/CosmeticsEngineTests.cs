using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cosmetics.Engine;
using System.IO;
using Cosmetics.Tests.Mocked;
using Moq;
using Cosmetics.Contracts;
using Cosmetics.Products;
using Cosmetics.Common;
using System.Collections.Generic;

namespace Cosmetics.Tests
{
    [TestClass]
    public class CosmeticsEngineTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Ignore]
        public void Start_ShouldThrowArgumentNullException_WhenInputIsNotValid()
        {
            var mockedFactory = new Mock<ICosmeticsFactory>();
            var mockedShoppingCart = new Mock<IShoppingCart>();

            var engine = new MockedCosmeticsEngine(mockedFactory.Object, mockedShoppingCart.Object);

            //Mock Console
            this.MockConsole("");

            engine.Start();
            //No exception thrown
        }

        [TestMethod]
        public void Start_ShouldExecuteCreateCategory_AndAddCategory()
        {
            ICategory testCategory = new Category("testCategory");
            var mockedFactory = new Mock<ICosmeticsFactory>();
            mockedFactory.Setup(f => f.CreateCategory(It.IsAny<String>())).Returns(testCategory);
            var mockedShoppingCart = new Mock<IShoppingCart>();
            var engine = new MockedCosmeticsEngine(mockedFactory.Object, mockedShoppingCart.Object);

            this.MockConsole("CreateCategory somecat");
            engine.Start();

            Assert.AreSame(testCategory, engine.Categories["somecat"]);
        }

        [TestMethod]
        public void Start_ShouldExecuteAddToCategory_AndAddProductToCategory()
        {
            IProduct shampoo = new Shampoo("test", "test", 0, GenderType.Unisex, 0, UsageType.Medical);
            var mockedFactory = new Mock<ICosmeticsFactory>();
            //var mockedCategory = new Mock<ICategory>();
            //mockedCategory.Setup(c => c.AddCosmetics(It.IsAny<IProduct>())).Verifiable();
            var mockedCategory = new Category("testCategory");
            var mockedShoppingCart = new Mock<IShoppingCart>();

            var engine = new MockedCosmeticsEngine(mockedFactory.Object, mockedShoppingCart.Object);
            engine.Categories.Add("testCategory", mockedCategory);
            engine.Products.Add("shampoo", shampoo);

            this.MockConsole("AddToCategory testCategory shampoo");
            engine.Start();
            //mockedCategory.Verify(c => c.AddCosmetics(It.Is<IProduct>(p => p==shampoo)));
            var prObj = new PrivateObject(mockedCategory);
            var productsInCategory =  prObj.GetField("products") as List<IProduct>;
            Assert.AreSame(shampoo, productsInCategory[0]);
        }

        [TestMethod]
        public void Start_ShouldExecuteRemoveFromCategory_AndRemoveProductFromCategory()
        {
            IProduct shampoo = new Shampoo("test", "test", 0, GenderType.Unisex, 0, UsageType.Medical);
            var mockedFactory = new Mock<ICosmeticsFactory>();
            var mockedCategory = new Mock<ICategory>();
            mockedCategory.Setup(c => c.RemoveCosmetics(It.IsAny<IProduct>())).Verifiable();

            var mockedShoppingCart = new Mock<IShoppingCart>();

            var engine = new MockedCosmeticsEngine(mockedFactory.Object, mockedShoppingCart.Object);
            engine.Categories.Add("testCategory", mockedCategory.Object);
            engine.Products.Add("shampoo", shampoo);

            this.MockConsole("RemoveFromCategory testCategory shampoo");
            engine.Start();
            mockedCategory.Verify(c => c.RemoveCosmetics(shampoo));
        }

        [TestMethod]
        public void Start_ShouldExecuteShowCategory_AndCallPrint()
        {
            var mockedFactory = new Mock<ICosmeticsFactory>();
            var mockedCategory = new Mock<ICategory>();

            var mockedShoppingCart = new Mock<IShoppingCart>();

            var engine = new MockedCosmeticsEngine(mockedFactory.Object, mockedShoppingCart.Object);
            engine.Categories.Add("testCategory", mockedCategory.Object);

            this.MockConsole("ShowCategory testCategory");
            engine.Start();

            mockedCategory.Verify(c => c.Print(), Times.Once());
        }

        private void MockConsole(string input)
        {
            //Mock Console
            var str = new StringReader(input);
            Console.SetIn(str);
        }
    }
}
