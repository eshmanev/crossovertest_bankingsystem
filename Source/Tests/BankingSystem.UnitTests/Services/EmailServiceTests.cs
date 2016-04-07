using System;
using System.Collections.Generic;
using System.Net.Mail;
using BankingSystem.Common.Data;
using BankingSystem.LogicTier;
using BankingSystem.LogicTier.Impl;
using BankingSystem.LogicTier.Utils;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.UnitTests.Services
{
    [TestFixture]
    public class EmailServiceTests
    {
        private EmailService _service;
        private FakeDatabaseContext _context;
        private Mock<ISmtpFactory> _smtpFactory;

        [SetUp]
        public void Setup()
        {
            _context = new FakeDatabaseContext();
            _smtpFactory = new Mock<ISmtpFactory>();
            _service = new EmailService(_context, _smtpFactory.Object);
        }

        [Test]
        public void ShouldScheduleEmail()
        {
            // act
            var result = _service.ScheduleEmail("recipient", "subject", "body");

            // assert
            result.ShouldNotBeNull();
            result.RecipientAddress.ShouldBe("recipient");
            result.Subject.ShouldBe("subject");
            result.Body.ShouldBe("body");
            result.ScheduledDateTime.ShouldBeGreaterThan(DateTime.UtcNow.AddMinutes(-5));
            _context.ScheduledEmails.Verify(x => x.Insert(result));
        }

        [Test]
        public void ShouldDeliverEmails()
        {
            // ARRANGE
            var settings = new SmtpSettings {FromAddress = "from@test.com"};
            var smtpClient = new Mock<ISmtpClient>();
            _smtpFactory.Setup(x => x.CreateClient(settings)).Returns(smtpClient.Object);

            // first message will be sent, second message won't be sent and will remains scheduled
            var scheduledEmails = new List<IScheduledEmail>
            {
                Mock.Of<IScheduledEmail>(x => x.RecipientAddress == "success@test.com" && x.Subject == "subject" && x.Body == "body"),
                Mock.Of<IScheduledEmail>(x => x.RecipientAddress == "failure@test.com" && x.Subject == "subject" && x.Body == "body")
            };
            _context.ScheduledEmails.Setup(x => x.GetAll()).Returns(scheduledEmails);

            // this collection will contain a list of sent messages
            var sentMessage = new List<MailMessage>();

            smtpClient
                .Setup(x => x.Send(It.IsAny<MailMessage>()))
                .Callback((MailMessage m) =>
                {
                    if (m.To.ToString() == "failure@test.com")
                        throw new SmtpException();

                    sentMessage.Add(m);
                });

            // all sent emails should be moved to Delivered table
            var deliveredEmails = new List<IDeliveredEmail>();
            _context.DeliveredEmails
                .Setup(x => x.Insert(It.IsAny<IDeliveredEmail>()))
                .Callback((IDeliveredEmail e) => deliveredEmails.Add(e));

            // ACT
            _service.DeliverEmails(settings);

            // ASSERT
            sentMessage.Count.ShouldBe(1);
            sentMessage[0].To.ToString().ShouldBe(scheduledEmails[0].RecipientAddress);
            sentMessage[0].Subject.ShouldBe(scheduledEmails[0].Subject);
            sentMessage[0].Body.ShouldBe(scheduledEmails[0].Body);

            // check updates
            _context.ScheduledEmails.Verify(x => x.Update(scheduledEmails[0]), Times.Never);
            _context.ScheduledEmails.Verify(x => x.Update(scheduledEmails[1]), Times.Once);

            // check deletes
            _context.ScheduledEmails.Verify(x => x.Delete(scheduledEmails[0]), Times.Once);
            _context.ScheduledEmails.Verify(x => x.Delete(scheduledEmails[1]), Times.Never);

            // check Delivered table
            deliveredEmails.Count.ShouldBe(1);
            deliveredEmails[0].RecipientAddress.ShouldBe(scheduledEmails[0].RecipientAddress);
            deliveredEmails[0].Subject.ShouldBe(scheduledEmails[0].Subject);
            deliveredEmails[0].Body.ShouldBe(scheduledEmails[0].Body);
            deliveredEmails[0].DeliveredDateTime.ShouldBeGreaterThan(DateTime.UtcNow.AddMinutes(-5));
        }
    }
}