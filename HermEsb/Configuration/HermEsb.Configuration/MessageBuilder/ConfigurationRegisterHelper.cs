using HermEsb.Configuration.Builder.Registration;
using HermEsb.Core.Messages.Builders;

namespace HermEsb.Configuration.MessageBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationRegisterHelper<TConfigurationHelper>
    {
        private readonly TConfigurationHelper _configurationHelper;
        private readonly IMessageBuilder _messageBuilder;
        private readonly IRegister _register;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationRegisterHelper"/> class.
        /// </summary>
        /// <param name="configurationHelper">The configuration helper.</param>
        /// <param name="messageBuilder">The message builder.</param>
        /// <param name="register">The register.</param>
        public ConfigurationRegisterHelper(TConfigurationHelper configurationHelper, IMessageBuilder messageBuilder, IRegister register)
        {
            _configurationHelper = configurationHelper;
            _messageBuilder = messageBuilder;
            _register = register;
        }

        /// <summary>
        /// Alls the types.
        /// </summary>
        /// <returns></returns>
        public TConfigurationHelper AllTypes()
        {
            _messageBuilder.Register(_register.AllTypes());
            return _configurationHelper;
        }

        /// <summary>
        /// Alls the interface to class.
        /// </summary>
        /// <returns></returns>
        public TConfigurationHelper AllInterfaceToClass()
        {
            _messageBuilder.Register(_register.AllInterfaceToClass());
            return _configurationHelper;
        }

    }
}