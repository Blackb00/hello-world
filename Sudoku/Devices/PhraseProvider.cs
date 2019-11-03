using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sudoku
{
    class PhraseProvider: IPhraseProvider
    {
        private Dictionary<string, string> FileData;
        public PhraseProvider(string fileName)
        {
            var jsonFile = new FileInfo(fileName);
            if (!jsonFile.Exists)
            {
                throw new ArgumentException(
                   $"Can't find file in {jsonFile}");
            }

            var jsonFileContent = File.ReadAllText(jsonFile.FullName);

            try
            {
                var jsonFileData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonFileContent);
                FileData = jsonFileData;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't extract file {fileName}", ex);
            }

        }

        public string getPhrase(string phraseKey)
        {
            return FileData[phraseKey];
        }
    }
}
