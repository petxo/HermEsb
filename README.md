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

            var busPublisher = ConfigurationPublisher.With("config/publisher.xml")
                                                .Log4NetBuilder("config/logging.xml")
                                                .ConfigurePublisher()
                                                .Create();



