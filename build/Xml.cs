using System;
using System.IO;
using System.Xml;
using Nuke.Common;
using Nuke.Common.IO;

public static class Xml
{
    public static string XmlPeek(AbsolutePath filePath, string xpath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Source File not found.", filePath);

        using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
        using (var xmlReader = XmlReader.Create(fileStream))
        {
            var document = new XmlDocument {PreserveWhitespace = true};
            document.Load(xmlReader);

            var navigator = document.CreateNavigator();
            var namespaceManager = new XmlNamespaceManager(navigator.NameTable);

            var node = navigator.SelectSingleNode(xpath, namespaceManager);
            var xmlValue = node?.Value;
            if (xmlValue == null)
                Logger.Warn("Warning: Failed to find node matching the XPath '{0}'", xpath);
            return xmlValue;
        }
    }
}
