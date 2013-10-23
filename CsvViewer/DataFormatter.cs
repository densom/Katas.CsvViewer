namespace CsvViewer
{
    public class DataFormatter
    {
        public DataFormatter(Data data)
        {
            Data = data;
        }

        public Data Data { get; private set; }
    }
}