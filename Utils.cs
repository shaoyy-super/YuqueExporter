using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace yuque_exporter
{
    internal class Utils
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        /// <summary>
        /// 发送 HTTP 请求
        /// </summary>
        /// <param name="url">请求的 URL</param>
        /// <param name="method">HTTP 方法（如 GET、POST 等）</param>
        /// <param name="header">自定义请求头</param>
        /// <param name="requestBody">请求体（POST 或 PUT 时使用）</param>
        /// <returns>HTTP 响应内容</returns>
        public static async Task<string> SendHttpRequest(string url, HttpMethod method, Dictionary<string, string> header, object requestBody = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url), "URL不能为空");
            }

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "yuque-exporter/1.0");

            foreach (var keyValue in header)
            {
                _httpClient.DefaultRequestHeaders.Add(keyValue.Key, keyValue.Value);
            }

            string responseBody = null;
            int maxRetries = 5;
            int baseDelay = 1000; // 基础延迟1秒

            for (int retry = 0; retry < maxRetries; retry++)
            {
                try
                {
                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在发送{method}请求到 {url}，第 {retry + 1} 次尝试",ConsoleColor.Gray);
                    
                    HttpResponseMessage response = await SendRequestWithMethod(method, url, requestBody);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        responseBody = await response.Content.ReadAsStringAsync();
                        DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 请求成功: {url}",ConsoleColor.Green);
                        break;
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        DebugLog.LogError($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 请求失败: HTTP {(int)response.StatusCode} - {response.ReasonPhrase}");
                        DebugLog.LogError($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 错误详情: {errorContent}");

                        if ((int)response.StatusCode >= 500)
                        {
                            // 服务器错误，使用指数退避策略重试
                            int delayMilliseconds = (int)Math.Min(Math.Pow(2, retry) * baseDelay, 10000);
                            await Task.Delay(delayMilliseconds);
                            continue;
                        }
                        else if ((int)response.StatusCode == 429)
                        {
                            // 请求过于频繁，等待更长时间
                            await Task.Delay(Math.Min((retry + 1) * 5000, 30000));
                            continue;
                        }
                        else
                        {
                            // 客户端错误，直接抛出异常
                            throw new HttpRequestException($"HTTP {(int)response.StatusCode}: {errorContent}");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    DebugLog.LogError($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] HTTP请求异常: {ex.Message}");
                    if (retry == maxRetries - 1) throw;
                    
                    await Task.Delay((int)Math.Min(Math.Pow(2, retry) * baseDelay, 10000));
                }
                catch (TaskCanceledException)
                {
                    DebugLog.LogError($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 请求超时");
                    if (retry == maxRetries - 1) throw;
                    
                    await Task.Delay((int)Math.Min(Math.Pow(2, retry) * baseDelay, 10000));
                }
                catch (Exception ex)
                {
                    DebugLog.LogError($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 未知异常: {ex.Message}");
                    throw;
                }
            }

            return responseBody;
        }

        public struct ReqBoxInfo
        {
            public string url;
            public Dictionary<string, string> header;
            public object? requestBody;
            public ReqBoxInfo(string url, Dictionary<string, string> header, object? requestBody = null)
            {
                this.url = url;
                this.header = header;
                this.requestBody = requestBody;
            }
        }
        public static async Task<List<string>> SendHttpRequest(List<ReqBoxInfo> reqBoxInfos, HttpMethod method, int secondMaxReqCount = 100)
        {          
            int maxRetryCount = 3;
            Dictionary<ReqBoxInfo, int> reqRetryDic = new Dictionary<ReqBoxInfo, int>();
            List<ReqBoxInfo> curReqBoxInfos = new List<ReqBoxInfo>(reqBoxInfos);
            List<HttpResponseMessage> responseMessageLst = new List<HttpResponseMessage>();
            List<string> responseBodyLst = new List<string>();
            while (curReqBoxInfos.Count > 0)
            {
                long startTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                List<Task<HttpResponseMessage>> tasks = new List<Task<HttpResponseMessage>>();
                Dictionary<Task<HttpResponseMessage >,string> taskDict = new Dictionary<Task<HttpResponseMessage>, string>();
                int count = curReqBoxInfos.Count <= secondMaxReqCount ? curReqBoxInfos.Count : secondMaxReqCount;
                DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在发送{count}个请求，请求方式:{method}",ConsoleColor.Yellow);
                for (int i = 0;i < count; i++)
                {
                    ReqBoxInfo curReqBoxInfo = curReqBoxInfos[0];
                    string url = curReqBoxInfo.url;
                    Dictionary<string, string> header = curReqBoxInfo.header;
                    object? requestBody = curReqBoxInfo.requestBody;
                    curReqBoxInfos.RemoveAt(0);
                    int retryCount = 0;
                    if (reqRetryDic.TryGetValue(curReqBoxInfo, out retryCount))
                    {
                        reqRetryDic[curReqBoxInfo] = retryCount + 1;
                    }
                    else
                    {
                        retryCount = 1;
                        reqRetryDic.Add(curReqBoxInfo, retryCount);
                    }
                    if(retryCount > maxRetryCount)
                    {
                        continue;
                    }
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Add("User-Agent", "yuque-exporter/1.0");

                    foreach (var keyValue in header)
                    {
                        _httpClient.DefaultRequestHeaders.Add(keyValue.Key, keyValue.Value);
                    }

                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 发送请求{url}，第 {retryCount} 次尝试",ConsoleColor.Gray);
                    Task<HttpResponseMessage> task = SendRequestWithMethod(method, url, requestBody);
                    tasks.Add(task);
                    taskDict.Add(task,url);
                }
                int reqSuccessCount = 0;
                int reqFailCount = 0;
                for(int i = 0;i < tasks.Count; i++)
                {
                    Task<HttpResponseMessage> task = tasks[i];
                    HttpResponseMessage response = await task;
                    string reqUrl = taskDict[task];              
                    if (response.IsSuccessStatusCode)
                    {
                        reqSuccessCount++;
                        responseMessageLst.Add(response);                       
                    }
                    else
                    {
                        for (int j = 0; j < reqBoxInfos.Count; j++)
                        {
                            if (reqBoxInfos[j].url == reqUrl)
                            {
                                curReqBoxInfos.Add(reqBoxInfos[j]);
                            }
                        }
                        reqFailCount++;
                        DebugLog.LogWarn($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 请求失败，已重新添加进请求列表...");
                        //string errorContent = await response.Content.ReadAsStringAsync();
                        //DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 请求失败: HTTP {(int)response.StatusCode} - {response.ReasonPhrase}");
                        //DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 错误详情: {errorContent}");
                    }
                }
                DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 请求成功数量:{reqSuccessCount}",ConsoleColor.Green);
                if(reqFailCount > 0)
                {
                    DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 请求失败数量:{reqFailCount}", ConsoleColor.Red);
                }                           
                if (curReqBoxInfos.Count > 0)
                {
                    long endTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    long deltaTimestampMs = endTimestampMs - startTimestampMs;
                    int leftTimestampMs = (int)(deltaTimestampMs > 1000 ? 0 : 1000 - deltaTimestampMs);
                    if (leftTimestampMs > 0)
                    {
                        await Task.Delay(leftTimestampMs);
                    }                 
                }           
            }

            for(int i = 0; i < responseMessageLst.Count; i++)
            {
                HttpResponseMessage response = responseMessageLst[i];
                string responseBody = await response.Content.ReadAsStringAsync();
                responseBodyLst.Add(responseBody);
            }
            return responseBodyLst;
        }

        private static async Task<HttpResponseMessage> SendRequestWithMethod(HttpMethod method, string url, object requestBody)
        {
            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                var jsonContent = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                return await _httpClient.PostAsync(url, content);
            }
            else if (method == HttpMethod.Delete)
            {
                return await _httpClient.DeleteAsync(url);
            }
            else
            {
                return await _httpClient.GetAsync(url);
            }
        }
    }
}
