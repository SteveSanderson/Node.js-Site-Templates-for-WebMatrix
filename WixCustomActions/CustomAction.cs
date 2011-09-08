using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Xml.Linq;
using Microsoft.Deployment.WindowsInstaller;
using System;

namespace WebMatrixSiteTemplates.WixCustomActions
{
    public class CustomActions
    {
        // During uninstallation, we need to know which of the feed entries belong to this installer, so they can be removed
        private const string OwnedFeedEntriesIdentifier = "{58520BA8-DF03-4D4B-A311-5452F5DA5957}";
        private static readonly XNamespace FeedEntriesNamespace = XNamespace.Get("http://www.w3.org/2005/Atom");

        private static string TemplateFeedEntriesTempFolder {
            get { return Path.Combine(Path.GetTempPath(), "TemplateFeedEntries"); }
        }

        private static string WebMatrixTemplatesFolder {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Microsoft WebMatrix", "templates"); }
        }

        [CustomAction]
        public static ActionResult RegisterTemplateFeedItems(Session session) {
            // First clear out any previous versions
            var removeTemplateFeedItemsResult = RemoveTemplateFeedItems(session);
            if (removeTemplateFeedItemsResult != ActionResult.Success)
                return removeTemplateFeedItemsResult;

            // ... then add the new ones
            var feedEntriesDir = TemplateFeedEntriesTempFolder;
            if (Directory.Exists(feedEntriesDir)) {

                var feedEntries = Directory.EnumerateFiles(feedEntriesDir, "*.xml");
                var webMatrixFeedFile = Path.Combine(WebMatrixTemplatesFolder, "TemplateFeed.xml");
                if (File.Exists(webMatrixFeedFile)) {
                    foreach (var feedEntryPath in feedEntries) {
                        RegisterFeedEntries(webMatrixFeedFile, feedEntryPath);
                    }
                }
            }
            return ActionResult.Success;
        }

        private static void RegisterFeedEntries(string webMatrixFeedFile, string feedEntryPath) {
            var feedXml = XDocument.Load(webMatrixFeedFile);
            var feedEntryXml = XDocument.Load(feedEntryPath);

            var entriesToAdd = feedEntryXml.Root.Elements(FeedEntriesNamespace + "entry").ToList();
            foreach (var entryToAdd in entriesToAdd) {
                feedXml.Root.Add(entryToAdd);
            }

            feedXml.Save(webMatrixFeedFile);
        }

        private static ActionResult RemoveTemplateFeedItems(Session session) {
            var webMatrixFeedFile = Path.Combine(WebMatrixTemplatesFolder, "TemplateFeed.xml");
            if (File.Exists(webMatrixFeedFile)) {
                var feedXml = XDocument.Load(webMatrixFeedFile);
                var oldItems = from node in feedXml.Root.Elements(FeedEntriesNamespace + "entry")
                               let installedByNode = node.Element(FeedEntriesNamespace + "installedBy")
                               where installedByNode != null && installedByNode.Value == OwnedFeedEntriesIdentifier
                               select node;
                foreach (var itemToRemove in oldItems.ToList())
                    itemToRemove.Remove();
                feedXml.Save(webMatrixFeedFile);
            }

            return ActionResult.Success;
        }
    }
}
