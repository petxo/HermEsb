using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace HermEsb.Extended.JobObjects.Configuration.Converters
{
    public class ProcessPriorityClassConverter : TypeConverter
    {
        /// <summary>
        /// Devuelve si este convertidor puede convertir un objeto del tipo dado al tipo de este convertidor, utilizando el contexto especificado.
        /// </summary>
        /// <param name="context">Interfaz <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> que proporciona un contexto de formato.</param>
        /// <param name="sourceType"><see cref="T:System.Type"/> que representa el tipo a partir del cual se desea realizar la conversión.</param>
        /// <returns>
        /// true si este convertidor puede realizar la conversión; en caso contrario, false.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(int) || sourceType == typeof(long) || sourceType == typeof(string);
        }

        /// <summary>
        /// Convierte el objeto dado al tipo de este convertidor, utilizando el contexto especificado y la información de referencia cultural.
        /// </summary>
        /// <param name="context"><see cref="T:System.ComponentModel.ITypeDescriptorContext"/> que proporciona un contexto de formato.</param>
        /// <param name="culture"><see cref="T:System.Globalization.CultureInfo"/> que se va a utilizar como la referencia cultural actual.</param>
        /// <param name="value"><see cref="T:System.Object"/> que se va a convertir.</param>
        /// <returns>
        ///   <see cref="T:System.Object"/> que representa el valor convertido.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">No se puede realizar la conversión. </exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is int || value is long)
            {
                var lVal = (long) value;
                switch (lVal)
                {
                    case 32:
                        return ProcessPriorityClass.Normal;
                    case 64:
                        return ProcessPriorityClass.Idle;
                    case 128:
                        return ProcessPriorityClass.High;
                    case 256:
                        return ProcessPriorityClass.RealTime;
                    case 16384:
                        return ProcessPriorityClass.BelowNormal;
                    case 32768:
                        return ProcessPriorityClass.AboveNormal;
                    default:
                        return ProcessPriorityClass.Normal;
                }
            }
            if (value != null)
            {
                var sVal = (string)value;
                switch (sVal)
                {
                    case "Normal":
                        return 32;
                    case "Idle":
                        return 64;
                    case "High":
                        return 128;
                    case "RealTime":
                        return 256;
                    case "BelowNormal":
                        return 16384;
                    case "AboveNormal":
                        return 32768;
                    default:
                        return "Normal";
                }
            }
            return 32;
        }

        /// <summary>
        /// Devuelve si este convertidor puede convertir el objeto al tipo especificado, utilizando el contexto especificado.
        /// </summary>
        /// <param name="context">Interfaz <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> que proporciona un contexto de formato.</param>
        /// <param name="destinationType"><see cref="T:System.Type"/> que representa el tipo de destino de la conversión.</param>
        /// <returns>
        /// true si este convertidor puede realizar la conversión; en caso contrario, false.
        /// </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(int) || destinationType == typeof(long) || destinationType == typeof(string);
        }

        /// <summary>
        /// Convierte el objeto de valor dado al tipo especificado usando el contexto especificado y la información de referencia cultural.
        /// </summary>
        /// <param name="context"><see cref="T:System.ComponentModel.ITypeDescriptorContext"/> que proporciona un contexto de formato.</param>
        /// <param name="culture"><see cref="T:System.Globalization.CultureInfo"/>.Si se pasa null, se supone que se trata de la actual información de referencia cultural.</param>
        /// <param name="value"><see cref="T:System.Object"/> que se va a convertir.</param>
        /// <param name="destinationType"><see cref="T:System.Type"/> al que se va a convertir el parámetro <paramref name="value"/>.</param>
        /// <returns>
        ///   <see cref="T:System.Object"/> que representa el valor convertido.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">El valor del parámetro <paramref name="destinationType"/> es null. </exception>
        ///   
        /// <exception cref="T:System.NotSupportedException">No se puede realizar la conversión. </exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType)
        {
            var val = (ProcessPriorityClass) value;
            if (destinationType == typeof (string))
                return Enum.GetName(typeof(ProcessPriorityClass), val);
            if (destinationType == typeof (int) || destinationType == typeof (long))
            {
                switch (val)
                {
                    case ProcessPriorityClass.Normal:
                        return Enum.GetValues(typeof(ProcessPriorityClass)).GetValue(0);
                    case ProcessPriorityClass.Idle:
                        return Enum.GetValues(typeof(ProcessPriorityClass)).GetValue(1);
                    case ProcessPriorityClass.High:
                        return Enum.GetValues(typeof(ProcessPriorityClass)).GetValue(2);
                    case ProcessPriorityClass.RealTime:
                        return Enum.GetValues(typeof(ProcessPriorityClass)).GetValue(3);
                    case ProcessPriorityClass.BelowNormal:
                        return Enum.GetValues(typeof(ProcessPriorityClass)).GetValue(4);
                    case ProcessPriorityClass.AboveNormal:
                        return Enum.GetValues(typeof(ProcessPriorityClass)).GetValue(5);
                    default:
                        return Enum.GetValues(typeof(ProcessPriorityClass)).GetValue(0);
                }
            }
            return null;
        }         
    }
}