Black Scholes Model Implementation in C#

Overview
This project provides an implementation of the Black Scholes Model using C#. The Black Scholes Model is a widely used mathematical model for pricing options contracts. With this implementation, 
you can calculate the premiums for both Call (CE) and Put (PE) options based on different parameters such as stock price, strike price, time period, rate of interest, and volatility.

How to Use
To calculate the premium for a Call option using the Black Scholes Model, you can use the Call_bsm method provided in the code. Here's a brief overview of the parameters:

StockPrice: Current price of the underlying stock.
StrikePrice: Price at which the option can be exercised.
TimePeriod: Time to expiration of the option, in years.
RateofInterest: Annual risk-free interest rate.
roh_Volatility: Volatility of the underlying stock.

## 
WinForms Interface: The application utilizes WinForms, part of the .NET Framework, to provide an interactive interface for users to input instrument values and obtain option premiums.
<img width="512" alt="BSM" src="https://github.com/Coderixc/BlackScholesModel/assets/40321363/baa2e0bf-afa0-4040-9c40-51beccce4589">

## Note:
Ensure that all input parameters are accurate and consistent with financial market conventions to obtain reliable results.
In case of any errors during computation, the application handles exceptions and provides appropriate error messages.

## Contributions
Contributions and feedback are welcome! If you find any bugs, have suggestions for improvements, or want to contribute additional features, feel free to open an issue or submit a pull request.
