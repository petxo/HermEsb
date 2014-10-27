HermEsb
=======

HermEsb es una solución de codigo abierto, que facilita la comunicación entre la diferentes partes de una 
aplicación distribuida, por medio de sistema de mensajería (AMQP o MSMQ).

Permite la implementación de los patrones de diseño arquitectónicos:

- Request - Reply
- Sender - Receiver
- Publish - Suscriber

Sender-Receiver
---------------
Creacion del publicador
```cs
var busPublisher = ConfigurationPublisher.With("config/publisher.xml")
                                    .Log4NetBuilder("config/logging.xml")
                                    .ConfigurePublisher()
                                    .Create();
                                    
```
Creacion y publicación de un mensaje

```cs
var messageBasic = busPublisher.MessageBuilder.CreateInstance<IMessageBasic>(basic =>
                {
                    basic.Fecha = DateTime.UtcNow;
                    basic.Nombre = "Lorem ipsum dolor sit amet, iusto utamur consequuntur mel an.";
                });
                
busPublisher.Publish(messageBasic);
```


