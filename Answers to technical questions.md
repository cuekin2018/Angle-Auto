- How long did you spend on the coding test? What would you add to your solution if you had more time? If you didn't spend much time on the coding test then use this as an opportunity to explain what you would add.

  	- All up around 3 hours (on and off).
	- Ability to have DataAccess dependencies removed.
	- More comprehensive test cases to cover complex use cases. 
	- Test cases on other components of the solution.

- What was the most useful feature that was added to the latest version of C#?
	- Primary Constructors.

- How would you track down an issue with this in production, assuming this api would be deployed as an app service in Azure.
	- Since this legacy app invoke wcf service, firstly we need to identify the binding & endpoint configuration.
	- The next thing to investigate is the authentication/authorization part.
	- To have better visibility, ensure the Azure Application Insights is enabled.
	- We can also utilise the Azure App Service Diagnostics to get information grouped into categories which can assist in discovering information.
