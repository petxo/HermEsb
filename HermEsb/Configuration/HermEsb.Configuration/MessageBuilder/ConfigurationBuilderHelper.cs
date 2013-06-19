using System;
using System.Collections.Generic;
using HermEsb.Configuration.Builder.Registration;
using HermEsb.Core.Messages.Builders;

namespace HermEsb.Configuration.MessageBuilder
{
    /// <summary>
    /// </summary>
    public class ConfigurationBuilderHelper<TConfigurationHelper>
    {
        private readonly TConfigurationHelper _configurationHelper;
        private readonly IMessageBuilder _messageBuilder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigurationBuilderHelper&lt;TConfigurationHelper&gt;" /> class.
        /// </summary>
        /// <param name="configurationHelper">The configuration helper.</param>
        /// <param name="messageBuilder">The message builder.</param>
        public ConfigurationBuilderHelper(TConfigurationHelper configurationHelper, IMessageBuilder messageBuilder)
        {
            _configurationHelper = configurationHelper;
            _messageBuilder = messageBuilder;
        }

        /// <summary>
        ///     Configures the object builder.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public TConfigurationHelper With(IEnumerable<Type> types)
        {
            _messageBuilder.Register(types);

            return _configurationHelper;
        }

        /// <summary>
        ///     Withes the specified interface to type.
        /// </summary>
        /// <param name="interfaceToType">Type of the interface to.</param>
        /// <returns></returns>
        public TConfigurationHelper With(IEnumerable<KeyValuePair<Type, Type>> interfaceToType)
        {
            _messageBuilder.Register(interfaceToType);
            return _configurationHelper;
        }

        /// <summary>
        ///     Withes the specified register.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <returns></returns>
        public ConfigurationRegisterHelper<TConfigurationHelper> With(IRegister register)
        {
            return new ConfigurationRegisterHelper<TConfigurationHelper>(_configurationHelper, _messageBuilder, register);
        }
    }
}