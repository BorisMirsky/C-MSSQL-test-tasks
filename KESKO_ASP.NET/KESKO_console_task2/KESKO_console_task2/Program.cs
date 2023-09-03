using System.IO.MemoryMappedFiles;


namespace KESKO_console_task2
{
    class Program
    {
        static void Main()
        {
            char[] message;
            int size;
            MemoryMappedFile sharedMemory = MemoryMappedFile.OpenExisting("MemoryFile");
            using (MemoryMappedViewAccessor reader = sharedMemory.CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read))
            {
                size = reader.ReadInt32(0);
            }
            using (MemoryMappedViewAccessor reader = sharedMemory.CreateViewAccessor(4, size * 2, MemoryMappedFileAccess.Read))
            {
                message = new char[size];
                reader.ReadArray<char>(0, message, 0, size);
            }
            string charsStr = new string(message);
            string[] splitted = charsStr.Split(' ');
            int NumI = Convert.ToInt32(splitted[0]);
            int NumJ = Convert.ToInt32(splitted[1]);
            int result = NumI + NumJ;
            //---------------------------------------------------
            char[] message1 = result.ToString().ToCharArray();
            int size1 = message1.Length;
            MemoryMappedFile sharedMemory1 = MemoryMappedFile.CreateOrOpen("MemoryFile", size1 * 2 + 4);
            using (MemoryMappedViewAccessor writer1 = sharedMemory1.CreateViewAccessor(0, size1 * 2 + 4))
            {
                writer1.Write(0, size1);
                writer1.WriteArray<char>(4, message1, 0, size1);
            }
        }
    }
}

