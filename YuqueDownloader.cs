using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using static yuque_exporter.Utils;

namespace yuque_exporter
{
    /// <summary>
    /// 语雀文档下载器，负责从语雀API获取并下载文档内容
    /// </summary>
    public class YuqueDownloader
    {
        /// <summary>
        /// 知识库列表的响应数据结构
        /// </summary>
        public class Repos
        {
            [JsonPropertyName("data")]
            public List<Repo> Data { get; set; }
        }

        /// <summary>
        /// 知识库信息，包含知识库的基本属性
        /// </summary>
        public class Repo
        {
            /// <summary>
            /// 知识库名称
            /// </summary>
            [JsonPropertyName("name")]
            public string Name { get; set; }
        /// <summary>
        /// 知识库命名空间，用于API调用
        /// </summary>
            [JsonPropertyName("namespace")]
            public string Namespace { get; set; }
        /// <summary>
        /// 是否公开，0表示私有，1表示公开
        /// </summary>
            [JsonPropertyName("public")]
            public int Public { get; set; }
        /// <summary>
        /// 知识库中的文档数量
        /// </summary>
            [JsonPropertyName("items_count")]
            public int ItemsCount { get; set; }
        }

        /// <summary>
        /// 文档列表的响应数据结构
        /// </summary>
        public class Docs
        {
            [JsonPropertyName("data")]
            public List<Docinfo> Data { get; set; }
        }

        /// <summary>
        /// 文档基本信息
        /// </summary>
        public class Docinfo
        {
            /// <summary>
            /// 文档标题
            /// </summary>
            [JsonPropertyName("title")]
            public string Title { get; set; }
        /// <summary>
        /// 文档的唯一标识符
        /// </summary>
            [JsonPropertyName("slug")]
            public string Slug { get; set; }
        }

        /// <summary>
        /// 文档详情的响应数据结构
        /// </summary>
        public class DocData
        {
            [JsonPropertyName("data")]
            public DocDetail Data { get; set; }
        }

        /// <summary>
        /// 文档详细内容
        /// </summary>
        public class DocDetail
        {
            /// <summary>
            /// 文档标题
            /// </summary>
            [JsonPropertyName("title")]
            public string Title { get; set; }
        /// <summary>
        /// 文档正文内容（Markdown格式）
        /// </summary>
            [JsonPropertyName("body")]
            public string Body { get; set; }
        }

        /// <summary>
        /// 用户信息的响应数据结构
        /// </summary>
        public class UserData
        {
            [JsonPropertyName("data")]
            public User Data { get; set; }
        }

        /// <summary>
        /// 用户基本信息
        /// </summary>
        public class User
        {
            /// <summary>
            /// 用户名称
            /// </summary>
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        /// <summary>
        /// 下载语雀文档的主要方法
        /// </summary>
        /// <returns>异步任务</returns>
        public static async Task DownloadYuqueDoc()
        {
            // 获取当前执行目录的绝对路径
            string currentDirectory = AppContext.BaseDirectory;
            string yuqueDocsPath = Path.Combine(currentDirectory, "yuque_docs");

            // 导出文档的路径与文档内容
            List<KeyValuePair<string,string>> docInfoLst = new List<KeyValuePair<string,string>>();

            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 开始下载语雀文档，目标目录: {yuqueDocsPath}");

            // 如果目标目录已存在，则先删除再创建
            if (Directory.Exists(yuqueDocsPath))
            {
                DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 清理已存在的目标目录");
                Directory.Delete(yuqueDocsPath, true);
            }
            Directory.CreateDirectory(yuqueDocsPath);
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 创建目标目录完成",ConsoleColor.Green);

            // 加载配置文件
            var config = yuque_exporter.Config.LoadConfig();
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 配置文件加载完成",ConsoleColor.Green);

            // 遍历配置中的所有语雀分组
            DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 开始处理语雀分组，共 {config.Yuque.Groups.Count} 个分组");
            foreach (var group in config.Yuque.Groups)
            {
                try
                {
                    // 设置API请求头
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("X-Auth-Token", group.Token);

                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在处理分组: {group.Description}");
                    // 获取用户信息
                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在获取用户信息...");
                    string userResponse = await Utils.SendHttpRequest(config.Yuque.BaseUrl + $"/api/v2/user", HttpMethod.Get, headers);
                    var user = JsonSerializer.Deserialize<UserData>(userResponse);
                    if (user?.Data == null)
                    {
                        DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 获取用户信息失败: {group.Description}");
                        continue;
                    }
                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 成功获取用户信息: {user.Data.Name}", ConsoleColor.Green);

                    // 创建用户文档目录
                    string userPath = Path.Combine(yuqueDocsPath, user.Data.Name);
                    Directory.CreateDirectory(userPath);
                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 创建用户目录: {userPath}");

                    // 获取知识库列表
                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在获取知识库列表...");
                    string reposResponse = await Utils.SendHttpRequest(config.Yuque.BaseUrl + $"/api/v2/groups/{group.Url}/repos", HttpMethod.Get, headers);
                    var repos = JsonSerializer.Deserialize<Repos>(reposResponse);
                    if (repos?.Data == null)
                    {
                        DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 获取知识库列表失败: {group.Description}");
                        continue;
                    }
                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 成功获取知识库列表，共 {repos.Data.Count} 个知识库",ConsoleColor.Green);
                    // 遍历所有公开的知识库
                    foreach (var repo in repos.Data.Where(r => r.Public != 0))
                    {
                        try
                        {
                            await ProcessRepos(userPath, repo, headers, docInfoLst);
                        }
                        catch (Exception ex)
                        {
                            DebugLog.LogError($"处理知识库 {repo.Name} 时发生错误: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    DebugLog.LogError($"处理分组 {group.Description} 时发生错误: {ex.Message}");
                }
            }

            // 导出文档
            await ExportText(docInfoLst);

        }

        /// <summary>
        /// 处理单个知识库的文档下载
        /// </summary>
        /// <param name="user_path">用户文档目录路径</param>
        /// <param name="repo">知识库信息</param>
        /// <param name="header">HTTP请求头</param>
        /// <returns>异步任务</returns>
        static async Task ProcessRepos(string userPath, Repo repo, Dictionary<string, string> headers, List<KeyValuePair<string, string>> docInfoLst)
        {
            try
            {
                var config = yuque_exporter.Config.LoadConfig();
                string repoPath = Path.Combine(userPath, repo.Name);
                Directory.CreateDirectory(repoPath);
                int offset = 0;          
                List<ReqBoxInfo> reqBoxInfos = new List<ReqBoxInfo>();
                while (offset < repo.ItemsCount)
                {
                    string repoId = repo.Namespace;
                    reqBoxInfos.Add(new ReqBoxInfo(config.Yuque.BaseUrl + $"/api/v2/repos/{repoId}/docs?offset={offset}", headers));
                    offset += 100;
                }
                List< string > repoResponseLst = await Utils.SendHttpRequest(reqBoxInfos, HttpMethod.Get,100);
                List<ReqBoxInfo> docReqBoxInfo = new List<ReqBoxInfo>();
                for (int i = 0; i < repoResponseLst.Count; i++)
                {
                    Docs? docs = JsonSerializer.Deserialize<Docs>(repoResponseLst[i]);
                    if (docs?.Data == null)
                    {
                        continue;
                    }
                    // 遍历文档列表，获取每个文档的详细内容
                    foreach (var doc in docs.Data)
                    {                     
                        docReqBoxInfo.Add(new ReqBoxInfo(config.Yuque.BaseUrl + $"/api/v2/repos/{repo.Namespace}/docs/{doc.Slug}", headers));                                                 
                    }
                }

                List<string> docDetailLst = await Utils.SendHttpRequest(docReqBoxInfo, HttpMethod.Get);
                for (int i = 0;i < docDetailLst.Count; i++)
                {
                    // 解析文档详情并保存为Markdown文件
                    var docData = JsonSerializer.Deserialize<DocData>(docDetailLst[i]);
                    if (docData?.Data != null)
                    {
                        // 处理文件名中的非法字符
                        string fileName = docData.Data.Title;
                        foreach (char invalidChar in Path.GetInvalidFileNameChars())
                        {
                            fileName = fileName.Replace(invalidChar.ToString(), "");
                        }
                        string filePath = Path.Combine(repoPath, $"{fileName}.md");                      
                        docInfoLst.Add(new KeyValuePair<string, string>(filePath, docData.Data.Body));
                    }                   
                }             
            }
            catch (Exception ex)
            {
                DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 处理知识库时发生错误: {ex.Message}");
                throw;
            }
        }

        static async Task ExportText(List<KeyValuePair<string,string>> docInfoLst)
        {
            try
            {
                List<Task> taskLst = new List<Task>();
                for (int i = 0; i < docInfoLst.Count; i++)
                {
                    string filePath = docInfoLst[i].Key;
                    string docText = docInfoLst[i].Value;
                    taskLst.Add(File.WriteAllTextAsync(filePath, docText));
                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 导出文档: {filePath}",ConsoleColor.Gray);
                }
                await Task.WhenAll(taskLst);
            }
            catch (Exception ex)
            {
                DebugLog.LogError($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 导出文档时发生错误: {ex.Message}");
                throw;
            }           
        }
    }
}
