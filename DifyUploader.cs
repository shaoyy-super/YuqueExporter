using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static yuque_exporter.Utils;

namespace yuque_exporter
{
    /// <summary>
    /// Dify文档列表的响应数据结构
    /// </summary>
    public class DifyDocData
    {       
        [JsonPropertyName("data")]
        public List<DifyDoc> Data { get; set; }       
        [JsonPropertyName("limit")]
        public int Limit { get; set; }      
        [JsonPropertyName("page")]
        public int Page { get; set; }     
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
    /// <summary>
    /// Dify文档的基本信息
    /// </summary>
    public class DifyDoc
    {      
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// Dify 知识库响应列表
    /// </summary>
    public class DifyDatabaseData
    {      
        [JsonPropertyName("data")]
        public List<DifyDatabase> Data { get; set; }     
        [JsonPropertyName("limit")]
        public int Limit { get; set; }      
        [JsonPropertyName("page")]
        public int Page { get; set; }      
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
    /// <summary>
    /// Dify 知识库基本信息
    /// </summary>
    public class DifyDatabase
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("document_count")]
        public int DocumentCount { get; set; }
    }
    /// <summary>
    /// Dify文档上传器，负责将本地文档上传到Dify服务器
    /// </summary>
    public class DifyUploader
    {
        /// <summary>
        /// 上传文档到Dify服务器的主要方法
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task UploadToDify()
        {
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 开始上传文档到Dify服务器...",ConsoleColor.Yellow);
            string currentDirectory = AppContext.BaseDirectory;
            string yuqueDocsPath = Path.Combine(currentDirectory, "yuque_docs");

            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 加载验证配置文件", ConsoleColor.Yellow);
            var config = Config.LoadConfig();           
            if (string.IsNullOrEmpty(config.Dify.Url) || string.IsNullOrEmpty(config.Dify.ApiKey))
            {
                throw new InvalidOperationException("配置信息不完整，请检查config.json");
            }

            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 获取本地知识库列表", ConsoleColor.Yellow);
            List<string> databaseNameLst = SearchDatabaseDirectName(yuqueDocsPath);
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 获取成功，本地知识库数量:{databaseNameLst.Count}", ConsoleColor.Green);

            Dictionary<string, List<string>> databaseName2FilePathLstDict = new Dictionary<string, List<string>>();

            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 获取Dify知识库数据", ConsoleColor.Yellow);
            Dictionary<string, DifyDatabase> difyDatabaseDict = await GetDifyDatabase();
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 获取成功，Dify知识库数据数量:{difyDatabaseDict.Count}", ConsoleColor.Green);

            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 检查需要创建的知识库", ConsoleColor.Yellow);
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 获取本地知识库及其文件列表路径", ConsoleColor.Yellow);
            List<string> needCreateDatabaseNameLst = new List<string>();
            for(int i = 0; i < databaseNameLst.Count; i++)
            {
                if (!difyDatabaseDict.TryGetValue(databaseNameLst[i],out DifyDatabase? value))
                {
                    needCreateDatabaseNameLst.Add(databaseNameLst[i]);
                }
                string dirPath = Path.Combine(currentDirectory, "yuque_docs/" + databaseNameLst[i]);
                if (config.IsDebug)
                {
                    int index = databaseNameLst[i].IndexOf('-');
                    if(index != -1)
                    {
                        dirPath = Path.Combine(currentDirectory, "yuque_docs/" + databaseNameLst[i].Substring(index + 1));
                    }
                }
                databaseName2FilePathLstDict.Add(databaseNameLst[i], GetDirectoryAllFilePath(dirPath));
            }

            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 获取Dify知识库文档列表", ConsoleColor.Yellow);
            Dictionary<string, List<DifyDoc>> databaseDocListDic = new Dictionary<string, List<DifyDoc>>();
            foreach(var kvp in difyDatabaseDict)
            {
                List<DifyDoc> difyDocs = await GetDifyDatabaseDocuments(kvp.Value);
                databaseDocListDic.Add(kvp.Key, difyDocs);
            }
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 获取成功", ConsoleColor.Green);

            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 创建Dify新增知识库，创建数量:{needCreateDatabaseNameLst.Count}", ConsoleColor.Yellow);
            List<DifyDatabase> createDifyDatabaseLst = await CreateDifyDatabase(needCreateDatabaseNameLst);
            for(int i = 0;i < createDifyDatabaseLst.Count; i++)
            {
                if (!difyDatabaseDict.TryGetValue(createDifyDatabaseLst[i].Name, out DifyDatabase? value))
                {
                    difyDatabaseDict[createDifyDatabaseLst[i].Name] = createDifyDatabaseLst[i];
                }
            }
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 创建成功", ConsoleColor.Green);

            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 检查新增文档", ConsoleColor.Yellow);
            Dictionary<string, List<string>> databaseNeedAddDic = GetDatabaseNeedToAddFile(databaseDocListDic, databaseName2FilePathLstDict);
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 检查删除文档", ConsoleColor.Yellow);
            Dictionary<string, List<string>> databaseNeedDelDic = GetDatabaseNeedToDeleteDoc(databaseDocListDic, databaseName2FilePathLstDict);

            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 删除Dify知识库过期文档", ConsoleColor.Yellow);
            foreach (var kvp in databaseNeedDelDic)
            {
                if(difyDatabaseDict.TryGetValue(kvp.Key,out DifyDatabase? difyDatabase) && difyDatabase != null)
                {
                    await DeleteFilesFromDatabase(difyDatabase.ID, kvp.Value);
                }             
            }
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 删除成功", ConsoleColor.Green);

            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 新增Dify知识库最新文档", ConsoleColor.Yellow);
            foreach (var kvp in databaseNeedAddDic)
            {
                if (difyDatabaseDict.TryGetValue(kvp.Key, out DifyDatabase? difyDatabase) && difyDatabase != null)
                {
                    await UploadFilesToDatabase(difyDatabase.ID, kvp.Value);
                }
            }
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 新增成功", ConsoleColor.Green);
        }

        /// <summary>
        /// 获取所有知识库列表
        /// </summary>
        public static async Task<Dictionary<string, DifyDatabase>> GetDifyDatabase()
        {
            int curPage = 1;
            int limit = 100;
            var config = Config.LoadConfig();
            Dictionary<string,DifyDatabase> name2DatabaseDict = new Dictionary<string,DifyDatabase>();
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Authorization", $" Bearer {config.Dify.ApiKey}");
            while (true)
            {
                string responseBody = await Utils.SendHttpRequest(config.Dify.Url + $"/v1/datasets?page={curPage}&limit={limit}", HttpMethod.Get, header);
                DifyDatabaseData? databaseData = JsonSerializer.Deserialize<DifyDatabaseData>(responseBody);
                if (databaseData == null || databaseData.Data.Count == 0)
                {
                    break;
                }
                for (int i = 0; i < databaseData.Data.Count; i++)
                {
                    DifyDatabase difyDatabase = databaseData.Data[i];
                    if (!name2DatabaseDict.ContainsKey(difyDatabase.Name))
                    {
                        name2DatabaseDict.Add(difyDatabase.Name, difyDatabase);
                    }          
                }
                curPage = curPage + 1;
            }
            return name2DatabaseDict;
        }

        /// <summary>
        /// 获取知识库文档列表
        /// </summary>
        /// <param name="databaseId"></param>
        public static async Task<List<DifyDoc>> GetDifyDatabaseDocuments(DifyDatabase difyDatabase)
        {
            int curCount = 0;
            int reqCount = (int)Math.Ceiling(difyDatabase.DocumentCount / 100.0);

            var config = Config.LoadConfig();
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Authorization", $" Bearer {config.Dify.ApiKey}");
            
            List<ReqBoxInfo> reqBoxInfos = new List<ReqBoxInfo>();
           
            while(curCount < reqCount)
            {
                curCount = curCount + 1;
                reqBoxInfos.Add(new ReqBoxInfo(config.Dify.Url + $"/v1/datasets/{difyDatabase.ID}/documents?page={curCount}&limit=100", header));                         
            }

            List<DifyDoc> databaseDocLst = new List<DifyDoc>();

            List<string> responseBodyLst = await Utils.SendHttpRequest(reqBoxInfos,HttpMethod.Get);
            for (int i = 0; i < responseBodyLst.Count; i++)
            {
                var difyDocs = JsonSerializer.Deserialize<DifyDocData>(responseBodyLst[i]);
                if (difyDocs != null && difyDocs.Data != null)
                {
                    for (int j = 0; j < difyDocs.Data.Count; j++)
                    {
                        databaseDocLst.Add(difyDocs.Data[j]);
                    }
                }
            }

            return databaseDocLst;
        }

        /// <summary>
        /// 创建空知识库
        /// </summary>
        /// <returns></returns>
        public static async Task<List<DifyDatabase>> CreateDifyDatabase(List<string> databaseNameLst)
        {          
            var config = Config.LoadConfig();
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Authorization", $" Bearer {config.Dify.ApiKey}");

            List<ReqBoxInfo> reqBoxInfos = new List<ReqBoxInfo>();
            for(int i = 0;i < databaseNameLst.Count; i++)
            {
                var requestBody = new
                {
                    name = databaseNameLst[i],
                    Provider = "vendor",
                    indexing_technique = "high_quality",
                    permission = "only_me"
                };
                reqBoxInfos.Add(new ReqBoxInfo("http://192.168.4.73/v1/datasets", header, requestBody));
            }
            List<DifyDatabase> database = new List<DifyDatabase>();
            List<string> difyDatabaseLst = await Utils.SendHttpRequest(reqBoxInfos, HttpMethod.Post);
            for (int i = 0; i < difyDatabaseLst.Count; i++)
            {
                DifyDatabase? difyDatabase = JsonSerializer.Deserialize<DifyDatabase>(difyDatabaseLst[i]);
                if (difyDatabase == null || string.IsNullOrEmpty(difyDatabase.ID))
                {
                    continue;
                }
                database.Add(difyDatabase);
            }
            return database;
        }

        /// <summary>
        /// 上传文件至知识库
        /// </summary>
        public static async Task UploadFilesToDatabase(string databaseId, List<string> filePaths,int secondMaxReqCount = 20)
        {
            try
            {
                var config = Config.LoadConfig();
                Dictionary<string, string> header = new Dictionary<string, string>();
                header.Add("Authorization", $" Bearer {config.Dify.ApiKey}");
                
                var data = new
                {
                    indexing_technique = "high_quality",
                    process_rule = new
                    {
                        rules = new
                        {
                            pre_processing_rules = new[]
                                {
                                    new { id = "remove_extra_spaces", enabled = true },
                                    new { id = "remove_urls_emails", enabled = true }
                                },
                            segmentation = new { separator = "###", max_tokens = 500 }
                        },
                        mode = "custom"
                    },
                    doc_form = "text_model",
                };
                List<string> filePathLst = new List<string>(filePaths);            
                while (filePathLst.Count > 0)
                {
                    using HttpClient client = new HttpClient();                 
                    
                    foreach (var keyValue in header)
                    {
                        client.DefaultRequestHeaders.Add(keyValue.Key, keyValue.Value);
                    }
                    long startTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    int count = filePathLst.Count <= secondMaxReqCount ? filePathLst.Count : secondMaxReqCount;
                    List<Task<HttpResponseMessage>> tasks = new List<Task<HttpResponseMessage>>();

                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 上传Dify文档->上传数量: {count}", ConsoleColor.Yellow);

                    for (int i = 0; i < count; i++)
                    {
                        string filePath = filePathLst[0];
                        string url = config.Dify.Url + $"/v1/datasets/{databaseId}/document/create-by-file";
                        filePathLst.RemoveAt(0);
                        
                        var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                        var formData = new MultipartFormDataContent(boundary);

                        formData.Add(new StringContent(JsonSerializer.Serialize(data)), "data");                       
                                               
                        if (!File.Exists(filePath))
                        {
                            throw new FileNotFoundException($"文件不存在: {filePath}");
                        }

                        var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                        fileContent.Headers.Add("Content-Type", "application/octet-stream");
                        string headerValue = "form-data; name=\"file\"; filename=\"" + GetFileMD5(filePath) + "_" + Path.GetFileName(filePath) + "\"";
                        byte[] bytes = Encoding.UTF8.GetBytes(headerValue);
                        headerValue = "";
                        foreach (byte b in bytes)
                        {
                            headerValue += (Char)b;
                        }
                        fileContent.Headers.Add("Content-Disposition", headerValue);
                        formData.Add(fileContent);
                       
                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(url),
                            Content = formData
                        };
                        DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 发送请求{url}", ConsoleColor.Gray);
                        tasks.Add(client.SendAsync(request));
                    }
                    HttpResponseMessage[] responses = await Task.WhenAll(tasks);
                    int uploadSuccessCount = 0;
                    int uploadFailureCount = 0;
                    for (int i = 0; i < responses.Length; i++)
                    {                       
                        HttpResponseMessage response = responses[i];
                        
                        if (response.IsSuccessStatusCode)
                        {
                            uploadSuccessCount ++;                                                                          
                        }
                        else
                        {
                            uploadFailureCount++;
                            DebugLog.LogError($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 文档上传失败: {response.StatusCode}");
                            throw new HttpRequestException($"上传失败: {response.StatusCode}");
                        }
                    }
                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 上传Dify文档->上传成功数量: {uploadSuccessCount}",ConsoleColor.Green);
                    if(uploadFailureCount > 0)
                    {
                        DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 上传Dify文档->上传失败数量: {uploadFailureCount}", ConsoleColor.Red);
                    }                  
                    if (filePathLst.Count > 0)
                    {
                        long endTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        long deltaTimestampMs = endTimestampMs - startTimestampMs;
                        int leftTimestampMs = (int)(deltaTimestampMs > 1000 ? 0 : 1000 - deltaTimestampMs);
                        if (leftTimestampMs > 0)
                        {
                            await Task.Delay(leftTimestampMs);
                        }
                    }
                    client.Dispose();                  
                }               
            }
            catch (Exception ex)
            {
                DebugLog.LogError($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 上传文档时发生错误: {ex.Message}");
                throw;
            }

        }
        /// <summary>
        /// 从知识库删除文档
        /// </summary>
        public static async Task DeleteFilesFromDatabase(string databaseId, List<string> docIdLst)
        {
            var config = Config.LoadConfig();
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Authorization", $" Bearer {config.Dify.ApiKey}");
            List<ReqBoxInfo> reqBoxInfos = new List<ReqBoxInfo>();
            for(int i = 0;i < docIdLst.Count; i++)
            {
                reqBoxInfos.Add(new ReqBoxInfo(config.Dify.Url + $"/v1/datasets/{databaseId}/documents/{docIdLst[i]}", header));
            }         
            await Utils.SendHttpRequest(reqBoxInfos, HttpMethod.Delete, 1000);
        }

        /// <summary>
        /// 获取知识库文件夹名
        /// </summary>
        public static List<string> SearchDatabaseDirectName(string entryPath)
        {
            var config = Config.LoadConfig();
            List<string> databaseNameLst = new List<string>();
            if (Directory.Exists(entryPath))
            {
                string[] directories = Directory.GetDirectories(entryPath);
                foreach (string dir in directories)
                {   
                    string databaseName = Path.GetFileName(dir);
                    if (config.IsDebug)
                    {
                        databaseName = "测试-" + databaseName;
                    }
                    databaseNameLst.Add(databaseName);
                }
            }
            else
            {
                DebugLog.Log("入口目录不存在！");
            }
            return databaseNameLst;
        }

        /// <summary>
        /// 获取指定文件夹内的所有文件的路径
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static List<string> GetDirectoryAllFilePath(string directoryPath)
        {
            List<string> filePathLst = new List<string>();
            if (!Directory.Exists(directoryPath))
            {
                return filePathLst;
            }
            string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                filePathLst.Add(file);
            }
            return filePathLst;
        }

        /// <summary>
        /// 获取需要向Dify知识库新增文件的路径
        /// </summary>
        /// <param name="databaseDocListDic"></param>
        /// <param name="databaseName2FilePathLstDict"></param>
        /// <returns>key 知识库名;value 文件路径列表</returns>
        public static Dictionary<string, List<string>> GetDatabaseNeedToAddFile(Dictionary<string, List<DifyDoc>> databaseDocListDic, Dictionary<string, List<string>> databaseName2FilePathLstDict)
        {
            Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();
            foreach(var kvp in databaseName2FilePathLstDict)
            {
                List<string> filePathLst = kvp.Value;
                if (filePathLst == null || filePathLst.Count == 0)
                {
                    continue;
                }
                if(databaseDocListDic.TryGetValue(kvp.Key, out List<DifyDoc>? difyDocLst) && difyDocLst != null)
                {                    
                    for(int i = 0;i < filePathLst.Count; i++)
                    {
                        bool isExist = false;
                        string md5 = GetFileMD5(filePathLst[i]);
                        for(int j = 0;j < difyDocLst.Count; j++)
                        {
                            string docMd5 = difyDocLst[j].Name.Split('_')[0];
                            if(docMd5 == md5)
                            {
                                isExist = true;
                                break;
                            }
                        }
                        if (!isExist)
                        {
                            if(keyValuePairs.TryGetValue(kvp.Key,out List<string>? addFilePathLst))
                            {
                                if(addFilePathLst == null)
                                {
                                    keyValuePairs[kvp.Key] = new List<string>() { filePathLst[i] };
                                }
                                else
                                {
                                    keyValuePairs[kvp.Key].Add(filePathLst[i]);
                                }
                            }
                            else
                            {
                                keyValuePairs.Add(kvp.Key, new List<string>() { filePathLst[i] });
                            }
                        }

                    }                 
                }
                else
                {
                    keyValuePairs.Add(kvp.Key, kvp.Value);
                }

            }
            return keyValuePairs;
        }

        /// <summary>
        /// 获取需要从Dify知识库删除的文档
        /// </summary>
        /// <param name="databaseDocListDic"></param>
        /// <param name="databaseName2FilePathLstDict"></param>
        /// <returns>key 知识库名;value为文档id列表</returns>
        public static Dictionary<string, List<string>> GetDatabaseNeedToDeleteDoc(Dictionary<string, List<DifyDoc>> databaseDocListDic, Dictionary<string, List<string>> databaseName2FilePathLstDict)
        {
            Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();
            foreach(var kvp in databaseDocListDic)
            {
                List<DifyDoc> difydocLst = kvp.Value;
                if(difydocLst == null || difydocLst.Count == 0)
                {
                    continue;
                }
                if(!databaseName2FilePathLstDict.TryGetValue(kvp.Key,out List<string>? _))
                {
                    continue;
                }

                if(databaseName2FilePathLstDict.TryGetValue(kvp.Key,out List<string>? filePathLst) && filePathLst != null)
                {
                    List<string> md5Lst = new List<string>();
                    for(int i = 0;i < filePathLst.Count; i++)
                    {
                        md5Lst.Add(GetFileMD5(filePathLst[i]));
                    }
                    for(int i = 0;i < difydocLst.Count; i++)
                    {
                        bool isExist = false;
                        string md5 = difydocLst[i].Name.Split('_')[0];
                        for(int j = 0;j < md5Lst.Count; j++)
                        {
                            if(md5Lst[j] == md5)
                            {
                                isExist = true;
                                break;
                            }
                        }
                        if (!isExist)
                        {
                            if(keyValuePairs.TryGetValue(kvp.Key,out List<string>? deleteIdLst))
                            {
                                if(deleteIdLst != null)
                                {
                                    keyValuePairs[kvp.Key].Add(difydocLst[i].ID);
                                }
                                else
                                {
                                    keyValuePairs[kvp.Key] = new List<string>() { difydocLst[i].ID };
                                }
                            }
                            else
                            {
                                keyValuePairs.Add(kvp.Key, new List<string>() { difydocLst[i].ID });
                            }
                        }
                    }
                }
                else
                {
                    List<string> deleteDocIdLst = new List<string>();                 
                    for (int i = 0;i < difydocLst.Count; i++)
                    {
                        deleteDocIdLst.Add(difydocLst[i].ID);
                    }
                    keyValuePairs.Add(kvp.Key, deleteDocIdLst);
                }               
            }
            return keyValuePairs;
        }

        /// <summary>
        /// 获取文件的md5码
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileMD5(string filePath)
        {
            if (!File.Exists(filePath))
            {
                DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 文件不存在{filePath}");
                return "";
            }
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }



}
