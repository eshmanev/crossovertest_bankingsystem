using System;
using BankingSystem.Common.Data;
using BankingSystem.LogicTier.Impl;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.UnitTests.Services
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private FakeDatabaseContext _context;
        private CustomerService _service;

        [SetUp]
        public void Setup()
        {
            _context = new FakeDatabaseContext();
            _service = new CustomerService(_context);
        }

        [Test]
        public void ShouldFindCustomerByName()
        {
            // arrange
            var customer = Mock.Of<ICustomer>();
            _context.Customers.Setup(x => x.FindByUserName("testname")).Returns(customer);

            // act
            var result = _service.FindCustomerByName("testname");

            // assert
            result.ShouldBe(customer);
        }

        [Test]
        public void ShouldFindCustomerByEmail()
        {
            // arrange
            var customer = Mock.Of<ICustomer>();
            _context.Customers.Setup(x => x.FindByEmail("test@com")).Returns(customer);

            // act
            var result = _service.FindCustomerByEmail("test@com");

            // assert
            result.ShouldBe(customer);
        }

        [Test]
        public void ShouldFindCustomerById()
        {
            // arrange
            var customer = Mock.Of<ICustomer>();
            _context.Customers.Setup(x => x.GetById(100)).Returns(customer);

            // act
            var result = _service.FindCustomerById(100);

            // assert
            result.ShouldBe(customer);
        }

        [Test]
        public void ShouldFindCustomerByAccount()
        {
            // arrange
            var customer = Mock.Of<ICustomer>();
            _context.Customers.Setup(x => x.FindByAccount("12345")).Returns(customer);

            // act
            var result = _service.FindCustomerByAccount("12345");

            // assert
            result.ShouldBe(customer);
        }

        [Test]
        public void ShouldGetAvailableLogins_Fail()
        {
            // act
            var result = _service.GetAvailableLogins(100);

            // assert
            result.ShouldBe(new ILoginInfo[0]);
        }

        [Test]
        public void ShouldGetAvailableLogins_Success()
        {
            // arrange
            var customer = Mock.Of<ICustomer>(x => x.Logins == new[] {Mock.Of<ILoginInfo>()});
            _context.Customers.Setup(x => x.GetById(100)).Returns(customer);

            // act
            var result = _service.GetAvailableLogins(100);

            // assert
            result.ShouldBe(customer.Logins);
        }

        [Test]
        [TestCase(100)]
        [TestCase(99, ExpectedException = typeof(ArgumentException))]
        public void ShouldAddCustomerLogin(int customerId)
        {
            // arrange
            var customer = new Mock<ICustomer>();
            _context.Customers.Setup(x => x.GetById(100)).Returns(customer.Object);

            ILoginInfo addedLogin = null;
            customer.Setup(x => x.AddLogin(It.IsAny<ILoginInfo>())).Callback((ILoginInfo i) => addedLogin = i);

            // act
            _service.AddCustomerLogin(customerId, "provider", "login");

            // assert
            addedLogin.ShouldNotBeNull();
            addedLogin.ProviderName.ShouldBe("provider");
            addedLogin.LoginKey.ShouldBe("login");
            _context.Customers.Verify(x => x.Update(customer.Object));
        }

        [Test]
        [TestCase(100)]
        [TestCase(99, ExpectedException = typeof(ArgumentException))]
        public void ShouldRemoveCustomerLogin(int customerId)
        {
            // arrange
            var customer = new Mock<ICustomer>();
            _context.Customers.Setup(x => x.GetById(100)).Returns(customer.Object);

            ILoginInfo login = Mock.Of<ILoginInfo>(x => x.ProviderName == "provider" && x.LoginKey == "login");
            customer.Setup(x => x.Logins).Returns(new[] { login });

            // act
            var result = _service.RemoveCustomerLogin(customerId, "provider", "login");

            // assert
            result.ShouldBe(login);
            customer.Verify(x => x.RemoveLogin(login));
            _context.Customers.Verify(x => x.Update(customer.Object));
        }

        [Test]
        public void ShouldFindCustomerByLogin()
        {
            // arrange
            ILoginInfo login = Mock.Of<ILoginInfo>(x => x.ProviderName == "provider" && x.LoginKey == "login" && x.Customer == Mock.Of<ICustomer>());
            _context.LoginInfos.Setup(x => x.FindByProviderAndLogin("provider", "login")).Returns(login);

            // act
            var result1 = _service.FindCustomerByLogin("provider", "login");
            var result2 = _service.FindCustomerByLogin("provider", "invalid");
            var result3 = _service.FindCustomerByLogin("invalid", "login");

            // assert
            result1.ShouldBe(login.Customer);
            result2.ShouldBeNull();
            result3.ShouldBeNull();
        }

        [Test]
        public void ShouldValidatePassword()
        {
            // arrange

            // 'test' encrypted by SHA256
            var customer = Mock.Of<ICustomer>(x => x.PasswordHash == "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08");
            _context.Customers.Setup(x => x.FindByUserName("user")).Returns(customer);

            // act
            var result1 = _service.ValidatePassword("user", "test");
            var result2 = _service.ValidatePassword("invalid user", "test");
            var result3 = _service.ValidatePassword("user", "invalid");

            // assert
            result1.ShouldBeTrue();
            result2.ShouldBeFalse();
            result3.ShouldBeFalse();
        }

        [Test]
        public void ShouldUpdateCustomerEmail()
        {
            // arrange
            var customer = new Mock<ICustomer>();
            _context.Customers.Setup(x => x.GetById(100)).Returns(customer.Object);

            // act
            _service.UpdateCustomerEmail(100, "test@test.com");

            // assert
            customer.VerifySet(x => x.Email = "test@test.com");
            _context.Customers.Verify(x => x.Update(customer.Object));
        }
    }
}