using System.Configuration;
using HermEsb.Core.Ioc;

namespace HermEsb.Extended.JobObjects.Configuration
{
    [ConfigurationCollection(typeof(JobObjectConfig), AddItemName = "jobObject", CollectionType = ConfigurationElementCollectionType.BasicMapAlternate)]
    public class JobsConfig : ConfigurationElementCollection
    {
        #region [ Overrides of ConfigurationElementCollection ]

        /// <summary>
        /// Cuando se reemplaza en una clase derivada, se crea un nuevo objeto <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </summary>
        /// <returns>
        /// Nuevo objeto <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
        	return (ConfigurationElement)ContextManager.Instance.CurrentContext.Resolve<IJobObjectConfig>();
        }

        /// <summary>
        /// Obtiene la clave de elemento para un elemento de configuración especificado cuando se reemplaza en una clase derivada.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Object"/> que actúa como clave para el objeto <see cref="T:System.Configuration.ConfigurationElement"/> especificado.
        /// </returns>
        /// <param name="element">Objeto <see cref="T:System.Configuration.ConfigurationElement"/> para el que se va a devolver la clave. </param>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((JobObjectConfig) element).Name;
        }

        /// <summary>
        /// Obtiene el nombre que se utiliza para identificar esta colección de elementos en el archivo de configuración cuando se reemplaza en una clase derivada.
        /// </summary>
        /// <returns>Nombre de la colección; de lo contrario, una cadena vacía.El valor predeterminado es una cadena vacía.</returns>
        protected override string ElementName
        {
            get { return "jobObject"; }
        }

        /// <summary>
        /// Indica si el objeto <see cref="T:System.Configuration.ConfigurationElement"/> especificado existe en la colección <see cref="T:System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="elementName">Nombre del elemento que se va a comprobar.</param>
        /// <returns>
        /// true si el elemento está en la colección; en caso contrario, false.El valor predeterminado es false.
        /// </returns>
        protected override bool IsElementName(string elementName)
        {
            return elementName == "jobObject";
        }

        /// <summary>
        /// Obtiene el tipo de <see cref="T:System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <returns>Enumerador <see cref="T:System.Configuration.ConfigurationElementCollectionType"/> de esta colección.</returns>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMapAlternate; }
        }

        /// <summary>
        /// Obtiene un valor que indica si la colección <see cref="T:System.Configuration.ConfigurationElementCollection"/> es de sólo lectura.
        /// </summary>
        /// <returns>
        /// true si la colección <see cref="T:System.Configuration.ConfigurationElementCollection"/> es de sólo lectura; en caso contrario, false.
        /// </returns>
        public override bool IsReadOnly()
        {
            return false;
        }

        /// <summary>
        /// Obtiene o establece una propiedad, un atributo o un elemento secundario de este elemento de configuración.
        /// </summary>
        /// <returns>Propiedad, atributo o elemento secundario especificados.</returns>
        ///   
        /// <exception cref="T:System.Configuration.ConfigurationErrorsException">
        ///   <paramref name="prop"/> es de sólo lectura o se bloquea.</exception>
        public JobObjectConfig this[int index]
        {
            get { return (JobObjectConfig) BaseGet(index); }
            set 
            { 
                if (BaseGet(index) != null) BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Obtiene o establece una propiedad, un atributo o un elemento secundario de este elemento de configuración.
        /// </summary>
        /// <returns>Propiedad, atributo o elemento secundario especificados.</returns>
        ///   
        /// <exception cref="T:System.Configuration.ConfigurationErrorsException">
        ///   <paramref name="prop"/> es de sólo lectura o se bloquea.</exception>
        new public JobObjectConfig this[string name]
        {
            get { return (JobObjectConfig) BaseGet(name); }
        }


        #endregion
    }
}