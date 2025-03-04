using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace yuque_exporter
{
    /// <summary>
    /// Dify文档列表的响应数据结构
    /// </summary>
    public class DifyDocData
    {
        /// <summary>
        /// 文档列表数据
        /// </summary>
        [JsonPropertyName("data")]
        public List<DifyDoc> Data { get; set; }
    /// <summary>
    /// 每页显示的数量限制
    /// </summary>
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    /// <summary>
    /// 当前页码
    /// </summary>
    [JsonPropertyName("page")]
    public int Page { get; set; }
    /// <summary>
    /// 文档总数
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }
    }
    /// <summary>
    /// Dify文档的基本信息
    /// </summary>
    public class DifyDoc
    {
        /// <summary>
        /// 文档的唯一标识符
        /// </summary>
        [JsonPropertyName("id")]
        public string ID { get; set; }
    }
    /// <summary>
    /// Dify文档上传器，负责将本地文档上传到Dify服务器
    /// </summary>
    public class DifyUploader
    {
        /// <summary>
        /// 上传文档到Dify服务器的主要方法
        /// </summary>
        /// <returns>异步任务</returns>
        public static async Task UploadToDify()
        {
            // 获取当前执行目录的绝对路径
            string currentDirectory = Environment.CurrentDirectory;
            string yuqueDocsPath = Path.Combine(currentDirectory, "yuque_docs");
    
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 开始上传文档到Dify服务器...");
    
            // 加载并验证Dify配置信息
            var config = Config.LoadConfig();
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 配置文件加载完成");
    
            if (string.IsNullOrEmpty(config.Dify.Url) || string.IsNullOrEmpty(config.Dify.DatasetId) || string.IsNullOrEmpty(config.Dify.ApiKey))
            {
                throw new InvalidOperationException("Dify配置信息不完整，请检查config.json");
            }
    
            // 设置API请求头
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $" Bearer {config.Dify.ApiKey}");
    
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 开始清理Dify知识库中的现有文档...");
            // 删除知识库中的所有现有文档
            int page = 1;
            int total = 999999999; // 初始设置一个较大的值
            int deletedCount = 0;
            while (total > 0)
            {
                // 获取知识库中的文档列表
                string difyDocsResponse = await Utils.SendHttpRequest(config.Dify.Url + $"/v1/datasets/{config.Dify.DatasetId}/documents", HttpMethod.Get, headers);
                var difyDocs = JsonSerializer.Deserialize<DifyDocData>(difyDocsResponse);
                if (difyDocs == null)
                    break;
    
                total = difyDocs.Total;
                // 删除每个文档
                for (int i = 0; i < difyDocs.Data.Count; i++)
                {
                    await Utils.SendHttpRequest(config.Dify.Url + $"/v1/datasets/{config.Dify.DatasetId}/documents/{difyDocs.Data[i].ID}", HttpMethod.Delete, headers);
                    deletedCount++;
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 已删除文档 {deletedCount}/{total}");
                }
            }
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 知识库清理完成，共删除 {deletedCount} 个文档");
    
            // 上传本地目录中的所有文档
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 开始上传本地文档...");
            await UploadDirectory(yuqueDocsPath);
        }
    /// <summary>
    /// 上传单个文件到Dify服务器
    /// </summary>
    /// <param name="filePath">要上传的文件路径</param>
    /// <returns>异步任务</returns>
    static async Task UploadFile(string filePath)
        {
            var config = Config.LoadConfig();
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Authorization", $" Bearer {config.Dify.ApiKey}");
            await UploadDocument(config.Dify.Url + $"/v1/datasets/{config.Dify.DatasetId}/document/create-by-file", header, filePath);
        }
    /// <summary>
    /// 递归上传目录中的所有文件
    /// </summary>
    /// <param name="path">要上传的目录路径</param>
    /// <returns>异步任务</returns>
    static async Task UploadDirectory(string path)
        {
            // 上传当前目录下的所有文件
            string[] files = Directory.GetFiles(path);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在处理目录: {path}，发现 {files.Length} 个文件");
            foreach (string file in files)
            {
                try
                {
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在上传文件: {file}");
                    await UploadFile(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 处理文件 {file} 时发生错误: {ex.Message}");
                }
            }
    
            // 递归处理所有子目录
            string[] subDirs = Directory.GetDirectories(path);
            if (subDirs.Length > 0)
            {
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 发现 {subDirs.Length} 个子目录");
            }
            foreach (string subDir in subDirs)
            {
                try
                {
                    await UploadDirectory(subDir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 处理目录 {subDir} 时发生错误: {ex.Message}");
                }
            }
        }
    /// <summary>
    /// 执行文档上传的核心方法
    /// </summary>
    /// <param name="url">上传接口的URL</param>
    /// <param name="header">HTTP请求头</param>
    /// <param name="file_path">要上传的文件路径</param>
    /// <returns>异步任务</returns>
    static async Task UploadDocument(string url, Dictionary<string, string> header, string filePath)
        {
            try
            {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "DefaultUserAgent");
    
                // 添加请求头
                foreach (var keyValue in header)
                {
                    client.DefaultRequestHeaders.Add(keyValue.Key, keyValue.Value);
                }
    
                // 构造multipart/form-data请求体
                var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                var formData = new MultipartFormDataContent(boundary);
    
                // 添加文档处理规则
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
                formData.Add(new StringContent(JsonSerializer.Serialize(data)), "data");
    
                // 添加文件内容
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"文件不存在: {filePath}");
                }
    
                var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                fileContent.Headers.Add("Content-Type", "application/octet-stream");
                String headerValue = "form-data; name=\"file\"; filename=\"" + Path.GetFileName(filePath) + "\"";
                byte[] bytes = Encoding.UTF8.GetBytes(headerValue);
                headerValue = "";
                foreach (byte b in bytes)
                {
                    headerValue += (Char)b;
                }
                fileContent.Headers.Add("Content-Disposition", headerValue);
                formData.Add(fileContent);
    
                // 发送请求并处理响应
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Content = formData
                };
    
                HttpResponseMessage response = await client.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 文档上传成功:");
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 文档上传失败: {response.StatusCode}");
                    Console.WriteLine(responseBody);
                    throw new HttpRequestException($"上传失败: {response.StatusCode} - {responseBody}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 上传文档时发生错误: {ex.Message}");
                throw;
            }
        }
    }
}
