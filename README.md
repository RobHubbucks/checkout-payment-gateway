# checkout-payment-gateway
checkout.com technical challenge

# Running the app from Visual Studio
* Set multiple startup projects: Checkout.PaymentGateway.Api and Checkout.PaymentGateway.AcquiringBankSimulator
* Hit F5, on first run accept SSL certificates to avoid SSL warnings

# Sample requests

Successful transaction 1:
```
{
  "cardDetails": {
    "expiryMonth": 4,
    "expiryYear": 2025,
    "number": "4242424242424242",
    "cvv": "123"
  },
  "currency": "GBP",
  "amount": 110,
  "merchantReference": "string"
}
```

Successful transaction 2 (full details supplied and all match account):
```
{
  "cardDetails": {
    "expiryMonth": 1,
    "expiryYear": 2027,
    "cardholderName": "D. Customer",
    "number": "345678901234564",
    "cvv": "3333"
  },
  "billingAddress": {
    "address1": "11 C Road",
    "address2": "",
    "postcode": "EC3 WA1",
    "city": "London",
    "country": "UK"
  },
  "currency": "GBP",
  "amount": 1000,
  "merchantReference": "abcd-1234-bgfd-3211"
}
```

Successful transaction 3 (minimum required inputs):
```
{
  "cardDetails": {
    "expiryMonth": 8,
    "expiryYear": 2025,
    "number": "5305484748800098"
  },
  "currency": "GBP",
  "amount": 1111,
  "merchantReference": "dsadfsdfsdlkewl123"
}
```

Unsuccessful transaction 1 (configured account doesn't have enough money):
```
{
  "cardDetails": {
    "expiryMonth": 7,
    "expiryYear": 2025,
    "cardholderName": "B. Customer",
    "number": "4659105569051157",
    "cvv": "234"
  },
  "billingAddress": {
    "address1": "33 B Road",
    "address2": "",
    "postcode": "EC2 WA2",
    "city": "London",
    "country": "UK"
  },
  "currency": "GBP",
  "amount": 100,
  "merchantReference": "aaaa-1111-bbbb-2222"
}
```

Unsuccessful transaction 2 (card expired):
```
{
  "cardDetails": {
    "expiryMonth": 12,
    "expiryYear": 2020,
    "cardholderName": "C. Customer",
    "number": "5436031030606378",
    "cvv": "345"
  },
  "billingAddress": {
    "address1": "22 C Road",
    "address2": "",
    "postcode": "EC3 WA3",
    "city": "London",
    "country": "UK"
  },
  "currency": "GBP",
  "amount": 100,
  "merchantReference": "1234-6543-jhfd-0432"
}
```

Unsuccessful transaction 3 (billing address supplied but does not match account):
```
{
  "cardDetails": {
    "expiryMonth": 5,
    "expiryYear": 2024,
    "cardholderName": "E. Customer",
    "number": "341829238058580",
    "cvv": "4444"
  },
  "billingAddress": {
    "address1": "An incorrect road",
    "address2": "",
    "postcode": "ABC 123",
    "city": "Manchester",
    "country": "USA"
  },
  "currency": "GBP",
  "amount": 100,
  "merchantReference": "nbcx-dfsa-2134"
}
```

Any payment ID given in response to the above can then be used in the payments/GET endpoint to retrieve the payment details. Note that a couple of requests will fail immediate validation (e.g. expired card) and will not generate a payment ID.

# Project structure/Architecture
#### Checkout.PaymentGateway.Api
This is the payment gateway API. It contains two endpoints (post & get payment) as documented by swagger. This project follows a simple CQRS style pattern where the general logic flow is: Request -> DTO -> Command/Query -> Command/Query Handler -> Service -> External API (simulator) + Repository.

#### Checkout.PaymentGateway.DataAccess
This contains repository interfaces/implementation. For this project the only implemented repository is a simple in-memory cache.

#### Checkout.PaymentGateway.Api.Tests.Unit
Unit tests covering the various components of the payment gateway API.

#### Checkout.PaymentGateway.AcquiringBankSimulator
A mock acquiring bank simulator. It exposes a single API endpoint to make a payment. It holds an in memory cache of mock customer banks that each hold a set of mock bank accounts, against which incoming payment requests are validated following simple rules. This cache can be updated by editing the file 'Resources/Banks.json' before running the simulator. The banks/accounts are not currently stateful but could be by making the bank a singleton service. Note that by default a browser window will not launch when debugging this project.

#### Checkout.PaymentGateway.AcquiringBankSimulator.Tests.Unit
Unit tests covering the main functionality of the acquiring bank simulator.

# Deployment thoughts
Assuming we were wanting to host this in Azure I would:
* Deploy an app service plan (with scaleout enabled if deemed necessary) to host the API. These plans are flexible so we can change the level of resources at any time according to our needs.
* Deploy a managed SQL instance for the database.
* Put the above inside of a VPN, which will stop them being exposed directly to the internet.
* Deploy an API gateway to forward requests to the payment gateway API. This will give us load balancing (if we need it), authentication and various other benefits.
Duplicate everything to a second region for redunancy - We would need to replicate the data in our database across both regions.
* I would use terraform to create the infrastructure, this would allow us to rapidly deploy our entire product in case of expansion or distaster recovery.

# Areas for improvement/missing bits
* Authentication: Due to time constraints there is no authentication on either API. In reality we'd want to use some form of machine to machine authentication using an API key.
* Data storage: Obviously instead of an in-memory repository we'd want to store our payments in a database (probably alongside other data e.g. we'd likely want to store customer details as well as prior payments). A relational database e.g. SQL server would be my first choice for this.
* Asynchronous payments: In the case of payments that require some sort of customer interaction e.g. 3DS we would want an async payment flow.
* Validation: We aren't really validating that customer billing addresses actually exist. We also aren't trying to do any currency conversion (or anything with currency other than validating that the currency code is real).
* Project structure: We'd probably want to move some of the classes in the main API project out into class libraries for reuse purporses, e.g. everything in the Service folder.
* Tests: Integration tests would be helpful but due to time constraints there aren't any. They would take the form of self hosting the payment gateway API and running requests through it that communicated with an externally hosted bank simulator.
* Misc: The bank simulator response is just a status code, we'd probably want to include payment IDs and other bits in there ideally.
