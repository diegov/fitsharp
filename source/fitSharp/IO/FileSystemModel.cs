// Copyright � 2010 Syterra Software Inc. All rights reserved.
// The use and distribution terms for this software are covered by the Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file license.txt at the root of this distribution. By using this software in any fashion, you are agreeing
// to be bound by the terms of this license. You must not remove this notice, or any other, from this software.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace fitSharp.IO {
    public class FileSystemModel: FolderModel {
        
        private readonly Encoding encoding;
        private readonly Stack<string> directories;

        public FileSystemModel() 
            : this(Encoding.UTF8) {}

        public FileSystemModel(int codePage)
            : this(Encoding.GetEncoding(codePage)) {}

        private FileSystemModel(Encoding encoding) {
            this.encoding = encoding;
            directories = new Stack<string>();
        }

        public void PushDir(string path) {
            directories.Push(Directory.GetCurrentDirectory());
            Directory.SetCurrentDirectory(path);
        }

        public void PopDir() {
            var previousDir = directories.Pop();
            Directory.SetCurrentDirectory(previousDir);
        }

        public void MakeFile(string thePath, string theContent) {
            if (!Directory.Exists(Path.GetDirectoryName(thePath))) {
                Directory.CreateDirectory(Path.GetDirectoryName(thePath));
            }
            TextWriter writer = new StreamWriter(thePath, false, encoding);
            writer.Write(theContent);
            writer.Close();
        }

        public TextWriter MakeWriter(string thePath) {
            return new StreamWriter(thePath);
        }

        public string FileContent(string thePath) {
            StreamReader reader;
            try {
                var file = new FileStream(thePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                reader = new StreamReader(file, encoding);
            }
            catch (FileNotFoundException) {
                return null;
            }
            string result = reader.ReadToEnd();
            reader.Close();
            return result;
        }

        public string[] GetFiles(string thePath) {
            return Directory.Exists(thePath)
                       ? Directory.GetFiles(thePath)
                       : (File.Exists(thePath) ? new[] {thePath} : new string[] {});
        }

        public string[] GetFolders(string thePath) {
            return !Directory.Exists(thePath) ? new string[] {} : Directory.GetDirectories(thePath);
        }

        public void CopyFile(string theInputPath, string theOutputPath) {
            try {
                File.Delete(theOutputPath);
            }
            catch (DirectoryNotFoundException) {
                Directory.CreateDirectory(Path.GetDirectoryName(theOutputPath));
            }
            File.Copy(theInputPath, theOutputPath);
        }
        
        public void WriteToConsole(string theMessage) {
            Console.Write(theMessage);
        }
    }
}
