namespace TrabalhoProg.Utils
{
    public class TextToFile
    {
        public static async Task WriteToFile(string content, string fileName)
        {
            var logPath = @$"C:\{fileName}";
            using (var writer = File.CreateText(logPath))
            {
                await writer.WriteLineAsync(content);
            }
        }
    }
}
