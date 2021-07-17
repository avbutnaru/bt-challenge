# bt-challenge

A few notes about the solution:
- I used class libraries created in VS 2019, tried to keep it as simple as possible while aiming for good testability and meeting of requirements
- I did not use Dependency Injection here, but in a real project where these classes could be integrated DI would be mandatory
- The entry points into the functionality are in the OneTimePass.Service project: GeneratePasswordTask generates the password, ValidatePasswordTask validates it
- I realize that ValidatePasswordTask using an IList<IPasswordValidator> is a bit too much for such simple requirements, but it seemed like a nice idea to present for this challenge and also provides a very simple mechanism for extensibility in case we need to add more validation rules
- For this requirement: "input should be the following two pieces information: User Id and Date time" I assumed that these inputs would be provided by services (current user, current time, etc.), so for these roles I created 2 interfaces: IUserProvider and ICurrentTimeProvider
- PasswordGenerator simply concatenates the user id and date time, in a real project this algorithm would of course be much more sophisticated
- IPasswordStore could be a relational database, a NoSQL database, etc., but for this example I made a very simple in-memory implementation
- ISettingsProvider could also be stored in a config file, relational database, etc., for this example I chose the simplest solution

