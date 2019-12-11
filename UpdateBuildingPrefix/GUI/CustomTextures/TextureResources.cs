using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;

namespace UpdateBuildingPrefix.GUI.CustomTextures
{
    public static class TextureResources
    {
        public static readonly Texture2D MainMenuButtonTexture2D;
        public static readonly Texture2D MainMenuButtonsTexture2D;
        public static readonly Texture2D NoImageTexture2D;
        public static readonly Texture2D RemoveButtonTexture2D;
        public static readonly Texture2D WindowBackgroundTexture2D;
        public static readonly Texture2D DistrictCommandCenterLogo;
        public static Assembly SelectedAssembly { get; set; }

        static TextureResources()
        {
            NoImageTexture2D = LoadDllResource("no-image.png", 64, 64);
            NoImageTexture2D.name = "DCC.Icons.NoImage";

            MainMenuButtonTexture2D = LoadDllResource("Menu.main-menu-button.png", 300, 50);
            MainMenuButtonTexture2D.name = "DCC.Icons.MainMenuButton";

            DistrictCommandCenterLogo = LoadDllResource("Menu.main-menu-logo.png", 600, 111);
            DistrictCommandCenterLogo.name = "DCC.Logo";
        }
        
        internal static Texture2D LoadDllResource(string resourceName, int width, int height)
        {
            try
            {
                SelectedAssembly = Assembly.LoadFile("C:\\Users\\Mike\\AppData\\Local\\Colossal Order\\Cities_Skylines\\Addons\\Mods\\UpdateBuildingPrefix\\UpdateBuildingPrefix.dll");

                Debug.Log($"Loading DllResource {resourceName} from Assembly {SelectedAssembly.ToString()}.");
                var myStream = SelectedAssembly.GetManifestResourceStream("UpdateBuildingPrefix.Resources." + resourceName);

                var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);

                texture.LoadImage(ReadToEnd(myStream));

                return texture;

            } catch (Exception e)
            {
                Debug.LogError($"Error loading Texture {resourceName}: {e.Message}\r\nStack Trace:\r\n{e.StackTrace}.");
                return null;
            }
        }

        static byte[] ReadToEnd(Stream stream)
        {
            var originalPosition = stream.Position;
            stream.Position = 0;

            try
            {
                var readBuffer = new byte[4096];

                var totalBytesRead = 0;
                int bytesRead;

                while((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead != readBuffer.Length)
                        continue;

                    var nextByte = stream.ReadByte();
                    if (nextByte == -1)
                        continue;

                    var temp = new byte[readBuffer.Length * 2];
                    Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                    Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                    readBuffer = temp;
                    totalBytesRead++;
                }

                var buffer = readBuffer;
                if (readBuffer.Length == totalBytesRead)
                    return buffer;

                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                return buffer;
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace.ToString());
                return null;
            }
            finally
            {
                stream.Position = originalPosition;
            }
        }
    }
}
