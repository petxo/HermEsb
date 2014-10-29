HermEsb
=======

HermEsb es una solución de codigo abierto, que facilita la comunicación entre la diferentes partes de una 
aplicación distribuida, por medio de sistema de mensajería (AMQP o MSMQ).

Permite la implementación de los patrones de diseño arquitectónicos:

- Request - Reply
- Sender - Receiver
- Publish - Suscriber

Sender-Receiver
===============

Creación del emisor 
-------------------


```cs
var sender = ConfigurationPublisher.With("config/sender.xml")
                                    .Log4NetBuilder("config/logging.xml")
                                    .ConfigureMessageBuilder().With(
                                            RegisterTypes.FromAssembly(typeof(IMySimpleData))
                                                .Pick()
                                                .WithService.FirstInterface()
                                         ).AllInterfaceToClass()
                                    .ConfigurePublisher()
                                    .Create();
                                    
```

La configuracion del publicador se establece en el fichero "sender.xml"

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="publisher" type="HermEsb.Configuration.Publishers.PublisherConfig, HermEsb.Configuration"/>
  </configSections>

  <publisher>
    <identification id="Sender1" type="Sender"/>
      <output transport="RabbitMq">
        <add name="uri" value="amqp://localhost/HermEsbSamples.Exch/ReceiverSample.Input/inputReceiverSample"/>
      </output>
  </publisher>
</configuration>
```

Creacion y publicación de un mensaje

```cs
var messageBasic = Sender.MessageBuilder.CreateInstance<IMySimpleData>(basic =>
                {
                    basic.Fecha = DateTime.UtcNow;
                    basic.Nombre = "Lorem ipsum dolor sit amet, iusto utamur consequuntur mel an.";
                });
                
sender.Publish(messageBasic);
```

Creación del receptor
---------------------

```cs
    var service = ConfigurationHelper.With("config/receiver.xml")
        .Log4NetBuilder("config/logging.xml")
        .ConfigureListener()
        .Create();

    service.Start();
```
El fichero de configuracion del receptor, es casi igual que el del emisor, a excepción
de que hay que informar la libreria donde se encuentran los handlers de los mensajes.
En este caso es el propia consola.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="listener" type="HermEsb.Configuration.Listeners.ListenerConfig, HermEsb.Configuration"/>
  </configSections>

  <listener>
    <input transport="RabbitMq">
      <add name="uri" value="amqp://localhost/HermEsbSamples.Exch/ReceiverSample.Input/inputReceiverSample"/>
    </input>
    <handlersAssemblies>
      <add name="ReceiverSample" assembly="ReceiverSample.exe" />
    </handlersAssemblies>
  </listener>
</configuration>
```

El handler del mensaje del listerner debe implementar la interfaz "IListenerMessageHandler" en donde se especifica
que tipo de mensaje se maneja. En este ejemplo, cada vez que llegue un mensaje que implemente la interfaz "IMySimpleData",
se levantara este handler, y se ejecutará el método "HandleMessage"

```cs
public class MySimpleDataHandler : IListenerMessageHandler<IMySimpleData>
{
    public void HandleMessage(IMySimpleData message)
    {
        Console.WriteLine(message.Data);
    }

    public void Dispose()
    {
        
    }
}
```
