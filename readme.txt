1. PREREQUISITES

   The solution is built on Visual Studio 2015, Microsoft .NET Framework 4.5.2 and 
   Microsoft SQL Server 2014 Express, therefore before configure and run the application 
   please ensure you have installed the following tools:

   .Net 4.5.2
   Visual Studio 2015
   Microsoft SQL Server 2012 or later
   IIS

   To run intergration tests you also need to install SpecFlow for Visual Studio 2015

2. HOW TO CREATE AND INITIALIZE THE DATABASE
   
   Open SQL Server 2014 Management Studio
   Open .\Source\SQL\apply_me.sql
   Run the script

3. HOW TO BUILD THE SOLUTION
   
   Run Visual Studio 2015 
   Open .\Source\BankingSystem.sln 
   Build the solution.

   NOTE: MSBuild cannot be used because EnableNugetPackageRestore flag was removed in
   Visual Studio 2015, therefore MSBuild cannot automaticaly restore packages.

4. HOW TO CONFIGURE AND RUN
      
   A. Run Web Application
     Run IIS and add a new website:
        Phisycal path:  Source\BankingSystem.WebPortal
        Type:           HTTPS
        Host name:      localhost
        Port:           44300
     Browse to https://localhost:44300
     Login with the following credentials evgeny.shmanev/test (see apply_me.sql for more logins)

   B. Run Notification Service
     Run BankingSystem.NotificationService\bin\debug\BankingSystem.NotificationService.exe

     NOTE: You can install the service as a Windows service. For more information see
     http://docs.topshelf-project.com/en/latest/overview/commandline.html

   C. Run ATM Application
     Run BankingSystem.ATM\bin\debug\BankingSystem.ATM.exe

   D. Online Payment
     Navigate to https://localhost:44300/onlinepayment/5D9168B6-A03B-412E-A5D9-1AC5C5105BA9/usd/1000?redirectUrl=http://mystore.localhost.ru
     Where: 
       5D9168B6-A03B-412E-A5D9-1AC5C5105BA9 is a GUID which identifies the merchant.
       USD represents a currency of the merchant's account to credit. Merchant may contain few accounts with different currencies.
       1000 is a payment sum.
       redirectUrl represents an URL on which the request should be redirected when payment is submitted.
    Security code is 123 for all cards.

5. WHAT IS NOT COVERED

    You cannot login with Twitter account, because Twitter requires a valid SSL certificate and does not
    support development URLs. But in fact Twitter OAuth is implemented. If you have a valid certificate you
    can check it.

6. FOUND ISSUES

   ISSUE 1: Exchange rates
     I was unable to find any free external service which provides exchange rates with full
     functionality. Most of free services support only conversion from USD and a limited
     number of requrests per month.
   
     I decided to prepay for one month access on https://currencylayer.com/dashboard which
     expires on 30 April 2016. If you have any issues with conversions between non USD 
     currencies please ensure that your have prepaid Basic Plan subscription; otherwise, 
     conversion operation is possible only from USD.

     Credentials: 
       Login URL: https://currencylayer.com/dashboard
       email: evgeny.shmanev@aurea.com
       password: testevgeny

   ISSUE 2: Twitter. 
     Twitter does requre a valid SSL certificate and does not support development URLs like https://localhost:43000
     To manage it to work you need to run the application with a real URL and create a valid SSL certificate.

7. FEEDBACK

   This task is better than previous one. It is more structured and described, but
   some issues described in the previous paragraph can be stressful.