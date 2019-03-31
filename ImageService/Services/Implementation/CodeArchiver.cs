using System;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using ImageService.Exceptions;
using ImageService.Services.Interfaces;

namespace ImageService.Services.Implementation
{
  public class CodeArchiver : ICodeArchiver
  {
    private const string TgzFilenameFormat = "{0}.tar.gz";

    public string CreateArchive(string solutionPath, string buildId)
    {
      if (solutionPath == null)
        throw new ArgumentNullException(nameof(solutionPath));

      var tgzFilename = string.Format(TgzFilenameFormat, buildId);

      try
      {
        var files = Directory.GetFiles(solutionPath);

        using (var outStream = File.Create(tgzFilename))
        using (var gzoStream = new GZipOutputStream(outStream))
        using (var tarArchive = TarArchive.CreateOutputTarArchive(gzoStream))
        {
          tarArchive.RootPath = Path.GetDirectoryName(solutionPath);

          foreach (var file in files)
          {
            var tarEntry = TarEntry.CreateEntryFromFile(file);
            tarEntry.Name = Path.GetFileName(file);

            tarArchive.WriteEntry(tarEntry, true);
          }
        }
      }
      catch (Exception ex)
      {
        throw new CodeNotAchivedException($"Не удалось создать архив для {solutionPath}", ex);
      }

      return tgzFilename;
    }
  }
}