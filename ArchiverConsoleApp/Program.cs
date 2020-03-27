
using System;
using System.IO;
using System.IO.Compression;
using System.Threading;

class Program
{
    static object locker = new object();
    static int BlockCount = 0;
    public readonly struct argsForFileCompress
    {
        public readonly byte[] sourceBytearr;
        public readonly string compressedFile;
        public argsForFileCompress(byte[] sourceBytearr, string compressedFile)
        {
            this.sourceBytearr = sourceBytearr;
            this.compressedFile = compressedFile;
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Please, enter the path of the file that should be compressed ");
        string targetFile = "D:/test/file_new.gz";
        string sourceFile = Console.ReadLine();

        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
        {

            int fileIOblockSize = 64 * 1024;        // read up to 64kB each time 
            byte[] IObuff = new byte[fileIOblockSize];// buffer to hold bytes
            int count = 0;
            while (true)
            {

                int readCount = sourceStream.Read(IObuff, 0, IObuff.Length);
                Console.WriteLine(readCount);

                if (readCount == 0) break;


                argsForFileCompress argsCompress = new argsForFileCompress(IObuff, targetFile);
                Thread myThread = new Thread(new ParameterizedThreadStart((Compress)));
                myThread.Name = (count++).ToString();
                Console.WriteLine(myThread.Name);
                myThread.Start((object)argsCompress);
            }
        }
    }

    #region Archieve2
    public static void Compress(object _argsCompress)
    {
        int threadId;
        Int32.TryParse(Thread.CurrentThread.Name, out threadId);

        argsForFileCompress argsCompress = (argsForFileCompress)_argsCompress;
        Byte[] streamtobyte;
        // поток для чтения исходного файла
        using (MemoryStream sourceStream = new MemoryStream(argsCompress.sourceBytearr, false))
        {
            // поток для записи сжатого файла
            using (MemoryStream targetStream = new MemoryStream())
            {
                // поток архивации
                using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                {
                    sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                    Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}, ПОТОК: {3}.",
                     argsCompress.sourceBytearr, sourceStream.Length.ToString(), targetStream.Length.ToString(), threadId);

                }

                streamtobyte = targetStream.ToArray();
            }
        }
        do
        {                                   //задерживает дальнейшее выполнение кода, пока номер потока не совпадёт со значением переенной BlockCount, чтобы блоки передавались на запись по порядку  
        } while (threadId != BlockCount);
        if (threadId == BlockCount)
        {
            lock (locker)
            {
                WriteCompressedBlockToFile(argsCompress.compressedFile, streamtobyte, threadId);
            }

        }
    }

    public static void WriteCompressedBlockToFile(string filePath, Byte[] compressedBlock, int offset)
    {
        using (FileStream SourceStream = File.Open(filePath, FileMode.Append, FileAccess.Write))
        {
            long x = SourceStream.Position;
            Console.WriteLine("Position before: {0}", x);
            SourceStream.Write(compressedBlock, 0, compressedBlock.Length);
            x = SourceStream.Position;
            Console.WriteLine("Position after: {0}", x);
        }
        BlockCount++;
        Console.WriteLine("WriteCompressedBlockToFile offset {0} BlockCount{1}", offset, BlockCount);
    }

    public static void Decompress(string compressedFile, string targetFile)
    {
        // поток для чтения из сжатого файла
        using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
        {
            // поток для записи восстановленного файла
            using (FileStream targetStream = File.Create(targetFile))
            {
                // поток разархивации
                using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(targetStream);
                    Console.WriteLine("Восстановлен файл: {0}", targetFile);
                }
            }
        }
    }
    #endregion
}