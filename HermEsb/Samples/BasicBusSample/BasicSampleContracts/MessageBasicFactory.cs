using System;
using System.Collections.Generic;

namespace BasicSampleContracts
{
    public static class MessageBasicFactory
    {
        public static void FillData(MessageBasic messageBasic)
        {
            messageBasic.Fecha = DateTime.Now;
            messageBasic.Data = new SubMessage
                {
                    Data = new List<string> {"hola", "mundo"},
                    OtraFecha = DateTime.Now
                };
            messageBasic.ListaEnteros = new List<int> {2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 1, 1, 1, 1, 1, 1, 1};
        }
    }
}