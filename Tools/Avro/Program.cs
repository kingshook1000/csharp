using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Collections.Generic;
using Microsoft.Hadoop.Avro.Container;
using Microsoft.Hadoop.Avro.Schema;
using Microsoft.Hadoop.Avro;

namespace Avro
{
    class Program
    {
        public static string avroSchema = @"
        {
            ""type"":""record"",
            ""name"":""EventData"",
            ""namespace"":""Microsoft.ServiceBus.Messaging"",
            ""fields"":[
                {""name"":""SequenceNumber"",""type"":""long""},
                {""name"":""Offset"",""type"":""string""},
                {""name"":""EnqueuedTimeUtc"",""type"":""string""},
                {""name"":""SystemProperties"",""type"":{""type"":""map"",""values"":[""long"",""double"",""string"",""bytes""]}},
                {""name"":""Properties"",""type"":{""type"":""map"",""values"":[""long"",""double"",""string"",""bytes""]}},
                {""name"":""Body"",""type"":[""null"",""bytes""]}
            ]
        }";
        static void Main(string[] args)
        {
            DeserializeLedgerData();
            //SerializeLedgerData();

            

        }

        static void SerializeLedgerData()
        {
            //Create a generic serializer based on the schema
            var serializer = AvroSerializer.CreateGeneric(avroSchema);
            var rootSchema = serializer.WriterSchema as RecordSchema;
            var testData = new List<AvroRecord>();
            dynamic expected = new AvroRecord(rootSchema);
            expected.SequenceNumber = Convert.ToInt64(878425);
            expected.Offset = "339329813680";
            expected.EnqueuedTimeUtc = "2020-11-05T00:00:00.0";
            expected.SystemProperties = new Dictionary<string, object>();
            expected.Properties = new Dictionary<string, object>();
            
            var content = File.ReadAllText(@"E:\code\csharp1\Tools\Avro\input\2c212757-f000-4428-b458-fc5ef1df1a67");
            expected.Body = Encoding.ASCII.GetBytes(content);

            testData.Add(expected);

            dynamic expected2 = new AvroRecord(rootSchema);
            expected2.SequenceNumber = Convert.ToInt64(878425);
            expected2.Offset = "339329813680";
            expected2.EnqueuedTimeUtc = "2020-11-05T00:00:00.0";
            expected2.SystemProperties = new Dictionary<string, object>();
            expected2.Properties = new Dictionary<string, object>();

            var content2 = File.ReadAllText(@"E:\code\csharp1\Tools\Avro\input\01916726-cadc-4b33-91d0-ac6c2a003527");
            expected2.Body = Encoding.ASCII.GetBytes(content2);

            testData.Add(expected2);

            var path = @"c:\temp\ledger.avro";

            using (var buffer = new MemoryStream())
            {
                Console.WriteLine("Serializing Sample Data Set...");

                //Create a SequentialWriter instance for type SensorData, which can serialize a sequence of SensorData objects to stream.
                //Data will not be compressed (Null compression codec).
                using (var writer = AvroContainer.CreateGenericWriter(avroSchema, buffer, Codec.Null))
                {
                    using (var streamWriter = new SequentialWriter<object>(writer, 24))
                    {
                        // Serialize the data to stream by using the sequential writer
                        testData.ForEach(streamWriter.Write);
                    }
                }

                Console.WriteLine("Saving serialized data to file...");

                //Save stream to file
                if (!WriteFile(buffer, path))
                {
                    Console.WriteLine("Error during file operation. Quitting method");
                    return;
                }
            }

        }

        private static bool WriteFile(MemoryStream InputStream, string path)
        {
            if (!File.Exists(path))
            {
                try
                {
                    using (FileStream fs = File.Create(path))
                    {
                        InputStream.Seek(0, SeekOrigin.Begin);
                        InputStream.CopyTo(fs);
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("The following exception was thrown during creation and writing to the file \"{0}\"", path);
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Can not create file \"{0}\". File already exists", path);
                return false;

            }
        }

        static void DeserializeLedgerData()
        {
            var fileName = @"E:\code\csharp1\Tools\Avro\ledger.avro";
            var outputFolder = @".\output\";

            bool exists = System.IO.Directory.Exists(outputFolder);

            if (!exists)
                System.IO.Directory.CreateDirectory(outputFolder);

            using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = AvroContainer.CreateGenericReader(stream))
                {
                    while (reader.MoveNext())
                    {
                        foreach (dynamic result in reader.Current.Objects)
                        {
                            var record = new AvroEventData(result);
                            var jsonBody = Encoding.UTF8.GetString(record.Body);
                            //Console.WriteLine(jsonBody);
                            var jsonBlobName = outputFolder + Guid.NewGuid().ToString();
                            File.WriteAllText(jsonBlobName, jsonBody);



                        }
                    }
                }
            }
        }


    }

    public struct AvroEventData
    {
        public AvroEventData(dynamic record)
        {
            SequenceNumber = (long)record.SequenceNumber;
            Offset = (string)record.Offset;
            DateTime.TryParse((string)record.EnqueuedTimeUtc, out var enqueuedTimeUtc);
            EnqueuedTimeUtc = enqueuedTimeUtc;
            SystemProperties = (Dictionary<string, object>)record.SystemProperties;
            Properties = (Dictionary<string, object>)record.Properties;
            Body = (byte[])record.Body;
        }
        public long SequenceNumber { get; set; }
        public string Offset { get; set; }
        public DateTime EnqueuedTimeUtc { get; set; }
        public Dictionary<string, object> SystemProperties { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public byte[] Body { get; set; }
    }
}
