using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;

namespace LedTableSimulator
{
    public class Mqtt
    {
        public event EventHandler<IEnumerable<byte>> OnFrameBufferReceived;

        public async Task<bool> TryConnect(string ip)
        {
            if (String.IsNullOrWhiteSpace(ip))
            {
                return false;
            }

            try
            {
                var factory = new MqttFactory();
                var builder = new MqttClientOptionsBuilder();

                IMqttClientOptions options = builder
                                            .WithClientId("LedTableSimulator")
                                            .WithTcpServer(ip)
                                            .Build();

                IMqttClient client = factory.CreateMqttClient();

                await client.ConnectAsync(options);

                await client.SubscribeAsync("ledtable/simulation/framebuffer");

                client.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(Handler);

                return client.IsConnected;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Handler(MqttApplicationMessageReceivedEventArgs obj)
        {
            if (obj.ApplicationMessage?.Payload != null && obj.ApplicationMessage.Payload.Length == 300)
            {
                OnFrameBufferReceived?.Invoke(this, obj.ApplicationMessage?.Payload);
            }
        }
    }
}