using Base.Helpers;
using System;
using System.IO;
using System.Reflection;

namespace CloudKeyFileProvider
{
    public enum KeyType
    {
        GoogleCloudErrorReporting,
        GoogleCloudFirestore,
        GoogleCloudLogging,
        GoogleCloudMonitoring,
        GoogleCloudRedis
    }

    public class KeyProvider
    {
        private readonly static string KeyFilesDir = "KeyFiles";
        public static string GetCloudKey(string keyfileName, KeyType key)
        {
            try
            {
                var keyPath = Path.Combine(
                                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                Path.Combine(
                                    KeyFilesDir,
                                    key.ToString(),
                                    keyfileName
                                ));

                using (StreamReader r = new StreamReader(keyPath))
                {
                    string json = r.ReadToEnd();
                    return json;
                }

            }
            catch (Exception ex)
            {
                #region Error Catch and response error
                Logger.PringDebug($"-----Error{ System.Reflection.MethodBase.GetCurrentMethod().Name }-----");
                Logger.PringDebug(ex.ToString());
                return "";
                #endregion
            }

        }
    }
}
