HermEsb
=======

HermEsb es una solución de codigo abierto, que facilita la comunicación entre la diferentes partes de una 
aplicación distribuida, por medio de sistema de mensajería (AMQP o MSMQ).

Permite la implementación de los patrones de diseño arquitectónicos:

- Sender - Receiver
- Publish - Suscriber
- Request - Reply

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

Publish-Suscriber
=================

Creación del BUS 
----------------


```cs
var service = ConfigurationHelper.With(Path.Combine(Environment.CurrentDirectory, "config/service.xml"))
                    .Log4NetBuilder("config/logging.xml")
                    .ConfigureBus()
                    .Create();

service.Start();
                                    
```

La configuracion del bus se establece en el fichero "service.xml"

```xml
<?xml version="1.0"?>
<configuration>
  <configSections>
    <section name="HermEsb" type="HermEsb.Configuration.Bus.HermEsbConfig, HermEsb.Configuration"/>
  </configSections>
  <HermEsb>
    <identification id="Bus" type="Bus"/>
    <routerProcessor>
      <input transport="RabbitMq">
        <add name="uri" value="amqp://localhost/HermEsbSamples.Exch/BasicBusSample.Input/inputBasicBusSampleKey"/>
      </input>
    </routerProcessor>

    <routerControlProcessor>
      <input transport="RabbitMq">
        <add name="uri" value="amqp://localhost/HermEsbSamples.Exch/BasicBusSample.ControlInput/controlinputBasicBusSampleKey"/>
      </input>
    </routerControlProcessor>
  </HermEsb>
</configuration>

```

Creación del Servicio
---------------------

```cs
var service = ConfigurationHelper.With("config/service.xml")
                                .Log4NetBuilder("config/logging.xml")
                                .ConfigureMessageBuilder()
                                    .With(
                                            RegisterTypes.FromAssembly(typeof(MessageBasic))
                                                .Pick()
                                                .WithService.FirstInterface()
                                         ).AllInterfaceToClass()
                                .ConfigureService()
                                .Create();

service.Start();
```

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="HermEsbService" type="HermEsb.Configuration.Services.HermEsbServiceConfig, HermEsb.Configuration"/>
  </configSections>

  <HermEsbService>
    <identification id="Service1" type="Service"/>
    <bus>
      <controlInput transport="RabbitMq">
        <add name="uri" value="amqp://localhost/HermEsbSamples.Exch/BasicBusSample.ControlInput/controlinputBasicBusSampleKey"/>
      </controlInput>
    </bus>

    <serviceProcessor numberOfParallelTasks="100">
      <input transport="RabbitMq">
        <add name="uri" value="amqp://localhost/HermEsbSamples.Exch/BasicServiceSample.Input/BasicServiceSampleInputKey"/>
      </input>
      <handlersAssemblies>
        <add name="BasicSampleService" assembly="BasicSampleService.exe" />
      </handlersAssemblies>
    </serviceProcessor>
    <controlProcessor>
      <input transport="RabbitMq">
        <add name="uri" value="amqp://localhost/HermEsbSamples.Exch/BasicServiceSample.ControlInput/BasicServiceSampleControlInputKey"/>
      </input>
      <handlersAssemblies>
      </handlersAssemblies>
    </controlProcessor>
  </HermEsbService>
</configuration>

```

Para el servicio el handler debe implementar la interfaz "IServiceMessageHandler"

```cs
public class MessageBasicHandler : IServiceMessageHandler<IMessageBasic>
{
    public void HandleMessage(IMessageBasic message)
    {
        Console.WriteLine("Hola son las {0}", message.Fecha);
    }

    public void Dispose()
    {
        
    }

    public IBus Bus { get; set; }
}
```

Creación del Publicador
-----------------------

El publicador se configura igual que el sender a excepcion que se le indica la cola del BUS


```cs
var busPublisher = ConfigurationPublisher.With("config/publisher.xml")
                                    .Log4NetBuilder("config/logging.xml")
                                    .ConfigurePublisher()
                                    .Create();

var messageBasic = busPublisher.MessageBuilder.CreateInstance<MessageBasic>(basic =>
                {
                    basic.Fecha = DateTime.UtcNow;
                    basic.Nombre = "Lorem ipsum dolor sit amet, iusto utamur consequuntur mel an.";
                });
                
busPublisher.Publish(messageBasic);               
```

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="publisher" type="HermEsb.Configuration.Publishers.PublisherConfig, HermEsb.Configuration"/>
  </configSections>

  <publisher>
    <identification id="Publisher1" type="Publisher"/>
      <output transport="RabbitMq">
        <add name="uri" value="amqp://localhost/HermEsbSamples.Exch/BasicBusSample.Input/inputBasicBusSampleKey"/>
      </output>
  </publisher>
</configuration>
```
