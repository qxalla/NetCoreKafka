using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogApi.Domain.Entity;
using Confluent.Kafka;
using System.Threading;
using LogApi.Contracts;

namespace LogApi.Domain
{
    public class DataLogManager: IDataLogManager
    {
        public async Task<bool> AddAsync(DataLog data)
        {
            // TODO pasar a variables de ambiente o setting de la api
            string kafkaEndpoint = "127.0.0.1:9092";
            string kafkaTopic = "registro-log";

            var config = new ProducerConfig { BootstrapServers = kafkaEndpoint };

            Action<DeliveryReport<Null, string>> handler = r =>
                Console.WriteLine(!r.Error.IsError
                ? $"Delivered message to {r.TopicPartitionOffset}"
                : $"Delivery Error: {r.Error.Reason}");

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                await producer.ProduceAsync(kafkaTopic, new Message<Null, string> { Value = data.Mensaje });
                producer.Flush(TimeSpan.FromSeconds(10));
            }

            return true;
        }

        public List<DataLog> GetAllAsync()
        {
            var result = new List<DataLog>();

            // TODO pasar a variables de ambiente o setting de la api
            string kafkaEndpoint = "127.0.0.1:9092";
            string kafkaTopic = "registro-log";

            var config = new ConsumerConfig
            {
                GroupId = "logs-consumers",
                BootstrapServers = kafkaEndpoint,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(kafkaTopic);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = consumer.Consume(cts.Token);
                            // Console.WriteLine(cr.Value);
                            result.Add(new DataLog() { Mensaje = cr.Value });
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }

            return result;
        }
    }
}
