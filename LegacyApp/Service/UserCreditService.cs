namespace LegacyApp.Service
{
    [System.CodeDom.Compiler.GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContract(ConfigurationName = "LegacyApp.IUserCreditService")]
    public interface IUserCreditService
    {

        [System.ServiceModel.OperationContract(Action = "http://eqfx-real-service.com/IUserCreditService/GetCreditLimit", ReplyAction = "http://eqfx-real-service.com/IUserCreditService/GetCreditLimitResponse")]
        int GetCreditLimit(string firstname, string surname, System.DateTime dateOfBirth);
    }

    [System.CodeDom.Compiler.GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface IUserCreditServiceChannel : IUserCreditService, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThrough()]
    [System.CodeDom.Compiler.GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public partial class UserCreditServiceClient : System.ServiceModel.ClientBase<IUserCreditService>, IUserCreditService
    {

        public UserCreditServiceClient()
        {
        }

        public UserCreditServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public UserCreditServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public UserCreditServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public UserCreditServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public int GetCreditLimit(string firstname, string surname, System.DateTime dateOfBirth)
        {
            return Channel.GetCreditLimit(firstname, surname, dateOfBirth);
        }
    }
}
